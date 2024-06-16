<template>
  <NavBar />
  <div class="company-container">
    <div class="company-main">
      <div class="company-details">
        <div class="company-left">
          <h3 class="company-name">
            {{ company.name }}
            <span @click="toggleFavorite" class="favorite-star" v-if="isUser()">
              <i
                :class="company.isFavorite ? 'fas fa-star' : 'far fa-star'"
              ></i>
            </span>
          </h3>

          <p>Lokalizacja: {{ company.localization }}</p>
        </div>
        <div class="company-image">
          <img :src="company.imageUrl" :alt="'Company Image'" />
        </div>
      </div>
      <div class="company-description">
        {{ company.description }}
      </div>
    </div>
  </div>
  <notification-component></notification-component>
</template>

<script>
import { CheckUserRole } from "../../services/UserService.js";
import axios from "../../../config.js";
import NavBar from "@/components/NavBar.vue";
import Space from "@/components/Space.vue";
export default {
  components: {
    NavBar,
  },
  data() {
    return {
      company: {
        name: "",
        localization: "",
        imageUrl: "",
        description: ``,
        id: null,
        isFavorite: true,
      },
    };
  },
  methods: {
    toggleFavorite() {
      const token = localStorage.getItem("jwt");
      console.log(token);
      axios
        .put(
          `/companies/${this.company.id}/add-to-favourite`,
          {},
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        )
        .then((response) => {
          let message = "";
          if (this.company.isFavorite === false)
            message = "Firma została dodana do ulubionych";
          else message = "Firma została usunięta z ulubionych";

          this.$store.dispatch("showNotification", {
            message: message,
          });
          this.fetch();
        });
      // Tu możesz dodać logikę do zapisu stanu ulubionych, np. zapisu do lokalnej pamięci lub wysłania do serwera
    },
    fetch() {
      const companyid = this.$route.params.id;
      const token = localStorage.getItem("jwt");
      axios
        .get(`/companies/${companyid.slice(1)}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          this.company.name = response.data.name;
          this.company.description = response.data.description ?? "";
          this.company.localization = response.data.localization ?? "";
          this.company.imageUrl = response.data.imageUrl;
          this.company.isFavorite = response.data.favourite;
          this.company.id = response.data.id;
        });
    },
    isUser() {
      return CheckUserRole("User");
    },
  },
  mounted() {
    this.fetch();
  },
};
</script>

<style scoped>
body {
  margin: 0;
  padding: 0;
}

.company-container {
  background-color: rgb(40, 167, 69);
  min-height: 100vh;
  display: flex;
  align-items: center;
  flex-direction: column;
  margin-top: 0px;
  padding-top: 40px;
}

.company-main {
  background-color: #ddd;
  color: white;
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 80%; /* Szerokość pozostaje taka sama */
  padding: 50px; /* Zwiększony padding, aby rozciągnąć tło */
  padding-top: 55px;
  text-align: center;
  box-sizing: border-box;
  margin-top: 20px; /* Dodatkowe przesunięcie w dół */
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
  border-radius: 10px;
}

.company-details {
  width: 100%;
  display: flex;
  justify-content: space-between;
  background-color: white;
  font-weight: bold;
  font-size: 20px;
  padding: 20px; /* Zwiększony padding */
  box-sizing: border-box;
  margin-bottom: 20px;
  border-radius: 10px;
}

.company-left {
  width: 60%; /* Zmniejszenie szerokości dla lepszej proporcji */
  background-color: white;
  color: black;
  text-align: left;
}

.company-image {
  width: 30%; /* Zwiększenie szerokości dla lepszej proporcji */
}

.company-image img {
  width: 100%;
  height: auto;
  border-radius: 10px;
}

.company-description {
  background-color: white;
  color: black;
  width: 100%;
  font-size: 20px;
  font-weight: bold;
  padding: 40px; /* Zwiększony padding, aby rozciągnąć tło */
  box-sizing: border-box;
  margin-bottom: 20px;
  border-radius: 10px;
}

.favorite-star {
  margin-left: 10px;
  cursor: pointer;
  color: gold;
}

.favorite-star .fa-star {
  font-size: 24px;
}

.company-type {
  padding: 5px 10px;
  border-radius: 5px;
  text-align: center;
}

.company-name {
  background-color: #28a745;
  color: white;
  padding: 5px 10px;
  border-radius: 5px;
  text-align: center;
  display: flex;
  align-items: center;
}

.stars {
  color: gold;
}
</style>
