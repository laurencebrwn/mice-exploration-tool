var db = new PouchDB("mice_database");
db.bulkDocs([
    {
        _id: 'AA123456789',
        patient_sex: 'F',
        patient_gene:'Rab15',
        parameter_name:'body',
        phenotyping_center:'WTSI',
        urlString: 'dicomImages/mouse1.dcm'
    },
    {
        _id: 'BB123456789',
        patient_sex: 'F',
        patient_gene: 'Rab15',
        parameter_name: 'head',
        phenotyping_center: 'TCP',
        urlString: 'dicomImages/mouse2.dcm'
    },
    {
        _id: 'CC123456789',
        patient_sex: 'M',
        patient_gene: 'Rab15',
        parameter_name: 'body',
        phenotyping_center: 'ICS',
        urlString: 'dicomImages/mouse3.dcm'
    },
    {
        _id: 'DD123456789',
        patient_sex: 'M',
        patient_gene: 'Rab15',
        parameter_name: 'body',
        phenotyping_center: 'ICS',
        urlString: 'dicomImages/mouse4.dcm'
    },
    {
        _id: 'EE123456789',
        patient_sex: 'M',
        patient_gene: 'Rab15',
        parameter_name: 'paw',
        phenotyping_center: 'TCP',
        urlString: 'dicomImages/mouse5.dcm'
    },
    {
        _id: 'FF123456789',
        patient_sex: 'F',
        patient_gene: 'Rab15',
        parameter_name: 'paw',
        phenotyping_center: 'WTSI',
        urlString: 'dicomImages/mouse6.dcm'
    },
    {
        _id: 'GG123456789',
        patient_sex: 'F',
        patient_gene: 'Rab18',
        parameter_name: 'body',
        phenotyping_center: 'ICS',
        urlString: 'dicomImages/mouse7.dcm'
    },
    {
        _id: 'HH123456789',
        patient_sex: 'F',
        patient_gene: 'Rab15',
        parameter_name: 'head',
        phenotyping_center: 'WTSI',
        urlString: 'dicomImages/mouse8.dcm'
    },
    {
        _id: 'JJ123456789',
        patient_sex: 'F',
        patient_gene: 'Rab15',
        parameter_name: 'head',
        phenotyping_center: 'TCP',
        urlString: 'dicomImages/mouse9.dcm'
    },
    {
        _id: 'KK123456789',
        patient_sex: 'M',
        patient_gene: 'Rab15',
        parameter_name: 'body',
        phenotyping_center: 'ICS',
        urlString: 'dicomImages/mouse10.dcm'
    },
    {
        _id: 'LL123456789',
        patient_sex: 'M',
        patient_gene: 'Rab15',
        parameter_name: 'body',
        phenotyping_center: 'TCP',
        urlString: 'dicomImages/mouse11.dcm'
    },
    {
        _id: 'MM123456789',
        patient_sex: 'F',
        patient_gene: 'Rab15',
        parameter_name: 'body',
        phenotyping_center: 'TCP',
        urlString: 'dicomImages/mouse12.dcm'
    },
    {
        _id: 'NN123456789',
        patient_sex: 'F',
        patient_gene: 'Rab15',
        parameter_name: 'paw',
        phenotyping_center: 'WTSI',
        urlString: 'dicomImages/mouse13.dcm'
    },
    {
        _id: 'PP123456789',
        patient_sex: 'F',
        patient_gene: 'Rab18',
        parameter_name: 'paw',
        phenotyping_center: 'ICS',
        urlString: 'dicomImages/mouse14.dcm'
    },
    {
        _id: 'RR123456789',
        patient_sex: 'F',
        patient_gene: 'Rab18',
        parameter_name: 'body',
        phenotyping_center: 'ICS',
        urlString: 'dicomImages/mouse15.dcm'
    },
    {
        _id: 'SS123456789',
        patient_sex: 'F',
        patient_gene: 'Rab18',
        parameter_name: 'head',
        phenotyping_center: 'WTSI',
        urlString: 'dicomImages/mouse16.dcm'
    }
])

