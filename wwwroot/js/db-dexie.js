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
        console.log('query in process');

        return db.mice
            .where('urlString')
            .noneOf([])
            .and(value => function (value) { console.log('inside query'); if (id_q == 'A') { return value.patient_id !== ''; } else { return value.patient_id == id_q; }})
            /*.and(value => function (value) { console.log('we sex  ', sex_q); if (sex_q == 'A') { return value.patient_sex !== ''; } else { console.log('we here'); return value.patient_sex == sex_q; } })
            .and(value => function (value) { if (gene_q == 'A') { return value.patient_gene !== ''; } else { return value.patient_gene == gene_q; } })
            .and(value => function (value) { if (parameter_q == 'A') { return value.parameter_name !== '' ; } else { return value.parameter_name == parameter_q; } })
            .and(value => function (value) { if (center_q == 'A') { return value.phenotyping_center !== ''; } else { return value.phenotyping_center == center_q; } })
            */.toArray();
    }).then(function (queriedArr) {
        return db.open().then(function () {
            db.results.clear();
            return db.results.bulkAdd(queriedArr);
        })
    }).catch(function (err) {
        console.error(err.stack || err);
    });
};