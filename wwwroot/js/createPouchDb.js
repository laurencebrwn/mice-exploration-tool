var db = new PouchDB("mice_database");
db.bulkDocs([
    {
        _id: 'AA123456789',
        patient_sex: 'F',
        patient_gene:'Rab15',
        parameter_name:'body',
        phenotyping_center:'WTSI',
        urlString: 'dicomImages/mouse1.dcm'
    }
])

