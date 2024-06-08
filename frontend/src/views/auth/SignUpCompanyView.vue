<template>
  <NavBar />
  <!-- <Space /> -->
    <div class="vue-template">
      <form>
      <div class="card">
        <h3>Rejestracja</h3>
        <div class="mb-3">
          <label for="email">Podaj swój adres Email:</label>
          <input type="email" id="email" class="form-control form-control-lg" v-model="form.email" required>
        </div>
        <div class="mb-3">
          <label for="companyName"> Podaj nazwę swojej firmy:</label>
          <input type="companyName" id="companyName" class="form-control form-control-lg" v-model="form.companyName" required>
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
        <p>
        Masz już konto? Zaloguj się  
      <RouterLink to="/sign-in" class = "link">tutaj</RouterLink>
      </p>
      <div v-if="errors">
          <ul style="list-style-type: none; margin: 0; padding: 0; margin-top: 10px;">
            <li v-for="errorMsg in errors" :key="errorMsg" class="error-message">
              <div class="error">
                  {{ errorMsg }}
              </div>
            </li>
          </ul>
      </div>
      </div>
      </form>
    </div>
  </template>
  <script>
   import axios from '../../../config.js';
   import NavBar from '@/components/NavBar.vue';
 import Space from '@/components/Space.vue';
 import { handleErrors } from '../../../errorHandler.js';
  
  export default {
    name: 'RegisterForm',
    components: {
      NavBar,
      Space
    },
    data() {
      return {
        form: {
          email: '',
          companyName: '',
          password: '',
          confirmedPassword: ''
        },
        errors: []
      };
    },
    methods: {
      submitForm() {
        this.errors = [];
        console.log("qwer")

        axios.post('/user-identity/sign-up-company', this.form)
          .then(response => {
            alert('Na twój adres mailowy został wysłany link aktywacyjny. Aktywuj swoje konto by móc korzystać z platformy.');
            this.$router.push('/sign-in');
            this.formErrors = {};
          })
          .catch(error => {
            const errors = [];
            handleErrors(error, errors);
            this.errors = this.errors.concat(errors);
          });
      }
    }
  };
  </script>
  <style>
/* Globalne ustawienia */
.vue-template {
  background: rgb(56, 160, 96);
  min-height: 100vh;
  font-weight: 400;
  padding-top: 80px;
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

.error {
  color: red;
}

</style>
  