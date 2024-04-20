<template>
    <div class="vue-template">
      <form>
      <div class="card">
        <h3>Rejestracja</h3>
        <div class="mb-3">
          <label for="email">Podaj swój adres Email:</label>
          <input type="email" id="email" class="form-control form-control-lg" v-model="form.email" required>
        </div>
        <div class="mb-3">
          <label for="password"> Podaj Hasło:</label>
          <input type="password" id="password" class="form-control form-control-lg" v-model="form.password" required>
        </div>
        <div class="mb-3">
          <label for="confirmedPassword">Potwierdź hasło:</label>
          <input type="password" id="confirmedPassword" class="form-control form-control-lg" v-model="form.confirmedPassword" required>
        </div>
        <button type="submit">Zarejestruj się</button>
      </div>
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
  <style>
  * {
    box-sizing: border-box;
  }
  body {
    background: rgb(78, 138, 250);
    /* background: linear-gradient(90deg, rgb(37, 37, 40) 26%, rgb(19, 121, 9) 78%, rgb(8, 198, 113) 100%); */
    min-height: 100vh;
    display: flex;
    font-weight: 400;
  }
  body,
  html,
  .App,
  .vue-template{
    display: flex;
    justify-content: center;
    align-items: center;
    width: 150%;
    height: 150%;
    /* transform: scale(1.1); */
  }
  .vertical-center {
    width: 100%;
    height: 100%;
  }
  .card{
    padding: 30px;
  }
  .vertical-center {
    display: flex;
    text-align: left;
    justify-content: center;
    align-items: center;
    flex-direction: column;    
  }
  .inner-block {
    width: 450px;
    margin: auto;
    background: #ffffff;
    box-shadow: 0px 14px 80px rgba(34, 35, 58, 0.2);
    padding: 40px 55px 45px 55px;
    border-radius: 15px;
    transition: all .3s;
  }
  .vertical-center .form-control:focus {
    border-color: #252528;
    box-shadow: none;
  }
  .vertical-center h3 {
    text-align: center;
    margin: 0;
    line-height: 1;
    padding-bottom: 20px;
  }
  label {
    font-weight: 500;
  }
  .forgot-password,
  .forgot-password a {
    text-align: right;
    font-size: 13px;
    padding-top: 10px;
    color: #7a7a7a;
    margin: 0;
  }

</style>
  