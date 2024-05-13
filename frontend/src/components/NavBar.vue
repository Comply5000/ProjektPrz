<template>
  <div class="header">
    <div class="left">
      <span class="offer">Oferty</span>
      <span class="company">Firmy</span>
      <span class="company" v-if="isCompany()">Moja Firma</span>
      <span class="company" v-if="isCompany()">Moje Oferty</span>
    </div>
    <div class="right">
      <router-link to="/sign-in" class="login" v-if="!islogin">Zaloguj</router-link>
      <router-link to="/sign-up" class="register" v-if="!islogin">Zarejestruj</router-link>
      <span class="profile" v-if="islogin">{{ this.email }}</span>
    </div>
  </div>
</template>
<script>
  import { CheckUserRole } from '../services/UserService.js';


  export default {
    name: 'RegisterForm',
    data() {
      return {
       islogin: false,
       email: ''
      };
    },
    methods: {
      isCompany()
      {
        return CheckUserRole('CompanyOwner');
      }
    },
    mounted()
    {
      this.email = localStorage.getItem('email');

      console.log(this.islogin);

      const token = localStorage.getItem('jwt');
      if(token==null || token=='')
      {
        this.islogin = false;
      }
      else
      {
        this.islogin = true;
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
