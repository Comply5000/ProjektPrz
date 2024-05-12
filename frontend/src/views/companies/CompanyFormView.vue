<template>
    <NavBar />
    <div class="container">
        <h2>Aktualizacja danych firmy</h2>
        <form>
            <div class="form-group">
                <label for="field1">Nazwa:</label>
                <input type="text" class="form-control" id="field1" v-model="form.name">
            </div>
            <div class="form-group">
                <label for="field2">Lokalizacja:</label>
                <input type="text" class="form-control" id="field2" v-model="form.localization">
            </div>
            <div class="form-group">
                <label for="textareaField">Opis:</label>
                <textarea class="form-control" id="textareaField" rows="3" v-model="form.description"></textarea>
            </div>

            <div class="image-input">
                <label for="textareaField">Zdjęcie:</label>
                <cropper
                    class="cropper"
                    :src="img"
                    :stencil-props="{
                        aspectRatio: 1/1
                    }"
                    @change="change"
                />
                <input type="file" @change="onFileChange" accept="image/*" /> <br>
                <button class="reset-image-button" type="button" @click="resetImage">Resetuj obraz</button>
            </div>
            <button type="submit" class="btn btn-primary">Wyślij</button>
        </form>
    </div>
</template>

<script>
    import axios from '../../../config.js';
    import NavBar from '@/components/NavBar.vue';
    import Space from '@/components/Space.vue';
    import { Cropper } from 'vue-advanced-cropper';
    import 'vue-advanced-cropper/dist/style.css';
  
  export default {
    name: 'RegisterForm',
    components: {
      NavBar,
      Space,
      Cropper
    },
    data() {
      return {
        form: {
          name: '',
          localization: '',
          description: ''
        },
        formErrors: {},
        img: '',  // Początkowo puste
		canvas: null,  // Przechowuje canvas z wyciętym obrazem
      };
    },
    methods: {
      submitForm() {
        axios.post('/user-identity/sign-up', this.form)
          .then(response => {
            alert('Rejestracja zakończona sukcesem!');
            this.formErrors = {};
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
            } else {
                // Jeśli inne błędy
                console.error('Błąd podczas rejestracji', responseData);
                this.formErrors = { general: ['Wystąpił błąd podczas rejestracji.'] };
            }
        } else {
            // Jeśli błąd nie jest odpowiedzią z serwera
            console.error('Wystąpił błąd podczas rejestracji', error);
            this.formErrors = { general: ['Wystąpił nieoczekiwany błąd podczas rejestracji.'] };
        }
        });
      },
        change({ coordinates, canvas }) {
                this.canvas = canvas;  // Zapisz canvas, by można było go wysłać później
            },
        onFileChange(event) {
            const file = event.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = (e) => {
                    this.img = e.target.result;
                };
                reader.readAsDataURL(file);
            }
        },
        resetImage()
        {
            this.img =  '';
            this.canvas = null;
        }
    }
  };
  </script>



<style>
body {
    background-color: rgb(56, 160, 96); /* Jasnozielony kolor tła tylko dla sekcji wokół formularza */
}

.image-input {
    padding-top: 20px;
    padding-bottom: 20px;
}

.cropper {
	height: 300px;
	width: 300px;
	background: #DDD;
}
/* Stylowanie kontenera */
.container {
    margin-top: 80px;
    background-color: #f8f9fa; /* Jasnoszary tło */
    border: 1px solid #ddd; /* Cienka, solidna, jasnoszara ramka */
    padding: 20px; /* Wewnętrzny odstęp */
    border-radius: 8px; /* Zaokrąglone rogi */
    box-shadow: 0 2px 5px rgba(0,0,0,0.1); /* Subtelny cień */
}

/* Stylowanie nagłówka */
h2 {
    color: #333; /* Ciemnoszary kolor tekstu */
    text-align: center; /* Wyśrodkowany tekst */
    margin-bottom: 20px; /* Odstęp na dole */
}

/* Stylowanie pól formularza */
.form-control {
    border: 1px solid #ccc; /* Cienka, solidna, jasnoszara ramka */
    border-radius: 4px; /* Zaokrąglone rogi */
    padding: 10px 15px; /* Odstępy wewnątrz pól */
}

/* Stylowanie pól po aktywacji (focus) */
.form-control:focus {
    border-color: #0056b3; /* Ciemnoniebieska ramka */
    box-shadow: 0 0 0 0.2rem rgba(0,86,179,0.25); /* Rozmycie w kolorze niebieskim */
}

/* Stylowanie przycisku */
.btn-primary {
    background-color: #007bff; /* Niebieskie tło */
    border-color: #007bff; /* Niebieska ramka */
    color: white; /* Biały tekst */
    padding: 10px 20px; /* Odstępy wewnątrz przycisku */
    border-radius: 5px; /* Zaokrąglone rogi */
    cursor: pointer; /* Kursor wskazujący */
}

.btn-primary:hover {
    background-color: #0056b3; /* Ciemniejszy niebieski przy najechaniu */
}

.reset-image-button {
    padding-top: 5px;
}

</style>