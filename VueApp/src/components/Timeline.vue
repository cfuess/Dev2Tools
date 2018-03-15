<template>
<div>

  <div class="d-flex justify-content-between">
      <h4>{{name}} <span class="empId">(empId {{empId}})</span></h4>
      <div>
          ESD # 
          <input type="text" v-model="esdNum">
          <button v-on:click="doSearch">Search</button>
      </div>
  </div>

  <div id="example5.1" style="height: 2000px;"></div>

</div>
</template>

<script>
import moment from "moment";
import axios from "axios";

export default {
  name: "Timeline",
  data: function() {
    return {
      esdNum: "000283192003",
      empId: "",
      name: ""
    };
  },

  methods: {

    doSearch: function(event) {
      let vm = this;
      vm.esdNum = vm.esdNum.replace(/\D/g,'');
      axios.get(`http://localhost:61101/api/Ngts/${vm.esdNum}`)
        .then(function(response) {
          console.log(response);
          vm.loadChart(response.data)
        })
        .catch(function(error) {
          console.log(error);
        });
    },

    loadChart: function (employer) {
        this.name = employer.Name;
        this.empId = employer.EmpId;
        google.charts.load("current", { packages: ["timeline"] });
        google.charts.setOnLoadCallback(drawChart);
        
        function drawChart() {
            var colStartOpts = { type: 'date', pattern: 'M/d/yy', id: 'Start' };
            var colEndOpts = { type: 'date', pattern: 'M/d/yy', id: 'End' };
            var barColors = [];

            var container = document.getElementById('example5.1');
            var chart = new google.visualization.Timeline(container);
            var dataTable = new google.visualization.DataTable();
            
            dataTable.addColumn({ type: 'string', id: 'Room' });
            dataTable.addColumn({ type: 'string', id: 'Value' });
            dataTable.addColumn(colStartOpts);
            dataTable.addColumn(colEndOpts);

            employer.LiabilityPeriods.forEach((element, index, array) => {
                var startDate1 = moment(element.StartDate);
                var startDate2 = moment(element.EndDate);

                var mDate1 = startDate1.format("M/D/YY");
                var mDate2 = startDate2.format("M/D/YY");

                barColors.push('#0288d1');

                dataTable.addRow(['Active', mDate1 + ' - ' + mDate2, startDate1.toDate(), startDate2.toDate()]);
            });

            employer.WagePeriods.forEach((element, index, array) => {
                var startDate1 = moment(element.StartDate);
                var startDate2 = moment(element.EndDate);

                var mDate1 = startDate1.format("M/D/YY");
                var mDate2 = startDate2.format("M/D/YY");

                if (element.IsNoPayroll) {
                  barColors.push('#bdbdbd');
                  console.log(`${mDate1} : ${element.IsNoPayroll}`)
                }
                else {
                  barColors.push('#c53929');
                }
                dataTable.addRow(['Wages', element.Gross.toString(), startDate1.toDate(), startDate2.toDate()]);
            });

            var options = {
                //timeline: { colorByRowLabel: true },
                colors: barColors,
                hAxis: {
                    format: 'M/d/yy'
                }
            };

            chart.draw(dataTable, options);
            }
    }

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
