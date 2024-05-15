<template>
    <div class="vue-template">
      <form>
      <div class="card">
        <h3>Zmiana hasła</h3>
        <div class="mb-3">
          <label for="password"> Podaj nowe Hasło:</label>
          <input type="password" id="password" class="form-control form-control-lg" v-model="form.password" required>
        </div>
        <div class="mb-3">
          <label for="confirmedPassword">Potwierdź hasło:</label>
          <input type="password" id="confirmedPassword" class="form-control form-control-lg" v-model="form.confirmedPassword" required>
        </div>
        <button type="button" @click="submitForm">Zmień</button>
      </div>
      <div v-if="formErrors && formErrors.PasswordTooShort" class="error">{{ formErrors.PasswordTooShort[0] }}</div>
          <div v-if="formErrors && formErrors.PasswordRequiresDigit" class="error">{{ formErrors.PasswordRequiresDigit[0] }}</div>
          <div v-if="formErrors && formErrors.PasswordRequiresUpper" class="error">{{ formErrors.PasswordRequiresUpper[0] }}</div>
          <div v-if="formErrors && formErrors.PasswordRequiresLower" class="error">{{ formErrors.PasswordRequiresLower[0] }}</div>
          <div v-if="formErrors && formErrors.ConfirmedPassword" class="error">{{ formErrors.ConfirmedPassword[0] }}</div>
      </form>
    </div>
  </template>
  <script>
  import axios from '../../../config.js';
  
  export default {
    name: 'new-password',
    data() 
    {
      return {
        form: {
            password: '',
            confirmedPassword: '',
        },
        formErrors: {}
      };
    },
    methods: {
      submitForm() {
        axios.post('/user-identity/new-password', {password: this.form.password})
  .then(response => {
    alert('Hasło zostało zmienione');
  })
  .catch(error => {
          this.formErrors = {}; // Resetowanie błędów

          if (error.response && error.response.status === 400) {
            const responseData = error.response.data;

            // Przypisanie błędów do formErrors na podstawie odpowiedzi serwera
            this.formErrors = responseData.errors || {};

          } else {
            console.error('Wystąpił błąd podczas zmiany hasła', error);
            this.formErrors.general = ['Wystąpił nieoczekiwany błąd podczas zmiany hasła.'];
          }
        });
      }
    }
  };
  </script>
  <style>
  /* Globalne ustawienia */
  body, html {
  background: rgb(56, 160, 96);
  min-height: 100vh;
  font-weight: 400;
  display: flex;
  justify-content: center;
  align-items: center;
  }
  
  /* Stylowanie formularza */
  .vue-template {
  display: flex;
  justify-content: center;
  align-items: center;
  }
  .link{
    color: rgb(56, 160, 96);
  }
  
  .card {
  width: 450px; /* Zdefiniowanie szerokości karty */
  background: #ffffff;
  box-shadow: 0px 14px 80px rgba(34, 35, 58, 0.2);
  padding: 30px;
  border-radius: 15px;
  margin: auto; /* Wyśrodkowanie w kontenerze flex */
  }
  
  .form-control {
  width: 100%; /* Pełna szerokość w kontekście rodzica */
  margin-bottom: 20px; /* Odległość między polami formularza */
  }
  
  .form-control:focus {
  border-color: #252528;
  box-shadow: none;
  }
  
  /* Nagłówek w formularzu */
  .card h3 {
  text-align: center;
  margin: 0 0 20px 0; /* Usunięcie marginesu dolnego */
  line-height: 1;
  }
  
  /* Etykiety */
  label {
  display: block; /* Etykiety w nowej linii */
  font-weight: 500;
  margin-bottom: 5px; /* Margines dla lepszej czytelności */
  }
  
  /* Przyciski i linki */
  button {
  width: 100%; /* Pełna szerokość */
  padding: 10px; /* Wygodniejsze klikanie */
  margin-top: 10px; /* Odległość od ostatniego elementu formularza */
  }
  
  .forgot-password,
  .forgot-password a {
  text-align: right;
  font-size: 13px;
  color: #7a7a7a;
  margin: 10px 0 0 0; /* Usunięcie domyślnego marginesu */
  }
  
  /* Usunięcie niepotrzebnych klas */
  .vertical-center, .inner-block {
  display: none;
  }
  
  h3 {
  font-weight: bold;
  }
  
  </style>
  