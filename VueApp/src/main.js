import Vue from 'vue'
import App from './App.vue'
import Bootstrap from 'bootstrap/dist/css/bootstrap.min.css'
import Style from './assets/main.css'
import router from './router/index'
import vSelect from 'vue-select'


new Vue({
  el: '#app',
  router,
  render: h => h(App)
})

Vue.component('v-select', vSelect)
