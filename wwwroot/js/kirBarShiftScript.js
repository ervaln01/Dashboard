const Backgrounds = [['#FC7414', '#FFA11E'], ['#23a84c', '#41fc7a'], ['#9225a8', '#df40ff']];
const separatelabel = (str) => String(str).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
const emptyForm = (str) => { };

function RenderShiftBar(chart, data, context, ajaxUrl, index) {
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

    FillShiftBar(chart, [data[0], data[1]], context);

    setInterval(() => $.ajax({
        url: ajaxUrl,
        type: "POST",
        data: { "index": index },
        success: (r) => {
            var shift = r.shift;
            var plan = r.plan;
            FillShiftBar(chart, [shift, plan], context);
        }
    }), 1000);
}

function FillShiftBar(chart, shiftProd, waitElement) {
    chart.data.labels = ['', ''];
    let difference = shiftProd[1] - shiftProd[0];
    chart.data.datasets[0].data[0] = shiftProd[0];
    chart.data.datasets[0].data[1] = difference > 0 ? difference : 0;
    chart.options.plugins.title = shiftProd[1].toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
    chart.options.plugins.datalabels.formatter = (str) => str != null ? separatelabel(str) : emptyForm(str);
    waitElement[0].style.display = shiftProd == 0 ? "block" : "none";
    chart.update();
}

function CreateShiftBar(context, index, waitElement) {
    let chartBgColor = Backgrounds[index];
    let getTotal = function (theChart) {
        let sum = theChart.config.data.datasets[0].data[0];
        if (sum == 0) waitElement[0].style.display = "block";
        fsVal = sum == 0 ? "" : sum;
        return theChart.config.options.plugins.title;
    }

    let chartConfig =
    {
        labels: ['', ''],
        datasets: [{ backgroundColor: chartBgColor, borderWidth: 0, borderColor: 'midnightblue', data: [0, 0] }]
    }

    let chartOptions =
    {
        cutoutPercentage: 45,
        responsive: true,
        maintainAspectRatio: false,
        aspectRatio: 1,
        tooltips: { enabled: false },
        legend: { display: false },
        title: { display: false },
        plugins:
        {
            datalabels:
            {
                color: 'midnightblue',
                font: function (context) {
                    //let fsize;
                    //if (context.dataset.data[0] != 0) {
                    //    fsize = labelSize;
                    //    fsize += context.dataset.data[1] == 0 ? 2 : 0;
                    //}

                    return { family: "'Ubuntu', 'Segoe UI', 'Helvetica Neue', 'Helvetica', 'Arial', sans-serif", weight: 'bold', lineHeight: 0.9, size: 30 };
                },
                textAlign: 'start',
                anchor: 'center',
                clamp: true,
                clip: false,
                align: 'center',
                offset: -4,
                display: 'auto',
                formatter: function (value, context) {
                    if (value > 0)
                        return context.chart.data.labels[context.dataIndex] + "\n" + value;

                    return context.chart.data.labels[context.dataIndex] = null;
                }
            },
            doughnutlabel:
            {
                labels: [
                    {
                        text: getTotal,
                        font: { size: 40, family: "'Ubuntu', 'Segoe UI', 'Helvetica Neue', 'Helvetica', 'Arial', sans-serif", weight: 'bold', lineHeight: 0 },
                        color: chartBgColor[0]
                    }]
            }
        }
    }

    return new Chart(context, { type: 'doughnut', data: chartConfig, options: chartOptions });
}