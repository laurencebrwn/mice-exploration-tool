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

var miceTestData = [{
    patient_id: 'AA123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'body',
    phenotyping_center: 'WTSI',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/AA123456789.dcm'
}, {
    patient_id: 'BB123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'head',
    phenotyping_center: 'TCP',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/BB123456789.dcm'
}, {
    patient_id: 'CC123456789',
    patient_sex: 'M',
    patient_gene: 'Rab15',
    parameter_name: 'body',
    phenotyping_center: 'ICS',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/CC123456789.dcm'
}, {
    patient_id: 'DD123456789',
    patient_sex: 'M',
    patient_gene: 'Rab15',
    parameter_name: 'body',
    phenotyping_center: 'ICS',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/DD123456789.dcm'
}, {
    patient_id: 'EE123456789',
    patient_sex: 'M',
    patient_gene: 'Rab15',
    parameter_name: 'paw',
    phenotyping_center: 'TCP',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/EE123456789.dcm'
}, {
    patient_id: 'FF123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'paw',
    phenotyping_center: 'WTSI',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/FF123456789.dcm'
}, {
    patient_id: 'GG123456789',
    patient_sex: 'F',
    patient_gene: 'Rab18',
    parameter_name: 'body',
    phenotyping_center: 'ICS',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/GG123456789.dcm'
}, {
    patient_id: 'HH123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'head',
    phenotyping_center: 'WTSI',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/HH123456789.dcm'
}, {
    patient_id: 'JJ123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'head',
    phenotyping_center: 'TCP',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/JJ123456789.dcm'
}, {
    patient_id: 'KK123456789',
    patient_sex: 'M',
    patient_gene: 'Rab15',
    parameter_name: 'body',
    phenotyping_center: 'ICS',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/KK123456789.dcm'
}, {
    patient_id: 'LL123456789',
    patient_sex: 'M',
    patient_gene: 'Rab15',
    parameter_name: 'body',
    phenotyping_center: 'TCP',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/LL123456789.dcm'
}, {
    patient_id: 'MM123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'body',
    phenotyping_center: 'TCP',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/MM123456789.dcm'
}, {
    patient_id: 'NN123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'paw',
    phenotyping_center: 'WTSI',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/NN123456789.dcm'
}, {
    patient_id: 'PP123456789',
    patient_sex: 'F',
    patient_gene: 'Rab18',
    parameter_name: 'paw',
    phenotyping_center: 'ICS',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/PP123456789.dcm'
}, {
    patient_id: 'RR123456789',
    patient_sex: 'F',
    patient_gene: 'Rab18',
    parameter_name: 'body',
    phenotyping_center: 'ICS',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/RR123456789.dcm'
}, {
    patient_id: 'SS123456789',
    patient_sex: 'F',
    patient_gene: 'Rab18',
    parameter_name: 'head',
    phenotyping_center: 'WTSI',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/SS123456789.dcm'
}];

//adds json data into dexieDB
function loadData() {
    var jsonMice = {};
    var jsonMiceImages = {};
    var jsonMiceData = {};
    return db.mice.count(function (count) {
        if (count > 0) {
            console.log("Already populated");
        } else {
            console.log("Populating tables");
            return fetch('https://www.ebi.ac.uk/mi/impc/solr/impc_images/select?q=(biological_sample_id:[*%20TO%20*]%20AND%20parameter_name:%22XRay%20Images%20Whole%20Body%20Dorso%20Ventral%22)&fl=biological_sample_id&rows=40').then(
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
                return fetch(urlStr)
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
                return fetch(urlStr);
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
                mouseDiv.src = "https:" + mouse.jpeg_url;
                mouseDiv.style = "width:160px; height:160px; border: thin solid white; display: inline-block; overflow:hidden; text-align:center; margin:5px;";
                var currentDiv = document.getElementById("placeholderDiv");
                currentDiv.parentNode.insertBefore(mouseDiv, currentDiv);
                const mouseImg = document.createElement("img");
                mouseImg.className = "mouse"
                mouseImg.src = "https:" + mouse.jpeg_url;
                mouseImg.style = "height:100%; object-fit:cover; flex-grow:1;";
                document.getElementById(mouseDiv.id).appendChild(mouseImg);
            });
            console.log("Image", i+1, "of", arr.length, "loaded.");
        }
    }).catch(function (err) {
        console.log("oops");
        console.error(err.stack || err);
    });
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