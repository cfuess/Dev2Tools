<!-- MyComponent.vue -->

<!-- template -->
<template>
  <div class="vueNav">

  <nav class="navbar navbar-expand-lg navbar-light bg-light">

  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>

  <div class="collapse navbar-collapse" id="navbarSupportedContent">
    <ul class="navbar-nav mr-auto">
      <li class="nav-item">
        <router-link to='/' class="nav-link">Home</router-link>
      </li>
      <!-- <li class="nav-item">
        <router-link :to="{name: 'search' }" class="nav-link">Joke Search</router-link>
      </li>   -->
      <li class="nav-item">
        <router-link to='/timeline' class="nav-link">Timeline</router-link>
      </li>
      <!-- <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
          Dropdown
        </a>
        <div class="dropdown-menu" aria-labelledby="navbarDropdown">
          <a class="dropdown-item" href="#">Action</a>
          <a class="dropdown-item" href="#">Another action</a>
          <div class="dropdown-divider"></div>
          <a class="dropdown-item" href="#">Something else here</a>
        </div>
      </li> -->
      <!-- <li class="nav-item">
        <a class="nav-link disabled" href="#">Disabled</a>
      </li> -->
    </ul>
  </div>
</nav>

  </div>
</template>

<!-- js -->
<script>
export default {
  name: 'navigation',
  props: ['message'],
  created () {
    console.log('NavBar created!')
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.navIcon {
    font-size: x-large;
    background-image: url('../assets/icon-management.png') !important;
    width: 40px;
  }

  .jumbotron {
    background-position: center !important;
    background-repeat: no-repeat !important;
    background-size: cover !important;
  }  
</style>
