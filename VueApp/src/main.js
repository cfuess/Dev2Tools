import Vue from 'vue'
import App from './App.vue'
import Bootstrap from 'bootstrap/dist/css/bootstrap.min.css'
import Style from './assets/main.css'
import router from './router/index'

new Vue({
  el: '#app',
  router,
  render: h => h(App)
})
