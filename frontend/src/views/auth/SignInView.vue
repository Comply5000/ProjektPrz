<template>
    <div>
      <form @submit.prevent="submitForm">
        <div>
          <label for="email">Podaj swój adres Email:</label>
          <input type="email" id="email" v-model="form.email" required>
        </div>
        <div>
          <label for="password"> Podaj Hasło:</label>
          <input type="password" id="password" v-model="form.password" required>
        </div>
        <button type="submit">Zaloguj się</button>
      </form>
    </div>
  </template>
  
  <script>
  import axios from '../../../config.js';

  export default {
    name: 'LoginForm',
    data() {
      return {
        email: '',
        password: ''
      };
    },
    methods: {
      submitForm() {
        axios.post('/user-identity/sign-in', this.form)
        .then(response => {
            alert('Zalogowano pomyślnie!');
        })
        .catch(error =>{
            console.error('Wystąpił błąd podczas logowania', error);
            alert('Wystąpił błąd podczas lgowania', +error.response.data.message);
        })
      }
    }
  };
  </script>
  
  <style scoped>
  .login-form {
    max-width: 300px;
    margin: 0 auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
  }
  
  .form-group {
    margin-bottom: 20px;
  }
  
  label {
    display: block;
    margin-bottom: 5px;
  }
  
  input {
    width: 100%;
    padding: 8px;
    border-radius: 3px;
    border: 1px solid #ccc;
  }
  
  button {
    width: 100%;
    padding: 10px;
    background-color: #007bff;
    color: #fff;
    border: none;
    border-radius: 3px;
    cursor: pointer;
  }
  
  button:hover {
    background-color: #0056b3;
  }
  </style>