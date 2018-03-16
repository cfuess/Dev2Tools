<template>
<div>

  <div class="d-flex justify-content-between">
      <h3>{{legalName}}</h3>
      <h4>{{businessName}} <span class="empId"> (empId  {{empId}})</span></h4>
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

export default {
    name: "Timeline",
    data: function () {
        return {
            num: this.getSearchNum(),
            esdNum: "",
            empId: "",
            legalName: "",
            businessName: "",
            test: {
              "LegalName": "C1X, INC.",
              "BusinessName": "C1X, INC.",
              "EsdNum": "000730509005",
              "EmpId": 10295112,
              "LiabilityPeriods": [{
                "StartDate": "2017-12-15T00:00:00",
                "EndDate": "2018-03-16T14:32:53.6047333-07:00"
              }],
              "WagePeriods": [{
                "Qtr": 3,
                "Year": 2017,
                "Gross": 0.00,
                "StartDate": "2017-07-01T00:00:00",
                "EndDate": "2017-09-30T00:00:00",
                "IsNoPayroll": true
              },
              {
                "Qtr": 4,
                "Year": 2017,
                "Gross": 12800.00,
                "StartDate": "2017-10-01T00:00:00",
                "EndDate": "2017-12-31T00:00:00",
                "IsNoPayroll": false
              }]
            }
        };
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
            // axios.get(`http://localhost:61101/api/Ngts/${vm.num}`)
            //     .then(function (response) {
            //         console.log(response.data);
            //         vm.loadChart(response.data)
            //     })
            //     .catch(function (error) {
            //         console.log(error);
            //     });
            vm.loadChart(vm.test);
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

            function drawChart() {
                var barColors = [];

                var container = document.getElementById('example5.1');
                var chart = new google.visualization.Timeline(container);
                var dataTable = new google.visualization.DataTable();

                dataTable.addColumn({ type: 'string', id: 'Item' });
                dataTable.addColumn({ type: 'string', id: 'Label' });
                dataTable.addColumn({ type: 'string', role: 'tooltip' });
                dataTable.addColumn({ type: 'date', id: 'Start' });
                dataTable.addColumn({ type: 'date', id: 'End' });

                employer.LiabilityPeriods.forEach((element, index, array) => {
                    var startDate1 = moment(element.StartDate);
                    var startDate2 = moment(element.EndDate);

                    var mDate1 = startDate1.format("M/D/YY");
                    var mDate2 = startDate2.format("M/D/YY");

                    var barLabel = mDate1 + ' - ' + mDate2;
                    var d = startDate2.diff(startDate1, 'years', true);
                    var dRound = Math.round(d * 100) / 100
                    var tooltip = dRound + ' years ' + ' - Active:' + barLabel;

                    barColors.push('#0288d1');

                    dataTable.addRow(['Active', barLabel, tooltip, startDate1.toDate(), startDate2.toDate()]);
                });

                employer.WagePeriods.forEach((element, index, array) => {
                    var startDate1 = moment(element.StartDate);
                    var startDate2 = moment(element.EndDate);
                    var mDate1 = startDate1.format("M/D/YY");
                    var mDate2 = startDate2.format("M/D/YY");

                    var nf = new Intl.NumberFormat('en-US', {
                        style: 'currency',
                        currency: 'USD',
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    });

                    var tooltip = 'Q' + startDate1.quarter().toString() + ' ' + moment(element.StartDate).format("YYYY")
                        + ' ' + nf.format(element.Gross);

                    if (element.IsNoPayroll) {
                        tooltip = 'No Payroll - ' + tooltip;
                        barColors.push('#bdbdbd');
                        console.log(`${mDate1} : ${element.IsNoPayroll}`)
                    }
                    else {
                        barColors.push('#c53929');
                    }
                    dataTable.addRow(['Wages', '', tooltip, startDate1.toDate(), startDate2.toDate()]);
                });

                var options = {
                    //timeline: { colorByRowLabel: true },
                    colors: barColors,
                    hAxis: {
                        format: 'M/d/yy'
                    }
                };


                // var dataTable2 = new google.visualization.DataTable();
                // dataTable2.addColumn({ type: 'string', id: 'Role' });
                // dataTable2.addColumn({ type: 'string', id: 'Name' });
                // dataTable2.addColumn({ type: 'date', id: 'Start' });
                // dataTable2.addColumn({ type: 'date', id: 'End' });
                // dataTable2.addRows([
                //     ['Washington', 'George Washington', new Date(1789, 3, 30), new Date(1797, 2, 4)],
                //     ['Adams', 'John Adams', new Date(1797, 2, 4), new Date(1801, 2, 4)],
                //     ['Jefferson', 'Thomas Jefferson', new Date(1801, 2, 4), new Date(1809, 2, 4)]]);


                // var options2 = {
                //     colors: ['#cbb69d', '#c62', '#603913'],
                //     timeline: {
                //         rowLabelStyle: { fontName: 'Helvetica', fontSize: 24, color: '#603913' },
                //         barLabelStyle: { fontName: 'Garamond', fontSize: 14 }
                //     }
                // };

                //chart.draw(dataTable2, options2);

                chart.draw(dataTable, options);
            }
        }

    },

    mounted() {
        this.doSearch();
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
