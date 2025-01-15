//create chart variable and assign object canvas
const chart = document.getElementById('myChart');
//jsondata variable to get response
let JSONdata;
//myChart variable to assign new Chart object to set up chart
let myChart;

$(document).ready(function () {
    $('#btn_bar').click(function () {
        $.ajax({
            url: 'admin-chart',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                JSONdata = response;
                createChart(JSONdata, 'bar');
            }
        });
    });
});

//this function is used for creating Chart
function createChart(data, type) {
    let myChart = new Chart(chart, {
        type: type,
        data: {
            // Creating an array from the `name` from the json data
            // using the `map` method and assign it to the labels
            // property.
            labels: data.map(row => row.name),

            datasets: [{
                //note for information of each bar
                label: 'Test',

                // Creating an array from the `height` from the json data
                // using the `map` method and assign it to the data
                // property.
                data: data.map(row => parseFloat(row.height)),
                // width property of each bar
                borderWidth: 2
            }]
        },
        options: {
            scales: {
                y: {
                    //scale at begin position
                    beginAtZero: true
                }
            },
            // Making the chart responsive.
            responsive: true,
            maintainAspectRatio: false,
        }
    });
}
