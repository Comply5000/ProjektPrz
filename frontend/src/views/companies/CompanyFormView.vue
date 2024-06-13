<template>
    <NavBar />
    <div class="container">
        <h2>Aktualizacja danych firmy</h2>
        <form @submit.prevent="send">
            <div class="form-group">
                <label for="field1">Nazwa:</label>
                <input type="text" class="form-control" :class="{'is-invalid': formErrors.name}" id="field1" v-model="form.name">
                <div v-if="formErrors.name" class="invalid-feedback">{{ formErrors.name }}</div>
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
                        aspectRatio: 1,
                    }"
                    :default-size="{width: 10000, height: 10000}"
                    @change="change"
                />
                <input type="file" @change="onFileChange" accept="image/*" /> <br>
                <button class="reset-image-button" type="button" @click="resetImage">Resetuj obraz</button>
            </div>
            <button type="submit" class="btn btn-primary">Wyślij</button>
        </form>
    </div>
    <notification-component></notification-component>
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
    async send() {
      const formData = new FormData();

      formData.append('name', this.form.name);
      formData.append('localization', this.form.localization);
      formData.append('description', this.form.description);

      if (this.canvas) {
        await new Promise((resolve) => {
          this.canvas.toBlob((blob) => {
            formData.append('image', blob, "cropped-image.png");
            resolve();
          }, 'image/png');
        });
      }

      console.log(formData);

      const token = localStorage.getItem('jwt');

      axios.put(`/companies`, formData, {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'multipart/form-data'
        }
      })
      .then(response => {
        this.formErrors = {};
        this.fetch();
        this.$store.dispatch('showNotification', { message: 'Firma zaktualizowana pomyślnie!'});
      })
      .catch(error => {
        if (error.response && error.response.data) {
          const serverErrors = error.response.data.errors;
          const serverTitle = error.response.data.title;

          if (serverErrors && serverErrors.Name) {
            this.formErrors.name = serverErrors.Name[0];
          } else if (serverTitle && serverTitle.includes('firma o takiej nazwie już istnieje')) {
            this.formErrors.name = serverTitle;
          }
        } else {
          console.error(error);
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
    resetImage() {
      this.img =  '';
      this.canvas = null;
    },
    fetch() {

        const token = localStorage.getItem('jwt');
        axios.get(`/companies/update`, {
          headers: {
            'Authorization': `Bearer ${token}`
          }
        })
        .then(response => {
            this.form.name = response.data.name;
            this.form.description = response.data.description ?? '';
            this.form.localization = response.data.localization ?? '';

            const imageUrl = response.data.imageUrl;
            if (imageUrl) {
                const img = new Image();
                img.crossOrigin = 'Anonymous';
                img.onload = () => {
                const canvas = document.createElement('canvas');
                const ctx = canvas.getContext('2d');
                canvas.width = img.width;
                canvas.height = img.height;
                ctx.drawImage(img, 0, 0);
                    this.img = canvas.toDataURL('image/png');
                };
                img.onerror = () => {
                };
                img.src = imageUrl;

                this.$nextTick(() => {
                    this.$refs.cropper.fitToImage();
                });
            }
        });
    }
  },
  mounted() {
    this.fetch();
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

.is-invalid {
  border-color: #dc3545; /* Czerwona ramka dla błędnych pól */
}

.invalid-feedback {
  color: #dc3545; /* Czerwony tekst dla komunikatów o błędach */
  font-size: 80%;
  margin-top: 0.25rem;
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
