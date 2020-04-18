var db = new Dexie('database');
db.version(1).stores({
    mice: "biological_sample_id",
    miceImages: "++,biological_sample_id,jpeg_url,parameter_name",
    miceImagesWorking: "++,biological_sample_id,jpeg_url,parameter_name",
    miceImagesResults: "++,biological_sample_id,jpeg_url,parameter_name",
    miceData: "biological_sample_id,phenotyping_center,date_of_birth,sex,age_in_weeks,weight,gene_symbol,gene_accession_id,zygosity,parameter_name,observation_type,category",
    results: "biological_sample_id,phenotyping_center,date_of_birth,sex,age_in_weeks,weight,gene_symbol,gene_accession_id,zygosity,parameter_name,observation_type,category",
});
db.open();


//adds json data into dexieDB
function loadData() {
    var jsonMice = {};
    var jsonMiceImages = {};
    var jsonMiceData = {};
    const proxyurl = "https://cors-anywhere.herokuapp.com/";
    return db.mice.count(function (count) {
        if (count > 0) {
            console.log("Already populated");
        } else {
            console.log("Populating tables");
            return fetch(proxyurl + 'https://www.ebi.ac.uk/mi/impc/solr/impc_images/select?q=(biological_sample_id:[*%20TO%20*]%20AND%20parameter_name:%22XRay%20Images%20Whole%20Body%20Dorso%20Ventral%22)&fl=biological_sample_id&rows=40').then(
                res => res.json()
            ).then(
                (data) => {
                    jsonMice = data.response.docs;
                }
            ).then(function () {
                return db.mice.clear();
            }).then(function () {
                return db.miceImages.clear();
            }).then(function () {
                return db.miceImagesWorking.clear();
            }).then(function () {
                return db.miceImagesResults.clear();
            }).then(function () {
                return db.miceData.clear();
            }).then(function () {
                return db.results.clear();
            }).then(function () {
                return db.mice.bulkPut(jsonMice);
            }).then(function () {
                return db.mice.toArray();
            }).then(function (arr) {
                var urlStr = 'https://www.ebi.ac.uk/mi/impc/solr/experiment/select?q=biological_sample_id:(';
                for (var i = 0; i < arr.length; i++) {
                    urlStr += arr[i].biological_sample_id + '%20OR%20';
                };
                urlStr = urlStr.substring(0, urlStr.length - 8);
                urlStr += ')&rows=40&fl=biological_sample_id,phenotyping_center,date_of_birth,sex,age_in_weeks,weight,gene_symbol,gene_accession_id,zygosity,parameter_name,observation_type,category&group=true&group.field=biological_sample_id&group.limit=1&group.format=simple';
                console.log(urlStr);
                return urlStr;
            }).then(function (urlStr) {
                return fetch(proxyurl + urlStr)
            }).then(
                res => res.json()
            ).then(
                (data) => {
                    jsonMiceData = data.grouped.biological_sample_id.doclist.docs;
                }
            ).then(function () {
                db.miceData.bulkAdd(jsonMiceData);
            }).then(function () {
                return db.mice.toArray();
            }).then(function (arr) {
                var urlStr = 'https://www.ebi.ac.uk/mi/impc/solr/impc_images/select?q=(biological_sample_id:(';
                for (var i = 0; i < arr.length; i++) {
                    urlStr += arr[i].biological_sample_id + '%20OR%20';
                };
                urlStr = urlStr.substring(0, urlStr.length - 8);
                urlStr += ')%20NOT%20jpeg_url:"img/filetype_pdf.png")&rows=1000&fl=biological_sample_id,jpeg_url,parameter_name';
                console.log(urlStr);
                return urlStr;
            }).then(function (urlStr) {
                return fetch(proxyurl + urlStr);
            }).then(
                res => res.json()
            ).then(
                (data) => {
                    jsonMiceImages = data.response.docs;
                    console.log(jsonMiceImages);
                }
            ).then(function () {
                db.miceImages.bulkAdd(jsonMiceImages);
            }).then(function () {
                console.log("Tables Populated");
            }).catch(function (err) {
                console.error(err.stack || err);
            });
        }
    });
};

//Queries for dexieDB
function queryDb(idOption, centerOption, dobOption, sexOption, ageOption, weightOption, geneSymbOption, geneAccIdOption, zygosityOption, parameterOption, obvsTypeOption, categoryOption) {
    console.log(idOption, centerOption, dobOption, sexOption, ageOption, weightOption, geneSymbOption, geneAccIdOption, zygosityOption, parameterOption, obvsTypeOption, categoryOption);
    return db.miceData.toArray().then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
    }).then(function () {
        if (idOption == 'A') { return db.results.toArray(); }
        else { return db.results.where('biological_sample_id').equals(parseInt(idOption)).toArray(); }
    }).then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
    }).then(function () {
        if (centerOption == 'A') { return db.results.toArray(); }
        else { return db.results.where('phenotyping_center').equals(centerOption).toArray(); }
    }).then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
    }).then(function () {
        if (dobOption[0] == 'A' && dobOption[1] == 'A') { return db.results.toArray(); }
        else if (dobOption[0] == 'A' && dobOption[1] != 'A') { return db.results.where('date_of_birth').noneOf('').and(value => Date.parse(value.date_of_birth) <= Date.parse(dobOption[1])).toArray();}
        else if (dobOption[1] == 'A' && dobOption[0] != 'A') { return db.results.where('date_of_birth').noneOf('').and(value => Date.parse(value.date_of_birth) >= Date.parse(dobOption[0])).toArray();}
        else { return db.results.where('date_of_birth').noneOf('').and(value => Date.parse(value.date_of_birth) >= Date.parse(dobOption[0]) && Date.parse(value.date_of_birth) <= Date.parse(dobOption[1])).toArray(); }
    }).then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
    }).then(function () {
        if (sexOption == 'A') { return db.results.toArray(); }
        else { return db.results.where('sex').equals(sexOption).toArray(); }
    }).then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
    }).then(function () {
        if (ageOption[0] == 'A' && ageOption[1] == 'A') { return db.results.toArray(); }
        else if (ageOption[0] == 'A' && ageOption[1] != 'A') { return db.results.where('age_in_weeks').belowOrEqual(parseInt(ageOption[1])).toArray(); }
        else if (ageOption[1] == 'A' && ageOption[0] != 'A') { return db.results.where('age_in_weeks').aboveOrEqual(parseInt(ageOption[0])).toArray(); }
        else { return db.results.where('age_in_weeks').aboveOrEqual(parseInt(ageOption[0])).and(value => value.age_in_weeks <= parseInt(ageOption[1])).toArray(); }
    }).then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
    }).then(function () {
        if (weightOption[0] == 'A' && weightOption[1] == 'A') { return db.results.toArray(); }
        else if (weightOption[0] == 'A' && weightOption[1] != 'A') { return db.results.where('weight').belowOrEqual(parseFloat(weightOption[1])).toArray(); }
        else if (weightOption[1] == 'A' && weightOption[0] != 'A') { return db.results.where('weight').aboveOrEqual(parseFloat(weightOption[0])).toArray(); }
        else { return db.results.where('weight').aboveOrEqual(parseFloat(weightOption[0])).and(value => value.weight <= parseFloat(weightOption[1])).toArray(); }
    }).then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
    }).then(function () {
        if (geneSymbOption == 'A') { return db.results.toArray(); }
        else { return db.results.where('gene_symbol').equals(geneSymbOption).toArray(); }
    }).then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
    }).then(function () {
        if (geneAccIdOption == 'A') { return db.results.toArray(); }
        else { return db.results.where('gene_accession_id').equals(geneAccIdOption).toArray(); }
    }).then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
    }).then(function () {
        if (zygosityOption == 'A') { return db.results.toArray(); }
        else { return db.results.where('zygosity').equals(zygosityOption).toArray(); }
    }).then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
    }).then(function () {
        if (parameterOption == 'A') { return db.results.toArray(); }
        else { return db.results.where('parameter_name').equals(parameterOption).toArray(); }
    }).then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
    }).then(function () {
        if (obvsTypeOption == 'A') { return db.results.toArray(); }
        else { return db.results.where('observation_type').equals(obvsTypeOption).toArray(); }
    }).then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
    }).then(function () {
        if (categoryOption == 'A') { return db.results.toArray(); }
        else { return db.results.where('category').equals(categoryOption).toArray(); }
    }).then(function (queriedArr) {
        db.results.clear();
        db.results.bulkAdd(queriedArr);
        console.log("Query success");
        showAppliedFilters(idOption, centerOption, dobOption, sexOption, ageOption, weightOption, geneSymbOption, geneAccIdOption, zygosityOption, parameterOption, obvsTypeOption, categoryOption);
    }).catch(function (err) {
        console.error(err.stack || err);
    });
};

function showAppliedFilters(idOption, centerOption, dobOption, sexOption, ageOption, weightOption, geneSymbOption, geneAccIdOption, zygosityOption, parameterOption, obvsTypeOption, categoryOption) {
    var ul = document.getElementById("filterList");
    ul.innerHTML = "";
    if (idOption == 'A' && centerOption == 'A' && dobOption == ['A', 'A'] && sexOption == 'A' && ageOption == ['A', 'A'] && weightOption == ['A', 'A'] && geneSymbOption == 'A' && geneAccIdOption == 'A' && zygosityOption == 'A' && parameterOption == 'A' && obvsTypeOption == 'A' && categoryOption == 'A') {
        var li = document.createElement("li");
        li.appendChild(document.createTextNode("None"));
        li.style = " background-color: crimson;";
        ul.appendChild(li);
    }   
    else {
        if (idOption != 'A') {
            var li = document.createElement("li");
            li.appendChild(document.createTextNode("Biological Sample ID: " + idOption));
            li.style = " background-color: darkmagenta;";
            ul.appendChild(li);
        }
        if (centerOption != 'A') {
            var li = document.createElement("li");
            li.appendChild(document.createTextNode("Phenotyping Center: " + centerOption));
            li.style = " background-color: seagreen;";
            ul.appendChild(li);
        }
        if (dobOption[0] != 'A' && dobOption[1] != 'A') {
            var li = document.createElement("li");
            li.appendChild(document.createTextNode("Date of Birth: " + dobOption[0] + " - " + dobOption[1]));
            li.style = " background-color: darkcyan;";
            ul.appendChild(li);
        }
        if (sexOption != 'A') {
            var li = document.createElement("li");
            li.appendChild(document.createTextNode("Sex: " + sexOption));
            li.style = " background-color: mediumvioletred;";
            ul.appendChild(li);
        }
        if (ageOption[0] != 'A' && ageOption[1] != 'A') {
            var li = document.createElement("li");
            li.appendChild(document.createTextNode("Age: " + ageOption[0] + " - " + ageOption[1]));
            li.style = " background-color: gray;";
            ul.appendChild(li);
        }
        if (weightOption[0] != 'A' && weightOption[1] != 'A') {
            var li = document.createElement("li");
            li.appendChild(document.createTextNode("Weight: " + weightOption[0] + " - " + weightOption[1] + "g"));
            li.style = " background-color: sienna;";
            ul.appendChild(li);
        }
        if (geneSymbOption != 'A') {
            var li = document.createElement("li");
            li.appendChild(document.createTextNode("Gene Symbol: " + geneSymbOption));
            li.style = " background-color: midnightblue;";
            ul.appendChild(li);
        }
        if (geneAccIdOption != 'A') {
            var li = document.createElement("li");
            li.appendChild(document.createTextNode("Gene Accession ID: " + geneAccIdOption));
            li.style = " background-color: slateblue;";
            ul.appendChild(li);
        }
        if (zygosityOption != 'A') {
            var li = document.createElement("li");
            li.appendChild(document.createTextNode("Zygosity: " + zygosityOption));
            li.style = " background-color: forestgreen;";
            ul.appendChild(li);
        }
        if (parameterOption != 'A') {
            var li = document.createElement("li");
            li.appendChild(document.createTextNode("Parameter Name: " + parameterOption));
            li.style = " background-color: darkkhaki;";
            ul.appendChild(li);
        }
        if (obvsTypeOption != 'A') {
            var li = document.createElement("li");
            li.appendChild(document.createTextNode("Observation Type: " + obvsTypeOption));
            li.style = " background-color: tomato;";
            ul.appendChild(li);
        }
        if (categoryOption != 'A') {
            var li = document.createElement("li");
            li.appendChild(document.createTextNode("Category: " + categoryOption));
            li.style = " background-color: indianred;";
            ul.appendChild(li);
        }
    }
}

function showFilters() {
    var x = document.getElementById("filters");
    if (x.style.display === "none") {
        x.style.display = "flex";
    } else {
        x.style.display = "none";
    }
}

function clearDivs() {
    //delete current divs
    var x = document.getElementById("images").querySelectorAll(".mouse");
    for (i = 0; i < x.length; i++) {
        x[i].remove();
        console.log("removed div", i);
    }
}

function generateThumbs() {
    return db.miceImagesResults.clear().then(function () {
        return db.results.toArray()
    }).then(function (arr) {
        for (var i = 0; i < arr.length; i++) {
            db.miceImages.where('biological_sample_id').equals(arr[i].biological_sample_id).toArray().then(function (arr) {
                db.miceImagesWorking.clear();
                db.miceImagesWorking.bulkAdd(arr);
            }).then(function () {
                return db.miceImagesWorking.where('parameter_name').equals("XRay Images Whole Body Dorso Ventral").toArray().length;
            }).then(function (foundResults) {
                if (foundResults != 0) {
                    console.log("good");
                    return db.miceImagesWorking.where('parameter_name').equals("XRay Images Whole Body Dorso Ventral").first();
                }
                else {
                    console.log("bad");
                    return db.miceImagesWorking.sortBy('parameter_name').reverse().first();
                }
            }).then(function (mouse) {
                console.log("mouse:",mouse);
                return db.miceImagesResults.put(mouse);
            }).catch(function (err) {
                console.error(err.stack || err);
            });
        }
    }).catch(function (err) {
        console.error(err.stack || err);
    });
}

//Takes results from QueryDB array and creates new ImageArray item with url string 
function loadDivs() {
    db.miceImagesResults.clear().then(function () {
        return db.results.toArray()
    }).then(function (arr) {
        var element = document.getElementById("resultsFound");
        element.innerHTML = (arr.length);

        for (var i = 0; i < arr.length; i++) {
            db.miceImages.where('biological_sample_id').equals(arr[i].biological_sample_id).toArray().then(function (arr) {
                db.miceImagesWorking.clear();
                db.miceImagesWorking.bulkAdd(arr);
            }).then(function () {
                return db.miceImagesWorking.where('parameter_name').equals("XRay Images Whole Body Dorso Ventral").toArray().length;
            }).then(function (foundResults) {
                if (foundResults != 0) {
                    return db.miceImagesWorking.where('parameter_name').equals("XRay Images Whole Body Dorso Ventral").first();
                }
                else {
                    return db.miceImagesWorking.sortBy('parameter_name').reverse().first();
                }
            }).then(function (mouse) {
                db.miceImagesResults.put(mouse);
                const mouseDiv = document.createElement("div");
                mouseDiv.id = "mouseDiv" + mouse.biological_sample_id;
                mouseDiv.className = "mouseDiv"
                mouseDiv.src = "https:" + mouse.jpeg_url;
                mouseDiv.onclick = function () { showModal(mouse.biological_sample_id) };
                //mouseDiv.style = "width:160px; height:160px; border: thin solid white; display: inline-block; overflow:hidden; text-align:center; margin:5px;";
                var currentDiv = document.getElementById("placeholderDiv");
                currentDiv.parentNode.insertBefore(mouseDiv, currentDiv);
                const mouseImg = document.createElement("img");
                mouseImg.className = "mouse"
                mouseImg.src = "https:" + mouse.jpeg_url;
                //mouseImg.style = "height:100%; object-fit:cover; flex-grow:1;";
                document.getElementById(mouseDiv.id).appendChild(mouseImg);
                const mouseId = document.createElement("p");
                mouseId.className = "mouseId"
                mouseId.innerText = mouse.biological_sample_id;
                document.getElementById(mouseDiv.id).appendChild(mouseId);
                return mouse;
            //}).then(function (mouse) {
              //  loadModal(mouse);
            });
            console.log("Image", i+1, "of", arr.length, "loaded.");
        }
    }).catch(function (err) {
        console.log("oops");
        console.error(err.stack || err);
    });
}

function loadModal(mouseId) {
    return db.miceData.where('biological_sample_id').equals(parseInt(mouseId)).first().then(function (mouse) {
        const mouseModal = document.createElement("div");
        mouseModal.className = "modal";
        mouseModal.id = "modal" + mouse.biological_sample_id;
        document.getElementById("modalContainer").appendChild(mouseModal)

        const modalContent = document.createElement("div");
        modalContent.className = "modalContent";
        modalContent.id = "modalContent" + mouse.biological_sample_id;
        document.getElementById(mouseModal.id).appendChild(modalContent);

        const closeSpan = document.createElement("span");
        closeSpan.className = "close";
        closeSpan.onclick = function () { closeModal() };
        closeSpan.innerHTML = "&times;";
        document.getElementById(modalContent.id).appendChild(closeSpan);

        const slideContainer = document.createElement("div");
        slideContainer.className = "slideContainer";
        slideContainer.id = "slideContainer" + mouse.biological_sample_id;
        document.getElementById(modalContent.id).appendChild(slideContainer);

        const slideThumbRow = document.createElement("div");
        slideThumbRow.id = "thumbRow" + mouse.biological_sample_id;
        slideThumbRow.className = "thumbRow";
        document.getElementById(slideContainer.id).appendChild(slideThumbRow);

        db.miceImages.where('biological_sample_id').equals(mouse.biological_sample_id).toArray().then(function (arr) {
            for (i = 0; i < arr.length; i++) {
                //const x = i;
                const mouseSlide = document.createElement("div");
                mouseSlide.id = "mouseSlide" + mouse.biological_sample_id + "-" + i;
                if (i == 0) { mouseSlide.className = "mouseSlide defaultSlide"; }
                else { mouseSlide.className = "mouseSlide"; }
                document.getElementById(slideContainer.id).appendChild(mouseSlide);

                const mouseSlideImg = document.createElement("img");
                mouseSlideImg.src = arr[i].jpeg_url;
                document.getElementById(mouseSlide.id).appendChild(mouseSlideImg);

                const mouseSlideThumb = document.createElement("div");
                mouseSlideThumb.id = "mouseSlideThumb" + mouse.biological_sample_id + "-" + i;
                mouseSlideThumb.className = "mouseSlideThumb";
                document.getElementById(slideThumbRow.id).appendChild(mouseSlideThumb);

                const mouseSlideThumbImg = document.createElement("img");
                mouseSlideThumbImg.src = arr[i].jpeg_url;
                mouseSlideThumbImg.className = "mouseSlideThumbImg"
                mouseSlideThumbImg.onclick = function () { currentSlide(mouseSlide.id) };
                document.getElementById(mouseSlideThumb.id).appendChild(mouseSlideThumbImg);
            }
        })

        const mouseInfo = document.createElement("div");
        mouseInfo.className = "mouseInfo";
        mouseInfo.id = "mouseInfo" + mouse.biological_sample_id;
        document.getElementById(modalContent.id).appendChild(mouseInfo);

        const mouseTitle = document.createElement("h3");
        mouseTitle.className = "mouseTitle";
        mouseTitle.textContent = "Mouse " + mouse.biological_sample_id;
        document.getElementById(mouseInfo.id).appendChild(mouseTitle);

        const mouseCenter = document.createElement("p");
        mouseCenter.textContent = "Phenotyping center: " + mouse.phenotyping_center;
        document.getElementById(mouseInfo.id).appendChild(mouseCenter);

        const mouseDob = document.createElement("p");
        mouseDob.textContent = "Date of birth: " + mouse.date_of_birth;
        document.getElementById(mouseInfo.id).appendChild(mouseDob);

        const mouseSex = document.createElement("p");
        mouseSex.textContent = "Sex: " + mouse.sex;
        document.getElementById(mouseInfo.id).appendChild(mouseSex);

        const mouseAge = document.createElement("p");
        mouseAge.textContent = "Age : " + mouse.age_in_weeks + " weeks";
        document.getElementById(mouseInfo.id).appendChild(mouseAge);

        const mouseWeight = document.createElement("p");
        mouseWeight.textContent = "Weight : " + mouse.weight + "g";
        document.getElementById(mouseInfo.id).appendChild(mouseWeight);

        const mouseGeneSymb = document.createElement("p");
        mouseGeneSymb.textContent = "Gene symbol: " + mouse.gene_symbol;
        document.getElementById(mouseInfo.id).appendChild(mouseGeneSymb);

        const mouseGeneAccId = document.createElement("p");
        mouseGeneAccId.textContent = "Gene accession ID: " + mouse.gene_accession_id;
        document.getElementById(mouseInfo.id).appendChild(mouseGeneAccId);

        const mouseZygosity = document.createElement("p");
        mouseZygosity.textContent = "Zygosity: " + mouse.zygosity;
        document.getElementById(mouseInfo.id).appendChild(mouseZygosity);

        const mouseParamName = document.createElement("p");
        mouseParamName.textContent = "Parameter name: " + mouse.parameter_name;
        document.getElementById(mouseInfo.id).appendChild(mouseParamName);

        const mouseObvsType = document.createElement("p");
        mouseObvsType.textContent = "Observation type: " + mouse.observation_type;
        document.getElementById(mouseInfo.id).appendChild(mouseObvsType);

        const mouseCategory = document.createElement("p");
        mouseCategory.textContent = "Category: " + mouse.category;
        document.getElementById(mouseInfo.id).appendChild(mouseCategory);

        return mouse;
    }).catch(function (err) {
        console.log("oops");
        console.error(err.stack || err);
    });

}

function clearModal() {
    document.getElementById("modalContainer").innerHTML = '';
}

//modal js

function showSlides(id) {
    var i;
    var modalSlide = document.getElementById(modId).querySelectorAll(".mouseSlide"); //get slides in open modal

    for (i = 0; i < modalSlide.length; i++) {
        modalSlide[i].style.display = "none"; //hide all slides
    }
    document.getElementById(id).style.display = "block"; //show current slide
}

function currentSlide(id) {
    showSlides(id);
}

function showModal(mouseId) {
    loadModal(mouseId).then(function () {
        modId = "modal"+mouseId
        window.modId = modId;
        window.modal = document.getElementById(modId);
        window.span = document.getElementsByClassName("close")[0];
        modal.style.display = "flex"; //display selected modal
    }).catch(function (err) {
        console.log("oops");
        console.error(err.stack || err);
    });
}

function closeModal() {
    modal.style.display = "none"; //close modal when close button pressed
    clearModal();
}

window.onclick = function (event) {
    if (event.target == modal) {
        closeModal(); //close modal when pressed outside of the modal
    }
}

function populateOptions() {
    return db.miceData.orderBy('phenotyping_center').uniqueKeys().then(function (uniqueKeys) {
        populateOptionBox("optionCenter", uniqueKeys, false);
    }).then(function () {
        return db.miceData.orderBy('date_of_birth').uniqueKeys();
    }).then(function (uniqueKeys) {
        populateOptionBox("optionMinDob", uniqueKeys, true);
        populateOptionBox("optionMaxDob", uniqueKeys, true);
    }).then(function () {
        return db.miceData.orderBy('sex').uniqueKeys();
    }).then(function (uniqueKeys) {
        populateOptionBox("optionSex", uniqueKeys, false);
    }).then(function () {
        return db.miceData.orderBy('age_in_weeks').uniqueKeys();
    }).then(function (uniqueKeys) {
        populateOptionBox("optionMinAge", uniqueKeys, false);
        populateOptionBox("optionMaxAge", uniqueKeys, false);
    }).then(function () {
        return db.miceData.orderBy('weight').uniqueKeys();
    }).then(function (uniqueKeys) {
        populateOptionBox("optionMinWeight", uniqueKeys, false);
        populateOptionBox("optionMaxWeight", uniqueKeys, false);
    }).then(function () {
        return db.miceData.orderBy('gene_symbol').uniqueKeys();
    }).then(function (uniqueKeys) {
        populateOptionBox("optionGeneSymb", uniqueKeys, false);
    }).then(function () {
        return db.miceData.orderBy('gene_accession_id').uniqueKeys();
    }).then(function (uniqueKeys) {
        populateOptionBox("optionGeneAccId", uniqueKeys, false);
    }).then(function () {
        return db.miceData.orderBy('zygosity').uniqueKeys();
    }).then(function (uniqueKeys) {
        populateOptionBox("optionZygosity", uniqueKeys, false);
    }).then(function () {
        return db.miceData.orderBy('parameter_name').uniqueKeys();
    }).then(function (uniqueKeys) {
        populateOptionBox("optionParameter", uniqueKeys, false);
    }).then(function () {
        return db.miceData.orderBy('observation_type').uniqueKeys();
    }).then(function (uniqueKeys) {
        populateOptionBox("optionObvsType", uniqueKeys, false);
    }).then(function () {
        return db.miceData.orderBy('category').uniqueKeys();
    }).then(function (uniqueKeys) {
        populateOptionBox("optionCategory", uniqueKeys, false);
    }).catch(function (err) {
        console.error(err.stack || err);
    });
}
//biological_sample_id,phenotyping_center,date_of_birth,sex,age_in_weeks,weight,gene_symbol,gene_accession_id,zygosity,parameter_name,observation_type,category


function populateOptionBox(optionId, optionsArr, isDate) {
    var opt = document.getElementById(optionId);
    if (isDate == false) {
        for (var i = 0; i < optionsArr.length; i++) {
            var option = document.createElement("option");
            option.value = optionsArr[i];
            var optionText = document.createTextNode(optionsArr[i]);
            option.appendChild(optionText);
            opt.appendChild(option);
        }
    }
    else {
        for (var i = 0; i < optionsArr.length; i++) {
            var option = document.createElement("option");
            option.value = optionsArr[i];
            var optionText = document.createTextNode(optionsArr[i].substring(0, optionsArr[i].length - 10));
            option.appendChild(optionText);
            opt.appendChild(option);
        }
    }
}

function onLoadRun() {
    loadData()
        .then(populateOptions)
        .then(clearDivs)
        .then(loadDivs)
        .then(function () {
            console.log("Page Loaded :)");
        }).catch(function (err) {
            console.error(err.stack || err);
        });
}