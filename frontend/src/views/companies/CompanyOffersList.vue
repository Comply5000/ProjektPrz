<template>
  <NavBar />
  <div class="offer-container">
    <!-- Formularz wyszukiwania -->
    <div class="search-form">
      <input
        v-model="searchQuery"
        class="form-control search-input"
        type="search"
        placeholder="Search"
        aria-label="Search"
      />
      <button class="add-offer-button" @click="addOffer">Dodaj Ofertę</button>
    </div>
    <!-- Filtr: Typ oferty (zawsze widoczny) -->
    <div class="specific-offer-type">
      <label for="specificOfferType">Typ oferty:</label>
      <select id="specificOfferType" v-model.number="selectedOfferType">
        <option value="">Wybierz typ oferty</option>
        <option v-for="(name, value) in offerTypes" :value="value">
          {{ name }}
        </option>
      </select>
    </div>
    <!-- Lista ofert -->
    <div class="offer-list-container">
      <div class="offer-list">
        <ul>
          <li
            v-for="(offer, index) in offers"
            :key="offer.id"
            class="offer-item"
          >
            <div class="offer-details">
              <div class="offer-content">
                <div class="offer-text">
                  <h3 class="offer-name" @click="goToOffer(offer.id)">
                    {{ offer.name }}
                  </h3>
                  <p class="company-name">{{ offer.companyName }}</p>
                  <p>
                    Typ:
                    <span class="offer-type">{{
                      getOfferTypeName(offer.type)
                    }}</span>
                  </p>
                  <p>Ocena: {{ offer.rating }}</p>
                  <p>Opis: {{ offer.description }}</p>
                </div>
                <div class="offer-actions">
                  <button class="edit-button" @click="editOffer(offer.id)">
                    Edytuj
                  </button>
                  <button class="delete-button" @click="deleteOffer(offer.id)">
                    Usuń
                  </button>
                </div>
              </div>
              <div class="offer-image">
                <img
                  :src="offer.imageUrl"
                  :alt="'Offer Image ' + (index + 1)"
                  v-if="offer.imageUrl"
                />
              </div>
            </div>
          </li>
        </ul>
      </div>
    </div>
    <!-- Wybór liczby ofert na stronie -->
    <div class="offer-per-page">
      <label for="offerPerPage">Oferty na stronie:</label>
      <select v-model="pageSize" id="offerPerPage">
        <option v-for="option in pageSizeOptions" :value="option">
          {{ option }}
        </option>
      </select>
    </div>
    <!-- Paginacja -->
    <nav aria-label="Page navigation example">
      <ul class="pagination justify-content-center">
        <li class="page-item" :class="{ disabled: currentPage === 1 }">
          <button
            class="page-link"
            @click="changePage(currentPage - 1)"
            :disabled="currentPage === 1"
          >
            Previous
          </button>
        </li>
        <li
          class="page-item"
          v-for="page in totalPages"
          :key="page"
          :class="{ active: currentPage === page }"
        >
          <a class="page-link" href="#" @click.prevent="changePage(page)">{{
            page
          }}</a>
        </li>
        <li class="page-item" :class="{ disabled: currentPage === totalPages }">
          <button
            class="page-link"
            @click="changePage(currentPage + 1)"
            :disabled="currentPage === totalPages"
          >
            Next
          </button>
        </li>
      </ul>
    </nav>
  </div>
  <notification-component></notification-component>
</template>

<script>
import axios from "../../../config.js";
import NavBar from "@/components/NavBar.vue";

export default {
  components: {
    NavBar,
  },
  data() {
    return {
      currentPage: 1,
      totalPages: 1,
      pageSize: 5,
      pageSizeOptions: [5, 10, 15],
      searchQuery: "",
      selectedOfferType: "",
      selectedCompany: "",
      isLogin: false,
      offerTypes: {
        1: "Produkt",
        2: "Usługa",
        3: "Promocja",
        4: "Program lojalnościowy",
      },
      offers: [],
      companies: [],
    };
  },
  methods: {
    goToOffer(offerid) {
      this.$router.push({ path: `/offer:${offerid}` });
    },
    changePage(page) {
      if (page >= 1 && page <= this.totalPages) {
        this.currentPage = page;
        this.fetchOffers();
      }
    },
    fetchOffers() {
      const token = localStorage.getItem("jwt");
      const params = {
        pageNumber: this.currentPage,
        pageSize: this.pageSize,
        search: this.searchQuery,
        type: this.selectedOfferType,
      };

      axios
        .get("/offers/my-offers", {
          params: params,
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          this.offers = response.data.items;
          this.currentPage = response.data.pageIndex;
          this.totalPages = response.data.totalPages;
        });
    },
    fetchCompanies() {
      axios.get("/companies/all").then((response) => {
        this.companies = response.data;
      });
    },
    getOfferTypeName(value) {
      return this.offerTypes[value];
    },
    addOffer() {
      this.$router.push({ path: `/offer/new` });
    },
    editOffer(offerId) {
      this.$router.push({ path: `/offer/edit:${offerId}` });
    },
    deleteOffer(offerId) {
      const token = localStorage.getItem("jwt");
      axios
        .delete(`/offers/${offerId}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          this.fetchOffers();
        });
    },
  },
  watch: {
    currentPage() {
      this.fetchOffers();
    },
    searchQuery() {
      this.fetchOffers();
    },
    pageSize() {
      this.fetchOffers();
    },
    selectedOfferType() {
      this.fetchOffers();
    },
    selectedCompany() {
      this.fetchOffers();
    },
  },
  mounted() {
    const token = localStorage.getItem("jwt");
    if (token) {
      this.isLogin = true;
    }
    this.fetchCompanies();
    this.fetchOffers();
  },
};
</script>

<style scoped>
.offer-container {
  background-color: rgb(40, 167, 69);
  min-height: 100vh;
  display: flex;
  align-items: center;
  flex-direction: column;
  padding-top: 80px;
  overflow: auto;
}

.search-form {
  margin-top: -20px;
  margin-bottom: 10px;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 5px;
}

.search-input {
  width: 200px;
  font-size: 12px;
  padding: 4px 8px;
}

.add-offer-button {
  background-color: gray;
  color: white;
  font-size: 12px;
  padding: 4px 8px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.add-offer-button:hover {
  background-color: #5a6268;
}

.specific-offer-type,
.company-filter {
  margin-bottom: 10px;
}

input[type="checkbox"] {
  margin-left: 5px;
  width: 15px;
  height: 15px;
}

.offer-list-container {
  width: 100%;
}

.offer-list {
  background-color: #ddd;
  padding: 25px;
  margin: 0 auto;
  width: 75%;
}

.offer-item {
  list-style-type: none;
  margin-bottom: 20px;
  background-color: white;
  padding: 15px;
  border-radius: 5px;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.offer-details {
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.offer-content {
  flex-grow: 1;
  text-align: center;
}

.offer-text {
  text-align: center;
}

.offer-text h3,
.offer-text p {
  margin: -4px;
}

.offer-type {
  padding: 5px 10px;
  border-radius: 5px;
  text-align: center;
}

.offer-name {
  background-color: #28a745;
  color: white;
  padding: 5px 10px;
  border-radius: 5px;
  text-align: center;
}

.offer-image img {
  width: 110px;
  height: 110px;
  margin-top: -4px;
  border-radius: 10px;
  border: 5px solid #28a745;
}

.offer-actions {
  display: flex;
  justify-content: center;
  gap: 10px;
  margin-top: 10px;
  width: 100%;
}

.edit-button {
  background-color: #28a745;
  color: white;
  border: none;
  border-radius: 5px;
  padding: 5px 10px;
  cursor: pointer;
}

.edit-button:hover {
  background-color: #218838;
}

.delete-button {
  background-color: #dc3545;
  color: white;
  border: none;
  border-radius: 5px;
  padding: 5px 10px;
  cursor: pointer;
}

.delete-button:hover {
  background-color: #c82333;
}

.offer-per-page {
  margin: 5px 0 5px;
}

.pagination {
  margin-bottom: 10px;
}

.pagination .page-item.disabled .page-link {
  background-color: transparent;
  color: #ccc;
  border: none;
}

.pagination .page-item.active .page-link {
  background-color: #28a745;
  color: white;
  border-color: #28a745;
}

.pagination .page-item.active .page-link:hover {
  background-color: #218838;
  border-color: #1e7e34;
}
</style>
