var db = new Dexie('database');
var miceTestData = [{
    patient_id: 'AA123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'body',
    phenotyping_center: 'WTSI',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse1.dcm'
}, {
    patient_id: 'BB123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'head',
    phenotyping_center: 'TCP',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse2.dcm'
}, {
    patient_id: 'CC123456789',
    patient_sex: 'M',
    patient_gene: 'Rab15',
    parameter_name: 'body',
    phenotyping_center: 'ICS',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse3.dcm'
}, {
    patient_id: 'DD123456789',
    patient_sex: 'M',
    patient_gene: 'Rab15',
    parameter_name: 'body',
    phenotyping_center: 'ICS',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse4.dcm'
}, {
    patient_id: 'EE123456789',
    patient_sex: 'M',
    patient_gene: 'Rab15',
    parameter_name: 'paw',
    phenotyping_center: 'TCP',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse5.dcm'
}, {
    patient_id: 'FF123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'paw',
    phenotyping_center: 'WTSI',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse6.dcm'
}, {
    patient_id: 'GG123456789',
    patient_sex: 'F',
    patient_gene: 'Rab18',
    parameter_name: 'body',
    phenotyping_center: 'ICS',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse7.dcm'
}, {
    patient_id: 'HH123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'head',
    phenotyping_center: 'WTSI',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse8.dcm'
}, {
    patient_id: 'JJ123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'head',
    phenotyping_center: 'TCP',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse9.dcm'
}, {
    patient_id: 'KK123456789',
    patient_sex: 'M',
    patient_gene: 'Rab15',
    parameter_name: 'body',
    phenotyping_center: 'ICS',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse10.dcm'
}, {
    patient_id: 'LL123456789',
    patient_sex: 'M',
    patient_gene: 'Rab15',
    parameter_name: 'body',
    phenotyping_center: 'TCP',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse11.dcm'
}, {
    patient_id: 'MM123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'body',
    phenotyping_center: 'TCP',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse12.dcm'
}, {
    patient_id: 'NN123456789',
    patient_sex: 'F',
    patient_gene: 'Rab15',
    parameter_name: 'paw',
    phenotyping_center: 'WTSI',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse13.dcm'
}, {
    patient_id: 'PP123456789',
    patient_sex: 'F',
    patient_gene: 'Rab18',
    parameter_name: 'paw',
    phenotyping_center: 'ICS',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse14.dcm'
}, {
    patient_id: 'RR123456789',
    patient_sex: 'F',
    patient_gene: 'Rab18',
    parameter_name: 'body',
    phenotyping_center: 'ICS',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse15.dcm'
}, {
    patient_id: 'SS123456789',
    patient_sex: 'F',
    patient_gene: 'Rab18',
    parameter_name: 'head',
    phenotyping_center: 'WTSI',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse16.dcm'
    }];
var urlTestData = [{
    patient_id: 'AA123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse1.dcm'
}, {
    patient_id: 'BB123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse2.dcm'
}, {
    patient_id: 'CC123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse3.dcm'
}, {
    patient_id: 'DD123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse4.dcm'
}, {
    patient_id: 'EE123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse5.dcm'
}, {
    patient_id: 'FF123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse6.dcm'
}, {
    patient_id: 'GG123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse7.dcm'
}, {
    patient_id: 'HH123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse8.dcm'
}, {
    patient_id: 'JJ123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse9.dcm'
}, {
    patient_id: 'KK123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse10.dcm'
}, {
    patient_id: 'LL123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse11.dcm'
}, {
    patient_id: 'MM123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse12.dcm'
}, {
    patient_id: 'NN123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse13.dcm'
}, {
    patient_id: 'PP123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse14.dcm'
}, {
    patient_id: 'RR123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse15.dcm'
}, {
    patient_id: 'SS123456789',
    urlString: 'https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse16.dcm'
}];

function createDb() {
    return db.version(1).stores({
        mice: "patient_id,patient_sex,patient_gene,parameter_name,phenotyping_center,urlString",
        url: "patient_id, urlString",
        results: "patient_id,patient_sex,patient_gene,parameter_name,phenotyping_center,urlString"
    });
};

function loadTestData() {
    loadJsonArray(miceTestData, urlTestData);
};

function loadJsonArray(miceArr,urlArr) {
    return db.open().then(function () {
        return db.mice.bulkAdd(miceArr);
    }).then(function () {
        return db.url.bulkAdd(urlArr);
    }).catch(function (err) {
        console.error(err.stack || err);
    });
};

function queryDb(id_q,sex_q,gene_q,parameter_q,center_q) {
    return db.open().then(function () {
        console.log('patient query in process');
        if (id_q == 'A') { return db.mice.toArray(); }
        else { return db.mice.where('patient_id').equals(id_q).toArray();}
    }).then(function (queriedArr) {
        return db.open().then(function () { db.results.clear(); return db.results.bulkAdd(queriedArr); })
    }).then(function () {
        console.log('sex query in process');
        if (sex_q == 'A') { return db.results.toArray(); }
        else { return db.results.where('patient_sex').equals(sex_q).toArray(); }
    }).then(function (queriedArr) {
        return db.open().then(function () { db.results.clear(); return db.results.bulkAdd(queriedArr); })
    }).then(function () {
        console.log('gene query in process');
        if (gene_q == 'A') { return db.results.toArray(); }
        else { return db.results.where('patient_gene').equals(gene_q).toArray(); }
    }).then(function (queriedArr) {
        return db.open().then(function () { db.results.clear(); return db.results.bulkAdd(queriedArr); })
    }).then(function () {
        console.log('parameter query in process');
        if (parameter_q == 'A') { return db.results.toArray(); }
        else { return db.results.where('parameter_name').equals(parameter_q).toArray(); }
    }).then(function (queriedArr) {
        return db.open().then(function () { db.results.clear(); return db.results.bulkAdd(queriedArr); })
    }).then(function () {
        console.log('center query in process');
        if (center_q == 'A') { return db.results.toArray(); }
        else { return db.results.where('phenotyping_center').equals(center_q).toArray(); }
    }).then(function (queriedArr) {
        return db.open().then(function () { db.results.clear(); return db.results.bulkAdd(queriedArr); })
    })/*.then(function () {
        return loadDivs();
    })*/.catch(function (err) {
        console.error(err.stack || err);
    });
};

function loadDivs() {
    db.open().then(function () {
        return db.results.toArray();
    }).then(function (arr) {
        var ImageArray = [];
        for (var i = 0; i < arr.length; i++) {
            ImageArray.push(arr[i].urlString);
        };

        cornerstoneWADOImageLoader.external.cornerstone = cornerstone;

        //Forloop through image set and appends image to end of previous image

        for (var i = 0; i < ImageArray.length; i++) {
            console.log("Images", i + 1, "of", ImageArray.length, "loaded");
            var imageNum = ImageArray[i];
            let imageId = "wadouri:" + imageNum;

            //Create new div element for each DICOM image and insert it befor the dicomImage div in body

            const imageDiv = document.createElement("div");
            imageDiv.style = "width:20vw; height:20vw; border: thin solid white; display:inline-block";
            var currentDiv = document.getElementById("dicomImage");
            currentDiv.parentNode.insertBefore(imageDiv, currentDiv);
            cornerstone.enable(imageDiv);

            //Load the DICOM image and allow supoprt tools for image zoom and contrast etc

            cornerstone.loadImage(imageId).then(function (image) {
                cornerstone.displayImage(imageDiv, image);
                cornerstoneTools.mouseInput.enable(imageDiv);
                cornerstoneTools.mouseWheelInput.enable(imageDiv);
                cornerstoneTools.wwwc.activate(imageDiv, 1); // wwwc is the default tool for left mouse button
                cornerstoneTools.pan.activate(imageDiv, 2); // pan is the default tool for middle mouse button
                cornerstoneTools.zoom.activate(imageDiv, 4); // zoom is the default tool for right mouse button
                cornerstoneTools.zoomWheel.activate(imageDiv); // zoom is the default tool for middle mouse wheel
                //cornerstoneTools.imageStats.enable(imageElement);
                console.log("Images", i, "of", ImageArray.length, "loaded");
            });
        }
    }).catch(function (err) {
        console.log("oops");
        console.error(err.stack || err);
    });
}