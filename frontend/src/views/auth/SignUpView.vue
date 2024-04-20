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
        <button type="button" @click="submitForm">Zarejestruj się</button>

        <div v-if="formErrors.PasswordTooShort" class="error">{{ formErrors.PasswordTooShort[0] }}</div>
        <div v-if="formErrors.PasswordRequiresDigit" class="error">{{ formErrors.PasswordRequiresDigit[0] }}</div>
        <div v-if="formErrors.PasswordRequiresUpper" class="error">{{ formErrors.PasswordRequiresUpper[0] }}</div>

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
        },
        formErrors: {}
      };
    },
    methods: {
      submitForm() {
        console.log("qwer")

        axios.post('/user-identity/sign-up', this.form)
          .then(response => {
            alert('Rejestracja zakończona sukcesem!');
            this.formErrors = {};
          })
          .catch(error => {
            if (error.response && error.response.status === 400) {
              this.formErrors = error.response.data.errors; // Zapisz błędy w stanie komponentu
            } else {
              console.error('Wystąpił błąd podczas rejestracji', error);
              this.formErrors = { general: ['Wystąpił nieoczekiwany błąd podczas rejestracji.'] };
            }
          });
      }
    }
  };
  </script>
  <style>
/* Globalne ustawienia */
body, html {
  background: rgb(78, 138, 250);
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

.error {
  color: red;
}

</style>
  