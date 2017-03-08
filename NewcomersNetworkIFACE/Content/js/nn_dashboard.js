function startgraph(){

  var cCharType = "Line";

  var graphdef = {
    categories : ['Users Registered', 'Needs Registered', 'Help Registered', 'Needs Met'],
    dataset : {
      'Users Registered' : [
        { name : '06/2016', value: 15},
        { name : '07/2016', value: 28},
        { name : '08/2016', value: 42},
        { name : '09/2016', value: 88},
        { name : '10/2016', value: 100},
        { name : '11/2016', value: 13}
      ],
      'Needs Registered' : [
        { name : '06/2016', value: 5},
        { name : '07/2016', value: 12},
        { name : '08/2016', value: 18},
        { name : '09/2016', value: 25},
        { name : '10/2016', value: 33},
        { name : '11/2016', value: 5}
      ],
      'Help Registered' : [
        { name : '06/2016', value: 1},
        { name : '07/2016', value: 5},
        { name : '08/2016', value: 7},
        { name : '09/2016', value: 9},
        { name : '10/2016', value: 15},
        { name : '11/2016', value: 2}
      ],
      'Needs Met' : [
        { name : '06/2016', value: 0},
        { name : '07/2016', value: 1},
        { name : '08/2016', value: 3},
        { name : '09/2016', value: 4},
        { name : '10/2016', value: 6},
        { name : '11/2016', value: 1}
      ]
    }
  }

  var config = {
    graph: {
      orientation: 'Vertical',
    },
    meta : {
      caption : 'Newcomers Network',
      subcaption : 'Registration',
      hlabel : 'Month/Year',
      vlabel : 'Number of occurrences',
      vsublabel : ''
    },
    legend : {
      position: 'bottom',

    }
  }

  var chartObject = uv.chart(cCharType, graphdef, config);

  var cCharType1 = "Area";

  var graphdef1 = {
    categories : ['Needs Registered', 'Needs Met', 'Needs Open (Total)'],
    dataset : {
      'Needs Registered' : [
        { name : '06/2016', value: 5},
        { name : '07/2016', value: 12},
        { name : '08/2016', value: 18},
        { name : '09/2016', value: 25},
        { name : '10/2016', value: 33},
        { name : '11/2016', value: 5}
      ],
      'Needs Met' : [
        { name : '06/2016', value: 0},
        { name : '07/2016', value: 1},
        { name : '08/2016', value: 3},
        { name : '09/2016', value: 4},
        { name : '10/2016', value: 6},
        { name : '11/2016', value: 1}
      ],
      'Needs Open (Total)' : [
        { name : '06/2016', value: 5},
        { name : '07/2016', value: 16},
        { name : '08/2016', value: 31},
        { name : '09/2016', value: 52},
        { name : '10/2016', value: 79},
        { name : '11/2016', value: 83}
      ]
    }
  }

  var config1 = {
    graph: {
      orientation: 'Vertical',
    },
    meta : {
      caption : 'Newcomers Network',
      subcaption : 'Needs',
      hlabel : 'Month/Year',
      vlabel : 'Number of occurrences',
      vsublabel : ''
    },
    legend : {
      position: 'bottom',
    }
  }

  var chartObject1 = uv.chart(cCharType1, graphdef1, config1);

}

function drawGCharts(){

  var chart1Data = new google.visualization.DataTable();
  chart1Data.addColumn('string', 'Month/Year');
  chart1Data.addColumn('number', 'New Registrations');
  chart1Data.addColumn('number', 'Needs Registered');
  chart1Data.addColumn('number', 'Help Registered');
  chart1Data.addRows([  ['06/2016', 15, 5, 1],
                        ['07/2016', 28, 12, 5],
                        ['08/2016', 42, 18, 7],
                        ['09/2016', 88, 25, 9],
                        ['10/2016', 100, 33, 15],
                        ['11/2016', 13, 5, 2] ]);

  var options1 = {
    title: 'Newcomers Network Registration',
    hAxis: {title: 'Month/Year',  titleTextStyle: {color: '#333'}},
    vAxis: {minValue: 0},
    legend: {position: 'bottom', maxLines: 3},
    height: 400
  };

  var chart1 = new google.visualization.LineChart(document.getElementById('register-chart'));
  chart1.draw(chart1Data, options1);


  var chart2Data = new google.visualization.DataTable();
  chart2Data.addColumn('string', 'Month/Year');
  chart2Data.addColumn('number', 'Needs Registered');
  chart2Data.addColumn('number', 'Needs Met');
  chart2Data.addColumn('number', 'Needs (Total)');
  chart2Data.addRows([  ['06/2016', 5, 0, 5],
                        ['07/2016', 12, 1, 16],
                        ['08/2016', 18, 3, 31],
                        ['09/2016', 25, 4, 52],
                        ['10/2016', 33, 6, 79],
                        ['11/2016', 5, 1, 83] ]);

  var options2 = {
    title: 'Needs',
    hAxis: {title: 'Month/Year',  titleTextStyle: {color: '#333'}},
    vAxis: {minValue: 0},
    legend: {position: 'bottom', maxLines: 3},
    height: 400
  };

  var chart2 = new google.visualization.AreaChart(document.getElementById('needs-chart'));
  chart2.draw(chart2Data, options2);

  var chart3Data = new google.visualization.DataTable();
  chart3Data.addColumn('string', 'Need Type');
  chart3Data.addColumn('number', 'Needs');
  chart3Data.addRows([  [ "Housing", 60 ],
                        [ "Settlement", 13 ],
                        [ "Employment", 15 ],
                        [ "Acessibility", 10 ]
 ]);

  var options3 = {
    title: 'Needs x Category',
    legend: {position: 'bottom', maxLines: 3},
    height: 400,
    is3D: true,
    slices: [{color: '#b6d554', textStyle: { color: '#000'} }, {color: '#ba0f6b'}, {color: '#fec33e', textStyle: { color: '#000'} }, {color: '#00588d'}]
  };

  var chart3 = new google.visualization.PieChart(document.getElementById('needs-category'));
  chart3.draw(chart3Data, options3);

}

//create trigger to resizeEnd event
$(window).resize(function() {
    if(this.resizeTO) clearTimeout(this.resizeTO);
    this.resizeTO = setTimeout(function() {
        $(this).trigger('resizeEnd');
    }, 500);
});

$(window).on('resizeEnd', drawGCharts);

//Function to change the selected sidebar class menu
function menuclick(nMenuID) {
    
    var menuActions = [
        { "menuLInk": "/NNAdmin1/Dashboard", "menuID": "#dashboardmnu" },
        { "menuLInk": "/NNAdmin1/Classifieds", "menuID": "#classifiedsmnu" },
        { "menuLInk": "/NNAdmin1/Events", "menuID": "#eventsmnu" },
        { "menuLInk": "/NNAdmin1/Reports", "menuID": "#reportsmnu" },
        { "menuLInk": "/NNAdmin1/Reports/MentorXMentee", "menuID": "#reportsmnu" },
        { "menuLInk": "/NNAdmin1/Reports/NeedsXHelp", "menuID": "#reportsmnu" },
        { "menuLInk": "/NNAdmin1/Services", "menuID": "#servicesmnu" },
        { "menuLInk": "/NNAdmin1/Users", "menuID": "#usersmnu" },
        { "menuLInk": "/NNAdmin1/Calendar", "menuID": "#calendarmnu" },
        { "menuLInk": "/NNAdmin1/Mailing", "menuID": "#mailingmnu" }
    ];
    var oAction;

    oAction = menuActions[nMenuID];

    if(oAction != null ){
        $(".menuitem").removeClass("active");
        $(oAction.menuID).addClass("active");
        window.location = oAction.menuLInk;
    }

}


