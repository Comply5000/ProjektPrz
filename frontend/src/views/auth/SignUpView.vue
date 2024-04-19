<template>
    <div>
      <form @submit.prevent="submitForm">
        <div>
          <label for="email">Email:</label>
          <input type="email" id="email" v-model="form.email" required>
        </div>
        <div>
          <label for="password">Hasło:</label>
          <input type="password" id="password" v-model="form.password" required>
        </div>
        <div>
          <label for="confirmedPassword">Potwierdź hasło:</label>
          <input type="password" id="confirmedPassword" v-model="form.confirmedPassword" required>
        </div>
        <button type="submit">Zarejestruj się</button>
      </form>
    </div>
  </template>
  
  <script>
   import axios from '../../../config.js';
  
  export default {
    name: 'RegisterForm',
    data() {
      return {
        form: {
          email: '',
          password: '',
          confirmedPassword: ''
        }
      };
    },
    methods: {
      submitForm() {
        axios.post('/user-identity/sign-up', this.form)
          .then(response => {
            alert('Rejestracja zakończona sukcesem!');
          })
          .catch(error => {
            console.error('Wystąpił błąd podczas rejestracji', error);
            alert('Wystąpił błąd podczas rejestracji: ' + error.response.data.message);
          });
      }
    }
  };
  </script>
  