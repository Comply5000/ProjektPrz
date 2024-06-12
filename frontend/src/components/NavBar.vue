<template>
  <div class="header">
    <div class="left">
      <router-link to="/" class="offer">Oferty</router-link>
      <router-link to="/company-list" class="company element">Firmy</router-link>
      <router-link to="/update-company" class="company element" v-if="isCompany()">Moja Firma</router-link>
      <router-link to="/company-offers-list" class="company element" v-if="isCompany()">Moje Oferty</router-link>
    </div>
    <div class="right">
      <router-link to="/sign-in" class="login element" v-if="!islogin">Zaloguj</router-link>
      <router-link to="/sign-up" class="register element" v-if="!islogin">Zarejestruj</router-link>
      <div class="profile element" v-if="islogin" @click="toggleDropdown">
        {{ email }}
        <div v-if="dropdownVisible" class="dropdown-menu">
          <router-link to="/change-password" v-if="!isExternal()">Zmiana hasła</router-link>
          <a href="#" @click.prevent="signOut">Wyloguj</a>
        </div>
      </div>
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
      email: '',
      dropdownVisible: false,
    };
  },
  methods: {
    isCompany() {
      return CheckUserRole('CompanyOwner');
    },
    isExternal() {
      return localStorage.getItem('isExternal') === 'true';
    },
    signOut() {
      localStorage.removeItem('jwt');
      localStorage.removeItem('roles');
      localStorage.removeItem('isExternal');
      localStorage.removeItem('companyId');
      location.reload();
      this.$router.push('/');
    },
    toggleDropdown() {
      this.dropdownVisible = !this.dropdownVisible;
    },
  },
  mounted() {
    this.email = localStorage.getItem('email');

    console.log(this.islogin);

    const token = localStorage.getItem('jwt');
    if (token == null || token == '') {
      this.islogin = false;
    } else {
      this.islogin = true;
    }
  },
};
</script>

<style>
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
.element {
  color: #008000; /* zielony kolor */
}

a {
  text-decoration: none;
  color: #008000;
}

.profile {
  position: relative;
}

.dropdown-menu {
  position: absolute;
  top: 100%;
  right: 0;
  background-color: #ffffff;
  border: 1px solid #ccc;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
}

.dropdown-menu a,
.dropdown-menu router-link {
  padding: 10px 20px;
  white-space: nowrap;
}

.dropdown-menu a:hover,
.dropdown-menu router-link:hover {
  background-color: #f0f0f0;
}
</style>
