<template>
  <NavBar />
  <!-- <Space /> -->
  <div class="vue-template">
    <form>
    <div class="card">
      <h3>Logowanie</h3>
      <div class="mb-3">
        <label for="email">Podaj swój adres Email:</label>
        <input type="email" id="email" class="form-control form-control-lg" v-model="form.email" required>
      </div>
      <div class="mb-3">
        <label for="password"> Podaj Hasło:</label>
        <input type="password" id="password" class="form-control form-control-lg" v-model="form.password" required>
      </div>
      <p>
        Zapomniałeś hasła? Kliknij 
      <RouterLink to="/forgot-password" class = "link">tutaj</RouterLink>
      </p>
      <button type="button" @click="submitForm">Zaloguj się</button>
      <p>
        Załóż konto 
      <RouterLink to="/sign-up" class = "link">tutaj</RouterLink>
      </p>
      <div v-if="formErrors && formErrors.Email" class="error">{{ formErrors.Email[0] }}</div>
      <div v-if="formErrors && formErrors.Password" class="error">{{ formErrors.Password[0] }}</div>
      <div v-if="formErrors && formErrors.general" class="error">{{ formErrors.general[0] }}</div>
      <div v-if="formErrors && formErrors.UnauthorizedAccount" class="error">{{ formErrors.UnauthorizedAccount[0] }}</div>
      <div v-if="formErrors && formErrors['nieprawidłowe dane']" class="error">{{ formErrors['nieprawidłowe dane'][0] }}</div>
    </div>
    </form>
  </div>
</template>
<script>
 import NavBar from '@/components/NavBar.vue';
 import Space from '@/components/Space.vue';
import axios from '../../../config.js';

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
        password: '',
      },
      formErrors: {}
    };
  },
  methods: {
    submitForm() {
      axios.post('/user-identity/sign-in', this.form)
        .then(response => {
          this.$router.push('/offers-list');
          console.log(response);
          localStorage.setItem('jwt', response.data.accessToken);
          SaveUserRoles(response.data.roles);
          console.log(CheckUserRole('abcd'))
          alert('Logowanie zakończone sukcesem!');
          let loginAttempts = 0;
          let blockedUntil = null;
          function isLoginBlocked() {
            return blockedUntil && blockedUntil > new Date();
        }
        if (isLoginBlocked()) {
            // Wyświetl komunikat informujący użytkownika o blokadzie możliwości logowania
            console.log('Możliwość logowania jest zablokowana. Spróbuj ponownie za minutę.');
            return;
}
        })
        .catch(error => {
    if (error.response && error.response.status === 400) {
        const responseData = error.response.data;

        if (responseData.title === "user istnieje") {
            // Jeśli użytkownik już istnieje
            console.error('Użytkownik już istnieje', responseData);
            this.formErrors = { UserAlreadyExists: ['Użytkownik o podanym adresie email już istnieje.'] };
        } else if (
            responseData.errors &&
            (responseData.errors.PasswordTooShort ||
             responseData.errors.PasswordRequiresLower ||
             responseData.errors.PasswordRequiresUpper ||
             responseData.errors.ConfirmedPassword ||
             responseData.errors.Email)
        ) {
            // Jeśli wystąpiły błędy walidacji hasła lub e-maila
            console.error('Błąd walidacji', responseData);
            this.formErrors = responseData.errors;
        } else if (responseData.title === "Unable to log in to unauthorized account.") {
            // Jeśli próbowano zalogować się na nieautoryzowane konto
            console.error('Nieautoryzowane konto', responseData);
            this.formErrors = { UnauthorizedAccount: ['Nie można zalogować się na nieautoryzowane konto.'] };
        } else {
            // Tutaj przekierowanie do innego okna, jeśli logowanie się powiodło
            console.log('Logowanie powiodło się, przekierowanie do innego okna...');
            this.$router.push('/sciezka/do/docelowego/okna');
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

/* Stylowanie formularza */
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

</style>
