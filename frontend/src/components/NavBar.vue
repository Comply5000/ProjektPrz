<template>
  <div class="header">
    <div class="left">
      <span class="offer">Oferty</span>
      <span class="company">Firmy</span>
      <span class="company" v-if="islogin">Moja Firma</span>
      <span class="company" v-if="islogin">Moje Oferty</span>
    </div>
    <div class="right">
      <span class="login" v-if="!islogin">Zaloguj</span>
      <span class="register" v-if="!islogin">Zarejestruj</span>
      <span class="profile" v-if="islogin">Maciek</span>
    </div>
  </div>
</template>
<script>
  export default {
    name: 'RegisterForm',
    data() {
      return {
       islogin: true
      };
    },
    mounted()
    {
      const token = localStorage.getItem('jwt');
      if(token==null || token=='')
      {
        this.islogin=true;
      }
      else
      {
        this.islogin=true;
      }
    }
  };
</script>
<style scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: #f0f0f0; /* szary kolor */
  padding: 10px 20px;

  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  z-index: 100; /* Upewnij się, że navbar jest na wierzchu innych elementów */

}

.left {
  display: flex;
}

.right {
  display: flex;
}

.offer,
.company,
.login,
.register {
  margin-right: 20px;
  cursor: pointer;
}

.offer,
.company {
  color: #008000; /* zielony kolor */
}
</style>
