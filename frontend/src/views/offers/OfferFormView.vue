<template>
    <NavBar />
    <div class="container">
        <h2 v-if="!isEditMode">Tworzenie oferty</h2>
        <h2 v-if="isEditMode">Aktualizacja oferty</h2>
        <form @submit.prevent="send">
            <div class="form-group">
                <label for="field1">Nazwa:</label>
                <input type="text" class="form-control" :class="{'is-invalid': formErrors.name}" id="field1" v-model="form.name">
                <div v-if="formErrors.name" class="invalid-feedback">{{ formErrors.name }}</div>
            </div>
            <div class="data-filter">
                <label for="startDate">Data od:</label>
                <input type="date" id="startDate" v-model="form.dateFrom" :class="{'is-invalid': formErrors.dateFrom}">
                <div v-if="formErrors.dateFrom" class="invalid-feedback">{{ formErrors.dateFrom }}</div>
                <label for="endDate">Data do:</label>
                <input type="date" id="endDate" v-model="form.dateTo" :class="{'is-invalid': formErrors.dateTo}">
                <div v-if="formErrors.dateTo" class="invalid-feedback">{{ formErrors.dateTo }}</div>
            </div>
            <div class="specific-offer-type">
                <label for="specificOfferType">Typ oferty:</label>
                <select id="specificOfferType" v-model="form.type" :class="{'is-invalid': formErrors.type}">
                    <option value="null">Wybierz typ oferty</option>
                    <option v-for="(name, value) in offerTypes" :value="value">{{ name }}</option>
                </select>
                <div v-if="formErrors.type" class="invalid-feedback">{{ formErrors.type }}</div>
            </div>
            <div class="form-group">
                <label for="textareaField">Opis:</label>
                <textarea class="form-control" id="textareaField" rows="3" v-model="form.description" :class="{'is-invalid': formErrors.description}"></textarea>
                <div v-if="formErrors.description" class="invalid-feedback">{{ formErrors.description }}</div>
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
            <button type="submit" class="btn btn-primary">Zapisz</button>
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
import moment from 'moment';
import 'moment-timezone';

export default {
  name: 'RegisterForm',
  components: {
    NavBar,
    Space,
    Cropper,
  },
  data() {
    return {
      form: {
        name: '',
        description: '',
        dateTo: '',
        dateFrom: '',
        type: null

      },
      formErrors: {},
      img: '',  // Początkowo puste
      canvas: null,  // Przechowuje canvas z wyciętym obrazem
      offerTypes: {
        1: 'Produkt',
        2: 'Usługa',
        3: 'Promocja',
        4: 'Program lojalnościowy'
      },
    };
  },
  computed:{
    isEditMode(){
        return this.$route.params.id !== undefined;
    }
  },
  methods: {
    async send() {

      this.formErrors = {};
      const formData = new FormData();

      const momentDateTo = moment(this.form.dateTo, 'YYYY-MM-DD');
      const formattedDateTo = momentDateTo.isValid() ? momentDateTo.toISOString() : '';
      const momentDateFrom = moment(this.form.dateFrom, 'YYYY-MM-DD');
      const formattedDateFrom = momentDateFrom.isValid() ? momentDateFrom.toISOString() : '';

      formData.append('name', this.form.name);
      formData.append('description', this.form.description);
      formData.append('dateTo', formattedDateTo);
      formData.append('dateFrom', formattedDateFrom);
      formData.append('type', this.form.type);

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

      if(this.isEditMode){
        const offerId = this.$route.params.id;
        axios.put(`/offers/${offerId.slice(1)}`, formData, {
            headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'multipart/form-data'
            }
        })
        .then(response => {
            this.formErrors = {};
            this.fetch(response.data.id);
            this.$store.dispatch('showNotification', { message: 'Oferta zaktualizowana pomyslnie!'});
        })
        .catch(error => {
            if (error.response && error.response.data) {
            const serverErrors = error.response.data.errors;
            const serverTitle = error.response.data.title;

            if (serverErrors) {
                this.formErrors.name = serverErrors.Name ? serverErrors.Name[0] : '';
                this.formErrors.description = serverErrors.Description ? serverErrors.Description[0] : '';
                this.formErrors.dateTo = serverErrors.DateTo ? serverErrors.DateTo[1] : '';
                this.formErrors.dateFrom = serverErrors.DateFrom ? serverErrors.DateFrom[1] : '';
                this.formErrors.type = serverErrors.DateFrom ? serverErrors.DateFrom[1] : '';
                console.log(this.formErrors);
            } else if (serverTitle && serverTitle.includes('oferta o takiej nazwie już istnieje')) {
                this.formErrors.name = serverTitle;
            }
            } else {
            console.error(error);
            }
        });
      }

      else{
      axios.post(`/offers`, formData, {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'multipart/form-data'
        }
      })
      .then(response => {
        this.formErrors = {};
        this.$store.dispatch('showNotification', { message: 'Oferta utworzona pomyslnie!'});
        this.$router.push({ path: `/offer/edit:${response.data.id}` }).then(() => {
            this.$nextTick(() => {
                this.fetch(response.data.id);
            });
        });
      })
      .catch(error => {
        if (error.response && error.response.data) {
          const serverErrors = error.response.data.errors;
          const serverTitle = error.response.data.title;

          if (serverErrors) {
            this.formErrors.name = serverErrors.Name ? serverErrors.Name[0] : '';
            this.formErrors.description = serverErrors.Description ? serverErrors.Description[0] : '';
            this.formErrors.dateTo = serverErrors.DateTo ? serverErrors.DateTo[1] : '';
            this.formErrors.dateFrom = serverErrors.DateFrom ? serverErrors.DateFrom[1] : '';
            this.formErrors.type = serverErrors.DateFrom ? serverErrors.DateFrom[1] : '';
            console.log(this.formErrors);
          } else if (serverTitle && serverTitle.includes('oferta o takiej nazwie już istnieje')) {
            this.formErrors.name = serverTitle;
          }
        } else {
          console.error(error);
        }
      });
    }
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
    fetch(offerId) {

        const token = localStorage.getItem('jwt');
        axios.get(`/offers/${offerId}/update`, {
          headers: {
            'Authorization': `Bearer ${token}`
          }
        })
        .then(response => {
            this.form.name = response.data.name;
            this.form.description = response.data.description ?? '';
            this.form.type = response.data.type ?? '';

            const momentDateTotimeoffset = moment(response.data.dateTo);
            const momentWarsawTimeDateTo = momentDateTotimeoffset.tz("Europe/Warsaw");
            this.form.dateTo = momentWarsawTimeDateTo.format("YYYY-MM-DD");

            const momentDateFromtimeoffset = moment(response.data.dateFrom);
            const momentWarsawTimeDateFrom = momentDateFromtimeoffset.tz("Europe/Warsaw");
            this.form.dateFrom = momentWarsawTimeDateFrom.format("YYYY-MM-DD");

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
  created() {
    if(this.isEditMode){
        const offerId = this.$route.params.id;
        this.fetch(offerId.slice(1));
        console.log(offerId.slice(1));
    } 
  }

};
</script>

<style>
body {
  background-color: rgb(56, 160, 96); /* Jasnozielony kolor tła tylko dla sekcji wokół formularza */
}

.data-filter{
    padding-top: 10px;
    padding-bottom: 10px;
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
