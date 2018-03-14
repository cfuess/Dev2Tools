<template>
  <div id="app" class="container-fluid">
    <app-navigation></app-navigation>
    <router-view/>
  </div>
</template>

<script>
import NavBar from './components/Navbar'

export default {
  name: 'app',
  components: {
    appNavigation: NavBar
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
}
</style>
