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

      <div v-if="formErrors.PasswordTooShort" class="error">{{ formErrors.PasswordTooShort[0] }}</div>
        <div v-if="formErrors.PasswordRequiresDigit" class="error">{{ formErrors.PasswordRequiresDigit[0] }}</div>
        <div v-if="formErrors.PasswordRequiresUpper" class="error">{{ formErrors.PasswordRequiresUpper[0] }}</div>
        <div v-if="formErrors.PasswordRequiresLower" class="error">{{ formErrors.PasswordRequiresLower[0] }}</div>
        <div v-if="formErrors.ConfirmedPassword" class="error">{{ formErrors.ConfirmedPassword[0] }}</div>
        <div v-if="formErrors.UserAlreadyExists" class="error">{{ formErrors.UserAlreadyExists[0] }}</div>
        <div v-if="formErrors.CompanyAlreadyExists" class="error">{{ formErrors.CompanyAlreadyExists[0] }}</div>
        <div v-if="formErrors && formErrors.Email" class="error">{{ formErrors.Email[0] }}</div>
        <div v-if="formErrors && formErrors.CompanyName" class="error">{{ formErrors.CompanyName[0] }}</div>
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
          companyName: '',
          password: '',
          confirmedPassword: ''
        },
        formErrors: {}
      };
    },
    methods: {
      submitForm() {
        console.log("qwer")

        axios.post('/user-identity/sign-up-company', this.form)
          .then(response => {
            alert('Rejestracja zakończona sukcesem!');
            this.formErrors = {};
          })
          .catch(error => {
    if (error.response && error.response.status === 400) {
        const responseData = error.response.data;

        if (
            responseData.errors &&
            (responseData.errors.PasswordTooShort ||
             responseData.errors.PasswordRequiresLower ||
             responseData.errors.PasswordRequiresUpper ||
             responseData.errors.ConfirmedPassword)
        ) {
            // Jeśli wystąpiły błędy walidacji hasła
            console.error('Błąd walidacji hasła', responseData);
            this.formErrors = responseData.errors;
        } else if (
            responseData.errors &&
            responseData.errors.Email
        ) {
            // Jeśli wystąpił błąd walidacji adresu email
            console.error('Błąd walidacji adresu email', responseData);
            this.formErrors = responseData.errors;
        } else if (
            responseData.title === "user istnieje"
        ) {
            // Jeśli użytkownik już istnieje
            console.error('Użytkownik już istnieje', responseData);
            this.formErrors = { UserAlreadyExists: ['Użytkownik o podanym adresie email już istnieje.'] };
        } else if (
            responseData.title === "firma o takiej nazwie już istnieje"
        ) {
            // Jeśli firma o takiej nazwie już istnieje
            console.error('Firma o takiej nazwie już istnieje', responseData);
            this.formErrors = { CompanyAlreadyExists: ['Firma o podanej nazwie już istnieje.'] };
        } else if (
            responseData.title === "One or more validation errors occurred." &&
            responseData.errors &&
            responseData.errors.CompanyName
        ) {
            // Jeśli błąd dotyczy pustego pola CompanyName
            console.error('Pole nazwy firmy jest wymagane', responseData);
            this.formErrors = responseData.errors;
        } else {
            // Jeśli inne błędy
            console.error('Inny błąd podczas rejestracji', responseData);
            this.formErrors = { general: ['Wystąpił błąd podczas rejestracji.'] };
        }
    } else {
        // Jeśli błąd nie jest odpowiedzią z serwera
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

.error {
  color: red;
}

</style>
  