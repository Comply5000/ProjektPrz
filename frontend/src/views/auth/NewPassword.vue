<template>
    <NavBar />
    <div class="vue-template">
      <form @submit.prevent="submitForm">
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
        <button type="submit">Zmień</button>

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
  import { handleErrors } from '../../../errorHandler.js';
  
  export default {
    name: 'new-password',
    components: {
      NavBar
    },
    data() 
    {
      return {
        form: {
            password: '',
            confirmedPassword: '',
        },
        errors: [],
      };
    },
    methods: {
      submitForm() {
        this.errors = [];

        const token = this.$route.query.token;
        const userId = this.$route.query.userId;
        axios.post(`/user-identity/reset-password`, {
            password: this.form.password,
            confirmedPassword: this.form.confirmedPassword,
            token: token.replace(/\s/g, '+'),
            userId: userId
        })
        .then(response => {
          alert('Hasło zostało zmienione');
          this.$router.push('/sign-in');
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
  <style scoped>
  /* Globalne ustawienia */
  body, html {
  background: rgb(40, 167, 69);
  min-height: 100vh;
  font-weight: 400;
  display: flex;
  justify-content: center;
  align-items: center;
  }
  
  /* Stylowanie formularza */
  .vue-template {
  background: rgb(40, 167, 69);
  min-height: 100vh;
  font-weight: 400;
  padding-top: 80px;
}
  .link{
    color: rgb(40, 167, 69);
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
  