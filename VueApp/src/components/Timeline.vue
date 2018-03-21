<template>
<div>

  <div class="d-flex justify-content-between">
      <h3>{{legalName}}</h3>
      <h4> <span v-if="!sameName">{{businessName}}</span> <span class="empId"> (empId  {{empId}})</span></h4>
      <div>
          EmpId or <b>ESD #</b> 
          <input type="text" v-model="num">
          <button v-on:click="doSearch">Search</button>
      </div>
  </div>

  <div id="example5.1" style="height: 2000px;"></div>

</div>
</template>

<script>
import moment from "moment";
import axios from "axios";
import cookies from 'js-cookie';
//import google from '../assets/gcharts'  // doesn't work
export default {
    name: "Timeline",
    data: function () {
        return {
            num: this.getSearchNum(),
            esdNum: "",
            empId: "",
            legalName: "",
            businessName: "",
            fakeData: this.getFakeData()
        };
    },
    computed: {
        sameName () {
            return this.legalName.toLowerCase() == this.businessName.toLowerCase();
        }
    },
    methods: {

        getSearchNum() {
            let cookie = cookies.get('esdNum')

            // the cookie doesn't exist so create one and set to show the video
            if (cookie === undefined) {
                cookies.set('esdNum', '10290852')
                return '10290852'
            } else {
                return cookies.get('esdNum')
            }
        },

        doSearch: function (event) {
            let vm = this;
            vm.num = vm.num.replace(/\D/g, '');
            axios.get(`http://localhost:61101/api/Ngts/${vm.num}`)
                .then(function (response) {
                    console.log(response.data);
                    vm.loadChart(response.data)
                })
                .catch(function (error) {
                    console.log(error);
                });
            //vm.loadChart(vm.fakeData[Math.floor(Math.random()*vm.fakeData.length)]);
        },

        loadChart: function (employer) {
            this.legalName = employer.LegalName;
            this.businessName = employer.BusinessName;
            this.empId = employer.EmpId;
            this.esdNum = employer.EsdNum;
            this.num = employer.EsdNum;
            cookies.set('esdNum', employer.EsdNum)

            google.charts.load("current", { packages: ["timeline"] });
            google.charts.setOnLoadCallback(drawChart);

            function getRandColor (){
                return '#'+'0123456789abcdef'.split('').map(function(v,i,a){
                            return i>5 ? null : a[Math.floor(Math.random()*16)] }).join('');
            }

            function drawChart() {
                var container = document.getElementById('example5.1');
                var chart = new google.visualization.Timeline(container);
                var dataTable = new google.visualization.DataTable();

                dataTable.addColumn({ type: 'string', id: 'Item' });
                dataTable.addColumn({ type: 'string', id: 'Label' });
                dataTable.addColumn({ type: 'string', role: 'tooltip' });
                dataTable.addColumn({ type: 'string', role: 'style' });
                dataTable.addColumn({ type: 'date', id: 'Start' });
                dataTable.addColumn({ type: 'date', id: 'End' });

                var periods = [];
                employer.LiabilityPeriods.forEach((element, index, array) => {
                    var color = '#134466';
                    var startDate1 = moment(element.StartDate);
                    var startDate2 = moment(element.EndDate);

                    var mDate1 = startDate1.format("M/D/YY");
                    var mDate2 = startDate2.format("M/D/YY");

                    var barLabel = mDate1 + ' - ' + mDate2;
                    var d = startDate2.diff(startDate1, 'years', true);
                    var dRound = Math.round(d * 100) / 100
                    var tooltip = dRound + ' years ' + ' - Active:' + barLabel;

                    periods.push(['Active', barLabel, tooltip, color, startDate1.toDate(), startDate2.toDate()]);
                });
                dataTable.addRows(periods);

                var wages = [];
                var qrYrs = [];
                employer.WagePeriods.forEach((element, index, array) => {
                    var color = '';
                    var startDate1 = moment(element.StartDate);
                    var startDate2 = moment(element.EndDate);
                    var mDate1 = startDate1.format("M/D/YY");
                    var mDate2 = startDate2.format("M/D/YY");

                    var nf = new Intl.NumberFormat('en-US', {
                        style: 'currency',
                        currency: 'USD',
                        minimumFractionDigits: 0,
                        maximumFractionDigits: 2
                    });

                    var qrYr = 'Q' + startDate1.quarter().toString() + ' ' + moment(element.StartDate).format("YYYY");
                    if (qrYrs.includes(qrYr)) {
                        qrYr = qrYr + ' ';
                    }
                    qrYrs.push(qrYr);
                    var tooltip = qrYr + ' ' + nf.format(element.Gross);

                    if (element.IsNoPayroll) {
                        tooltip = 'No Payroll - ' + tooltip;
                        color = '#808080';
                        // console.log(`${mDate1} : ${element.IsNoPayroll}`)
                    }
                    else {
                        color = '#955659';
                    }
                    // color = getRandColor();
                    wages.push(['Wages', qrYr, tooltip, color, startDate1.toDate(), startDate2.toDate()]);
                });
                dataTable.addRows(wages);

                var options = {
                    hAxis: { format: 'M/d/yy' },
                    timeline: {
                        rowLabelStyle: { fontName: 'Helvetica', fontSize: 24, color: '#603913' },
                        barLabelStyle: { fontName: 'Garamond', fontSize: 16 }
                    }
                };

                chart.draw(dataTable, options);
            }
        },
        
        getFakeData: function () {
            var fakedata = [];
            fakedata.push({"LegalName":"Pinky","BusinessName":"C2X, INC.","EsdNum":"111730509005","EmpId":11195112,"LiabilityPeriods":[{"StartDate":"2017-12-15T00:00:00","EndDate":"2018-03-16T15:58:41.1724289-07:00"}],"WagePeriods":[{"Qtr":3,"Year":2017,"Gross":0.00,"StartDate":"2017-07-01T00:00:00","EndDate":"2017-09-30T00:00:00","IsNoPayroll":true},{"Qtr":4,"Year":2017,"Gross":12800.00,"StartDate":"2017-10-01T00:00:00","EndDate":"2017-12-31T00:00:00","IsNoPayroll":false}]});
            fakedata.push({"LegalName":"Dinky","BusinessName":"Optimus Inc","EsdNum":"112001204002","EmpId":11246,"LiabilityPeriods":[{"StartDate":"2001-07-01T00:00:00","EndDate":"2010-12-31T00:00:00"},{"StartDate":"2012-09-30T00:00:00","EndDate":"2015-07-17T00:00:00"},{"StartDate":"2016-07-18T00:00:00","EndDate":"2018-03-16T15:54:27.8545643-07:00"}],"WagePeriods":[{"Qtr":3,"Year":1997,"Gross":1137.18,"StartDate":"1997-07-01T00:00:00","EndDate":"1997-09-30T00:00:00","IsNoPayroll":true},{"Qtr":4,"Year":1997,"Gross":1599.75,"StartDate":"1997-10-01T00:00:00","EndDate":"1997-12-31T00:00:00","IsNoPayroll":false},{"Qtr":1,"Year":1998,"Gross":2700.77,"StartDate":"1998-01-01T00:00:00","EndDate":"1998-03-31T00:00:00","IsNoPayroll":true},{"Qtr":2,"Year":1998,"Gross":1552.50,"StartDate":"1998-04-01T00:00:00","EndDate":"1998-06-30T00:00:00","IsNoPayroll":true},{"Qtr":3,"Year":1998,"Gross":739.50,"StartDate":"1998-07-01T00:00:00","EndDate":"1998-09-30T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":1998,"Gross":989.00,"StartDate":"1998-10-01T00:00:00","EndDate":"1998-12-31T00:00:00","IsNoPayroll":false},{"Qtr":1,"Year":1999,"Gross":272.00,"StartDate":"1999-01-01T00:00:00","EndDate":"1999-03-31T00:00:00","IsNoPayroll":false},{"Qtr":2,"Year":1999,"Gross":3921.75,"StartDate":"1999-04-01T00:00:00","EndDate":"1999-06-30T00:00:00","IsNoPayroll":false},{"Qtr":3,"Year":1999,"Gross":170.00,"StartDate":"1999-07-01T00:00:00","EndDate":"1999-09-30T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":1999,"Gross":1475.00,"StartDate":"1999-10-01T00:00:00","EndDate":"1999-12-31T00:00:00","IsNoPayroll":false},{"Qtr":3,"Year":2000,"Gross":550.00,"StartDate":"2000-07-01T00:00:00","EndDate":"2000-09-30T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":2000,"Gross":10565.13,"StartDate":"2000-10-01T00:00:00","EndDate":"2000-12-31T00:00:00","IsNoPayroll":false},{"Qtr":1,"Year":2001,"Gross":1897.50,"StartDate":"2001-01-01T00:00:00","EndDate":"2001-03-31T00:00:00","IsNoPayroll":false},{"Qtr":2,"Year":2001,"Gross":127.50,"StartDate":"2001-04-01T00:00:00","EndDate":"2001-06-30T00:00:00","IsNoPayroll":false},{"Qtr":3,"Year":2001,"Gross":3571.26,"StartDate":"2001-07-01T00:00:00","EndDate":"2001-09-30T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":2001,"Gross":4839.58,"StartDate":"2001-10-01T00:00:00","EndDate":"2001-12-31T00:00:00","IsNoPayroll":false},{"Qtr":1,"Year":2002,"Gross":4099.11,"StartDate":"2002-01-01T00:00:00","EndDate":"2002-03-31T00:00:00","IsNoPayroll":false},{"Qtr":2,"Year":2002,"Gross":6925.62,"StartDate":"2002-04-01T00:00:00","EndDate":"2002-06-30T00:00:00","IsNoPayroll":false},{"Qtr":3,"Year":2002,"Gross":4819.50,"StartDate":"2002-07-01T00:00:00","EndDate":"2002-09-30T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":2002,"Gross":17707.13,"StartDate":"2002-10-01T00:00:00","EndDate":"2002-12-31T00:00:00","IsNoPayroll":false},{"Qtr":1,"Year":2003,"Gross":8491.50,"StartDate":"2003-01-01T00:00:00","EndDate":"2003-03-31T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":2003,"Gross":1842.58,"StartDate":"2003-10-01T00:00:00","EndDate":"2003-12-31T00:00:00","IsNoPayroll":false},{"Qtr":1,"Year":2004,"Gross":320.96,"StartDate":"2004-01-01T00:00:00","EndDate":"2004-03-31T00:00:00","IsNoPayroll":false},{"Qtr":1,"Year":2005,"Gross":4435.75,"StartDate":"2005-01-01T00:00:00","EndDate":"2005-03-31T00:00:00","IsNoPayroll":false},{"Qtr":2,"Year":2005,"Gross":5250.00,"StartDate":"2005-04-01T00:00:00","EndDate":"2005-06-30T00:00:00","IsNoPayroll":false},{"Qtr":3,"Year":2005,"Gross":5250.00,"StartDate":"2005-07-01T00:00:00","EndDate":"2005-09-30T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":2005,"Gross":5250.00,"StartDate":"2005-10-01T00:00:00","EndDate":"2005-12-31T00:00:00","IsNoPayroll":false},{"Qtr":1,"Year":2006,"Gross":5512.50,"StartDate":"2006-01-01T00:00:00","EndDate":"2006-03-31T00:00:00","IsNoPayroll":false},{"Qtr":2,"Year":2006,"Gross":5250.00,"StartDate":"2006-04-01T00:00:00","EndDate":"2006-06-30T00:00:00","IsNoPayroll":false},{"Qtr":3,"Year":2006,"Gross":5250.00,"StartDate":"2006-07-01T00:00:00","EndDate":"2006-09-30T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":2006,"Gross":5250.00,"StartDate":"2006-10-01T00:00:00","EndDate":"2006-12-31T00:00:00","IsNoPayroll":false},{"Qtr":1,"Year":2007,"Gross":5250.00,"StartDate":"2007-01-01T00:00:00","EndDate":"2007-03-31T00:00:00","IsNoPayroll":false},{"Qtr":2,"Year":2007,"Gross":5562.50,"StartDate":"2007-04-01T00:00:00","EndDate":"2007-06-30T00:00:00","IsNoPayroll":false},{"Qtr":3,"Year":2007,"Gross":5250.00,"StartDate":"2007-07-01T00:00:00","EndDate":"2007-09-30T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":2007,"Gross":5250.00,"StartDate":"2007-10-01T00:00:00","EndDate":"2007-12-31T00:00:00","IsNoPayroll":false},{"Qtr":1,"Year":2008,"Gross":5250.00,"StartDate":"2008-01-01T00:00:00","EndDate":"2008-03-31T00:00:00","IsNoPayroll":false},{"Qtr":2,"Year":2008,"Gross":5250.00,"StartDate":"2008-04-01T00:00:00","EndDate":"2008-06-30T00:00:00","IsNoPayroll":false},{"Qtr":3,"Year":2008,"Gross":5250.00,"StartDate":"2008-07-01T00:00:00","EndDate":"2008-09-30T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":2008,"Gross":5250.00,"StartDate":"2008-10-01T00:00:00","EndDate":"2008-12-31T00:00:00","IsNoPayroll":false},{"Qtr":3,"Year":2012,"Gross":4078.00,"StartDate":"2012-07-01T00:00:00","EndDate":"2012-09-30T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":2012,"Gross":33402.10,"StartDate":"2012-10-01T00:00:00","EndDate":"2012-12-31T00:00:00","IsNoPayroll":false},{"Qtr":1,"Year":2013,"Gross":17275.50,"StartDate":"2013-01-01T00:00:00","EndDate":"2013-03-31T00:00:00","IsNoPayroll":true},{"Qtr":2,"Year":2013,"Gross":7020.00,"StartDate":"2013-04-01T00:00:00","EndDate":"2013-06-30T00:00:00","IsNoPayroll":false},{"Qtr":3,"Year":2016,"Gross":2543.22,"StartDate":"2016-07-01T00:00:00","EndDate":"2016-09-30T00:00:00","IsNoPayroll":false},{"Qtr":1,"Year":2017,"Gross":2025.00,"StartDate":"2017-01-01T00:00:00","EndDate":"2017-03-31T00:00:00","IsNoPayroll":false},{"Qtr":2,"Year":2017,"Gross":10612.00,"StartDate":"2017-04-01T00:00:00","EndDate":"2017-06-30T00:00:00","IsNoPayroll":false},{"Qtr":3,"Year":2017,"Gross":915.00,"StartDate":"2017-07-01T00:00:00","EndDate":"2017-09-30T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":2017,"Gross":8807.50,"StartDate":"2017-10-01T00:00:00","EndDate":"2017-12-31T00:00:00","IsNoPayroll":false}]});
            fakedata.push({"LegalName":"Blinky","BusinessName":"Sparkplug Auto Parts","EsdNum":"113704299000","EmpId":1130819,"LiabilityPeriods":[{"StartDate":"2017-06-09T00:00:00","EndDate":"2017-09-30T00:00:00"}],"WagePeriods":[{"Qtr":3,"Year":2017,"Gross":11614.40,"StartDate":"2017-07-01T00:00:00","EndDate":"2017-09-30T00:00:00","IsNoPayroll":false},{"Qtr":4,"Year":2017,"Gross":0.00,"StartDate":"2017-10-01T00:00:00","EndDate":"2017-12-31T00:00:00","IsNoPayroll":true}]});
            fakedata.push({"LegalName":"Stinky","BusinessName":"STUFF AND FLASHING LIGHTS","EsdNum":"114703347009","EmpId":1142899,"LiabilityPeriods":[{"StartDate":"2017-07-01T00:00:00","EndDate":"2018-03-16T16:04:58.2684567-07:00"}],"WagePeriods":[{"Qtr":2,"Year":2017,"Gross":0.00,"StartDate":"2017-04-01T00:00:00","EndDate":"2017-06-30T00:00:00","IsNoPayroll":true},{"Qtr":3,"Year":2017,"Gross":1222.32,"StartDate":"2017-07-01T00:00:00","EndDate":"2017-09-30T00:00:00","IsNoPayroll":false}]});
            return fakedata;
        }
    },

    mounted() {
      if (document.getElementById('my-chart')) return; // was already loaded
      var scriptTag = document.createElement("script");
      scriptTag.src = "https://www.gstatic.com/charts/loader.js";
      scriptTag.id = "my-chart";
      document.getElementsByTagName('head')[0].appendChild(scriptTag);
    //   this.doSearch();
    }

};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1,
h2 {
  font-weight: normal;
}

.title {
  /*padding-bottom: 10px;*/
  margin: 0px 0 10px 0;
}

.image {
  width: 400px;
  margin: 10px 0 10px 0;
}

.empId {
  color: #888;
  font-size: .5em;
}
</style>
