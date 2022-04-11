const separateNumber = (str) => String(str).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
const emptyFormatter = (str) => { };
function FillData(production, chart, qty) {
    for (var i = 0; i < production.length; i++) {
        chart.data.datasets[0].data[i] = production[i];

        chart.data.datasets[0].backgroundColor[i] = (production[i + 1] == 0 || production[i + 1] == null) ? 'rgba(255, 255, 255, 1)' : '#23a84c';

        chart.options.plugins.datalabels.backgroundColor[i] = 'rgba(255, 255, 255, 1)';
        chart.options.plugins.datalabels.color[i] = 'darkgreen';
    }
    chart.options.plugins.datalabels.formatter = (str) => str != null ? separateNumber(str) : emptyFormatter(str);
    chart.update();
    filteredProd = production.filter(x => x != null)
    if (filteredProd.length > 0) {
        intProd = filteredProd.map(x => parseInt(x))
        if (Array.isArray(intProd))
            qty.text(separateNumber(intProd.reduce((a, b) => a + b)));
        else
            qty.text(separateNumber(intProd))
    }
    else
        qty.text(0)
}

// SetUP, render and fill charts
function Render(context, labels, fontsize) {
    let pageWidth = $(window).width();
    if (pageWidth <= 991) {
        _chartType = 'horizontalBar';
        _xAxisEnabled = false;
        _yAxisEnabled = true;
        labelSize = 40;
        middleLabelSize = 40;
        tickSize = 15;
    }
    else {
        _chartType = 'bar';
        _xAxisEnabled = true;
        _yAxisEnabled = false;
        labelSize = 25;
        middleLabelSize = pageWidth / 55;
        tickSize = pageWidth / 100;
    }

    return CreateChart(context, _chartType, labels, fontsize)
};

// Creating bar chart
function CreateChart(context, chartType, chartLabels, fontsize) {
    let chartConfig =
    {
        labels: chartLabels,
        datasets: [{ backgroundColor: [], borderWidth: 0, pointColor: "rgba(151,187,205,1)", data: [] }]
    }

    let chartOptions =
    {
        cornerRadius: 15,
        responsive: true,
        maintainAspectRatio: false,
        aspectRatio: 1,
        borderWidth: 0,
        layout: { padding: { left: 0, right: 0, top: 5, bottom: 0 } },
        scales:
        {
            xAxes: [
                {
                    display: _xAxisEnabled,
                    gridLines: { drawOnChartArea: false },
                    ticks:
                    {
                        fontFamily: "'Ubuntu', 'Segoe UI', 'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",
                        fontColor: "white",
                        fontSize: 20,
                        suggestedMax: 120,
                        min: 0,
                        stepSize: 1
                    }
                }],
            yAxes: [
                {
                    display: _yAxisEnabled,
                    gridLines: { drawOnChartArea: false },
                    ticks:
                    {
                        fontFamily: "'Ubuntu', 'Segoe UI', 'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",
                        fontColor: "white",
                        fontSize: 20,
                        suggestedMax: 80,
                        min: 0,
                        stepSize: 1
                    },
                }]
        },
        tooltips: { enabled: false },
        legend: { display: false },
        title: { display: false },
        plugins:
        {
            datalabels:
            {
                align: function (context) {
                    let value = context.dataset.data[context.dataIndex];
                    return value == 0 || value >= 60 ? 'start' : 'end';
                },
                anchor: 'end',
                backgroundColor: [],
                borderRadius: function (context) {
                    let width = context.chart.width;
                    return width <= 991 ? 14 : Math.round(width / 200)
                },
                clamp: true,
                clip: false,
                color: [],
                display: 'auto',
                font: function (context) {
                    //let width = context.chart.width;
                    //let size = width <= 991 ? 12 : Math.round(width / 100);
                    //if (context.dataset.data[context.dataIndex] == 0) size = 2;
                    return { size: fontsize, family: "'Ubuntu', 'Segoe UI','Helvetica Neue', 'Helvetica', 'Arial', sans-serif", weight: 'bold', lineHeight: 0.7 };
                },
                formatter: value => value == 0 ? '' : value,
                offset: context => context.dataset.data[context.dataIndex] == 0 ? -4 : 6,
                padding: function (context) {
                    let width = context.chart.width;
                    let size = width <= 991 ? 8 : Math.round(width / 150);
                    if (context.dataset.data[context.dataIndex] == 0) size = 6;
                    return { top: size / 1.6, bottom: size / 2, left: size / 2, right: size / 1.5 };
                },
                textAlign: 'center'
            }
        }
    }

    return new Chart(context, { type: chartType, data: chartConfig, options: chartOptions });
}