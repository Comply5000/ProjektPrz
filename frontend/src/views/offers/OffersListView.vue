<template>
  <NavBar />
  <div class="offer-container">
    <!-- Formularz wyszukiwania -->
    <div class="search-form">
      <input v-model="searchQuery" class="form-control" type="search" placeholder="Search" aria-label="Search">
    </div>
    <!-- Filtr: Ulubione firmy (widoczny tylko dla zalogowanych użytkowników) -->
    <div v-if="isLogin" class="favorite-companies">
      <label>Filtruj tylko ulubione: </label>
      <input type="checkbox" id="favoriteCompany" v-model="favoriteCompany">
      <label for="favoriteCompany"></label>
    </div>
    <!-- Filtr: Typ oferty (zawsze widoczny) -->
    <div class="specific-offer-type">
      <label for="specificOfferType">Typ oferty:</label>
      <select id="specificOfferType" v-model="selectedOfferType">
        <option value="">Wybierz typ oferty</option>
        <option v-for="(name, value) in offerTypes" :value="value">{{ name }}</option>
      </select>
    </div>
    <!-- Lista ofert -->
    <div class="offer-list-container">
      <div class="offer-list">
        <ul>
          <li v-for="(offer, index) in paginatedOffers" :key="index" class="offer-item">
            <div class="offer-details">
              <div class="offer-content">
                <div class="offer-text">
                  <h3 class="offer-name">{{ offer.offerName }}</h3>
                  <p class="company-name">{{ offer.companyName }}</p>
                  <p>Typ: <span class="offer-type">{{ getOfferTypeName(offer.companyType) }}</span></p>
                  <p>Ocena: {{ offer.rating }}</p>
                  <p>Data wystawienia: {{ offer.issueDate }}</p>
                  <p>Data ważności: {{ offer.expiryDate }}</p>
                </div>
              </div>
              <div class="offer-image">
                <img :src="offer.image" :alt="'Offer Image ' + (index + 1)">
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
        <option v-for="option in pageSizeOptions" :value="option">{{ option }}</option>
      </select>
    </div>
    <!-- Paginacja -->
    <nav aria-label="Page navigation example">
      <ul class="pagination justify-content-center">
        <li class="page-item" :class="{ 'disabled': currentPage === 1 }">
          <button class="page-link" @click="changePage(currentPage - 1)" :disabled="currentPage === 1">Previous</button>
        </li>
        <li class="page-item" v-for="page in pageCount" :key="page" :class="{ 'active': currentPage === page }">
          <a class="page-link" href="#" @click="changePage(page)">{{ page }}</a>
        </li>
        <li class="page-item" :class="{ 'disabled': currentPage === pageCount }">
          <button class="page-link" @click="changePage(currentPage + 1)" :disabled="currentPage === pageCount">Next</button>
        </li>
      </ul>
    </nav>
  </div>
</template>

<script>
import NavBar from '@/components/NavBar.vue';

export default {
  components: {
    NavBar
  },
  data() {
    return {
      currentPage: 1,
      pageSize: 5,
      pageSizeOptions: [5, 10, 15],
      searchQuery: '',
      favoriteCompany: false,
      selectedOfferType: '',
      isLogin: false,
      offerTypes: {
        1: 'Product',
        2: 'Service',
        3: 'Promotions',
        4: 'LoyaltyProgram'
      },
      offers: [
        { offerName: 'Oferta A', companyName: 'Firma A', companyType: 1, rating: 4.5, issueDate: '2024-04-21', expiryDate: '2024-05-21', image: "../src/views/offers/flamingo.jpg" },
        { offerName: 'Oferta B', companyName: 'Firma B', companyType: 2, rating: 4.0, issueDate: '2024-04-20', expiryDate: '2024-05-20', image: "../src/views/offers/flamingo.jpg" },
        { offerName: 'Oferta C', companyName: 'Firma C', companyType: 3, rating: 3.5, issueDate: '2024-04-19', expiryDate: '2024-05-19', image: "../src/views/offers/flamingo.jpg" },
        { offerName: 'Oferta D', companyName: 'Firma D', companyType: 1, rating: 5.0, issueDate: '2024-04-18', expiryDate: '2024-05-18', image: "../src/views/offers/flamingo.jpg" },
        { offerName: 'Oferta E', companyName: 'Firma E', companyType: 2, rating: 2.5, issueDate: '2024-04-17', expiryDate: '2024-05-17', image: "../src/views/offers/flamingo.jpg" },
        { offerName: 'Oferta F', companyName: 'Firma F', companyType: 4, rating: 3.0, issueDate: '2024-04-16', expiryDate: '2024-05-16', image: "../src/views/offers/flamingo.jpg" }
      ]
    };
  },
  computed: {
    filteredOffers() {
      let filtered = this.offers.filter(offer =>
        offer.offerName.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        offer.companyName.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
      
      if (this.favoriteCompany) {
        filtered = filtered.filter(offer => offer.favorite);
      }

      if (this.selectedOfferType) {
        filtered = filtered.filter(offer => offer.companyType == this.selectedOfferType);
      }

      return filtered;
    },
    paginatedOffers() {
      const startIndex = (this.currentPage - 1) * this.pageSize;
      const endIndex = startIndex + this.pageSize;
      return this.filteredOffers.slice(startIndex, endIndex);
    },
    pageCount() {
      return Math.ceil(this.filteredOffers.length / this.pageSize);
    }
  },
  methods: {
    changePage(page) {
      if (page >= 1 && page <= this.pageCount) {
        this.currentPage = page;
      }
    },
    getOfferTypeName(value) {
      return this.offerTypes[value];
    }
  },
  mounted() {
    const token = localStorage.getItem('jwt');
    if (token) {
      this.isLogin = true;
    }
  }
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
  overflow: auto; /* Pozwala na przewijanie całej strony */
}

.search-form {
  margin-top: -20px;
  margin-bottom: 10px;
  width: 30%;
}

.favorite-companies, .specific-offer-type {
  margin-bottom: 5px; /* Odstęp między filtrami */
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
  justify-content: space-between;
  align-items: center;
}

.offer-details {
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.offer-content {
  flex-grow: 1;
}

.offer-text {
  text-align: center;
}

.offer-text h3, .offer-text p {
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
  border: 5px solid #28a745; /* Zielona ramka o szerokości 5px */
}

.offer-per-page {
  margin: 5px 0 5px; /* Dodatkowy odstęp między elementami */
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

/* Optionally, add a hover effect for active page */
.pagination .page-item.active .page-link:hover {
  background-color: #218838;
  border-color: #1e7e34;
}
</style>
