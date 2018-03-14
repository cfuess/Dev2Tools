<template>
<div>
<div class="jumbotron">
  <h1 class="display-4">Team 2 Dev Tools!</h1>
  <p class="mx-auto centerbox">Handy tools for managing ngts.</p>
  <p class="lead">
    <a class="btn btn-primary btn-lg" href="https://www.edx.org/course/subject/history" role="button">Learn more</a>
  </p>
</div>
</div>
</template>

<script>


export default {
  name: 'Home',


}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  h1, h2 {
    font-weight: normal;
  }

  .title {
    /*padding-bottom: 10px;*/
    margin: 0px 0 10px 0;
  }

  .jumbotron {
    background-image: url('../assets/TypeApp_Images_Strip_3.jpg') !important;
    background-position: center !important;
    background-repeat: no-repeat !important;
    background-size: cover !important;
  }

  .centerbox {
    width: 300px;
  }

</style>
