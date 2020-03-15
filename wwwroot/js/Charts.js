//Script for each chart on CHARTS page of the webapp


//Global options for all charts

Chart.defaults.global.defaultFontFamily = 'lato';
Chart.defaults.global.defaultFontSize = 15;
Chart.defaults.global.defaultFontColor = '#777';

//Age of Mice Chart

let AgeOfMice = document.getElementById('AgeOfMice').getContext('2d');

    let AgeOfMiceChart = new Chart(AgeOfMice, {
        type: 'bar',//bar, horizontalBar,pie,line,doughnut,radar,polarArea
        data: {
            labels: ['Mouse 1', 'Mouse 2', 'Mouse 3', 'Mouse 4', 'Mouse 5', 'mouse 6'],
            datasets: [{
                label: 'Group 1',
                data: [
                    14,
                    23,
                    3,
                    21,
                    11,
                    5
                ],
                //backgroundColor:'green'
                backgroundColor: [
                    'rgba(255,99,132,0.6)',
                    'rgba(54,162,235,0.6)',
                    'rgba(255,206,86,0.6)',
                    'rgba(75,192,192,0.6)',
                    'rgba(153,102,255,0.6)',
                    'rgba(255,159,64,0.6)',
                    'rgba(255,99,132,0.6)',

                ],
                borderWidth: 1,
                borderColor: '#777',
                hoverBorderWidth: 3,
                hoverBorderColor: '#000'
            }]
        },
        options: {
            title: {
                display: true,
                text: 'Age of Mice',
                fontSize: 20
            },
            legend: {
                display: false,
                position: 'right', // top, bottom, left, right
                labels: {
                    fontColor: '#000'
                }
            },
            layout: {
                padding: {
                    left: 0,
                    right: 0,
                    top: 0,
                    bottom: 0
                }
            },
            tooltips: {
                enabled: true
            },
            scales: {
                yAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: 'Age (Weeks)'
                    }
                }],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: 'Mouse ID'
                    }
                }]
            }
        }
    });

// Centers Chart

let Centers = document.getElementById('Centers').getContext('2d');

//Global options

let CentersChart = new Chart(Centers, {
    type: 'pie',//bar, horizontalBar,pie,line,doughnut,radar,polarArea
    data: {
        labels: ['University of California Davis', 'University of Cincinnati Medical Center', 'University of Massachusetts', 'University of Michigan', 'Vanderbilt University School of Medicine', 'Alumni MMPCs'],
        datasets: [{
            label: 'Group 1',
            data: [
                142,
                232,
                30,
                215,
                111,
                50
            ],
            //backgroundColor:'green'
            backgroundColor: [
                'rgba(255,99,132,0.6)',
                'rgba(54,162,235,0.6)',
                'rgba(255,206,86,0.6)',
                'rgba(75,192,192,0.6)',
                'rgba(153,102,255,0.6)',
                'rgba(255,159,64,0.6)',
                'rgba(255,99,132,0.6)',

            ],
            borderWidth: 1,
            borderColor: '#777',
            hoverBorderWidth: 3,
            hoverBorderColor: '#000'
        }]
    },
    options: {
        title: {
            display: true,
            text: 'Phenotyping Centers',
            fontSize: 20
        },
        legend: {
            display: true,
            position: 'right', // top, bottom, left, right
            labels: {
                fontColor: '#000'
            }
        },
        layout: {
            padding: {
                left: 0,
                right: 0,
                top: 0,
                bottom: 0
            }
        },
        tooltips: {
            enabled: true
        },

    }
});

// Distribution of mice by age charts

let DistMiceByAge = document.getElementById('DistMiceByAge').getContext('2d');

//Global options

let DistMiceByAgeChart = new Chart(DistMiceByAge, {
    type: 'bar',//bar, horizontalBar,pie,line,doughnut,radar,polarArea
    data: {
        labels: ['1-20', '21-40', '41-60', '61 -80', '81-100', '100-121'],
        datasets: [{
            label: 'Male',
            data: [
                14,
                56,
                70,
                65,
                50,
                32
            ],
            backgroundColor: [
                'rgba(255,99,132,0.6)',
                'rgba(54,162,235,0.6)',
                'rgba(255,206,86,0.6)',
                'rgba(75,192,192,0.6)',
                'rgba(153,102,255,0.6)',
                'rgba(255,159,64,0.6)',
                'rgba(255,99,132,0.6)',
            ],
            borderWidth: 1,
            borderColor: '#777',
            hoverBorderWidth: 3,
            hoverBorderColor: '#000'
        }
        ]
    },
    options: {
        title: {
            display: true,
            text: 'Distribution of Mice by Age ',
            fontSize: 20
        },
        legend: {
            display: false,
            position: 'right', // top, bottom, left, right
            labels: {
                fontColor: '#000'
            }
        },
        layout: {
            padding: {
                left: 0,
                right: 0,
                top: 0,
                bottom: 0
            }
        },
        tooltips: {
            enabled: true
        },
        scales: {
            yAxes: [{
                scaleLabel: {
                    display: true,
                    labelString: 'Number of Mice'
                }
            }],
            xAxes: [{
                scaleLabel: {
                    display: true,
                    labelString: 'Age (Weeks)'
                }
            }]
        }
    }
});

// Distribution of male to female mice

let MFDist = document.getElementById('MFDist').getContext('2d');

//Global options

let MFDistChart = new Chart(MFDist, {
    type: 'bar',//bar, horizontalBar,pie,line,doughnut,radar,polarArea
    data: {
        labels: ['University of California Davis', 'University of Cincinnati Medical Center', 'University of Massachusetts', 'University of Michigan', 'Vanderbilt University School of Medicine', 'Alumni MMPCs'],
        datasets: [{
            label: 'Male',
            data: [
                142,
                232,
                70,
                215,
                111,
                50
            ],
            backgroundColor: 'blue',
            borderWidth: 1,
            borderColor: '#777',
            hoverBorderWidth: 3,
            hoverBorderColor: '#000'
        },
        {
            label: 'Female',
            data: [
                101,
                245,
                41,
                99,
                214,
                301
            ],
            backgroundColor: 'red',
            borderWidth: 1,
            borderColor: '#777',
            hoverBorderWidth: 3,
            hoverBorderColor: '#000'
        }
        ]
    },
    options: {
        title: {
            display: true,
            text: 'Distribution of Male to Female Mice ',
            fontSize: 20
        },
        legend: {
            display: true,
            position: 'right', // top, bottom, left, right
            labels: {
                fontColor: '#000'
            }
        },
        layout: {
            padding: {
                left: 0,
                right: 0,
                top: 0,
                bottom: 0
            }
        },
        tooltips: {
            enabled: true
        },

    }
});

// Distribution of genes knockouts

let genes = document.getElementById('genes').getContext('2d');

//Global options

let genesChart = new Chart(genes, {
    type: 'bar',//bar, horizontalBar,pie,line,doughnut,radar,polarArea
    data: {
        labels: ['1-20', '21-40', '41-60', '61 -80', '81-100', '100-121'],
        datasets: [{
            type: 'line',
            label: 'weight (g)',
            borderColor: 'blue',
            borderWidth: 2,
            fill: false,
            //  yAxisID: 'y-axis-1',
            data: [
                20,
                30,
                40,
                43,
                37,
                29,
            ],
            // yAxisID: "y-axis-1",
        },
        {
            label: 'Gene Symbol : Rab15',

            data: [
                14,
                56,
                70,
                65,
                50,
                32
            ],
            //  yAxisID: "y-axis-2",
            backgroundColor: [
                'rgba(255,99,132,0.6)',
                'rgba(54,162,235,0.6)',
                'rgba(255,206,86,0.6)',
                'rgba(75,192,192,0.6)',
                'rgba(153,102,255,0.6)',
                'rgba(255,159,64,0.6)',
                'rgba(255,99,132,0.6)',
            ],
            borderWidth: 1,
            borderColor: '#777',
            hoverBorderWidth: 3,
            hoverBorderColor: '#000'
        }
        ]
    },
    options: {
        title: {
            display: true,
            text: 'Distribution of Abnormalities Across Mice of Different Ages ',
            fontSize: 20
        },
        legend: {
            display: true,
            position: 'right', // top, bottom, left, right
            labels: {
                fontColor: '#000',
                filter: function (item, chart) {
                    return !item.text.includes('Gene Symbol : Rab15');
                }
            }
        },
        layout: {
            padding: {
                left: 0,
                right: 0,
                top: 0,
                bottom: 0
            }
        },
        tooltips: {
            enabled: true
        },
        scales: {
            yAxes: [{
                scaleLabel: {
                    display: true,
                    labelString: 'Number of Mice'
                }
            }],
            xAxes: [{
                scaleLabel: {
                    display: true,
                    labelString: 'Age (Weeks)'
                }
            }]
        }
    }
});