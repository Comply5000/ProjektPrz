<template>
  <NavBar />
  <!-- <Space /> -->
  <div class="offer-container">
    <!-- Formularz wyszukiwania -->
    <div class="search-form">
      <input v-model="searchQuery" class="form-control" type="search" placeholder="Search" aria-label="Search">
    </div>
    <!-- Przerwa po polu wyszukiwania -->
    <div class="filter-separator"></div>
    <!-- Filtr: Zakres dat -->
    <div class="date-filter">
      <label for="startDate">Data od:</label>
      <input type="date" id="startDate" v-model="startDate">
      <label for="endDate">Data do:</label>
      <input type="date" id="endDate" v-model="endDate">
    </div>
    <!-- Filtr: Ulubione firmy -->
    <div class="favorite-companies">
      <label>Ulubione firmy:</label>
      <input type="checkbox" id="favoriteCompany" v-model="favoriteCompany">
      <label for="favoriteCompany">Tylko ulubione</label>
    </div>
    <!-- Filtr: Firma konkretna -->
    <div class="specific-company">
      <label for="specificCompany">Firma konkretna:</label>
      <select id="specificCompany" v-model="selectedCompany">
        <option value="">Wybierz firmę</option>
        <option v-for="company in companies" :value="company">{{ company }}</option>
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
                  <p>Typ: <span class="offer-type">{{ offer.companyType }}</span></p>
                  <p>Data wystawienia: {{ offer.issueDate }}</p>
                  <p>Data ważności: {{ offer.expiryDate }}</p>
                </div>
              </div>
              <div class="offer-image">
                <img :src= "offer.image" :alt="'Offer Image ' + (index + 1)">
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
        <li class="page-item">
          <button class="page-link" @click="changePage(currentPage - 1)" :disabled="currentPage === 1">Previous</button>
        </li>
        <li class="page-item" v-for="page in pageCount" :key="page">
          <a class="page-link" href="#" @click="changePage(page)">{{ page }}</a>
        </li>
        <li class="page-item">
          <button class="page-link" @click="changePage(currentPage + 1)" :disabled="currentPage === pageCount">Next</button>
        </li>
      </ul>
    </nav>
  </div>
</template>

<script>
import NavBar from '@/components/NavBar.vue';
import Space from '@/components/Space.vue';

export default {
  components: {
    NavBar,
    Space
  },
  data() {
    return {
      currentPage: 1,
      pageSize: 5,
      pageSizeOptions: [5, 10, 15],
      searchQuery: '',
      startDate: '',
      endDate: '',
      favoriteCompany: false,
      selectedCompany: '',
      companies: ['Firma A', 'Firma B', 'Firma C', 'Firma D', 'Firma E', 'Firma F'],
      offers: [
        { offerName: 'Oferta A', companyName: 'Firma A', companyType: 'Gastronomia', issueDate: '2024-04-21', expiryDate: '2024-05-21', image: "../src/views/offers/flamingo.jpg" },
        { offerName: 'Oferta B', companyName: 'Firma B', companyType: 'Usługi', issueDate: '2024-04-20', expiryDate: '2024-05-20', image: "../src/views/offers/flamingo.jpg" },
        { offerName: 'Oferta C', companyName: 'Firma C', companyType: 'Wynajem', issueDate: '2024-04-19', expiryDate: '2024-05-19', image: "../src/views/offers/flamingo.jpg" },
        { offerName: 'Oferta D', companyName: 'Firma D', companyType: 'Gastronomia', issueDate: '2024-04-18', expiryDate: '2024-05-18', image: "../src/views/offers/flamingo.jpg" },
        { offerName: 'Oferta E', companyName: 'Firma E', companyType: 'Usługi', issueDate: '2024-04-17', expiryDate: '2024-05-17', image: "../src/views/offers/flamingo.jpg" },
        { offerName: 'Oferta F', companyName: 'Firma F', companyType: 'Wynajem', issueDate: '2024-04-16', expiryDate: '2024-05-16', image: "../src/views/offers/flamingo.jpg" }
      ]
    };
  },
  computed: {
    paginatedOffers() {
      const startIndex = (this.currentPage - 1) * this.pageSize;
      const endIndex = startIndex + this.pageSize;
      return this.filteredOffers.slice(startIndex, endIndex);
    },
    filteredOffers() {
      let filtered = this.offers.filter(offer => {
        // Filtr: Wyszukiwanie
        const searchMatch = 
          offer.offerName.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
          offer.companyName.toLowerCase().includes(this.searchQuery.toLowerCase());
        
        // Filtr: Zakres dat
        let dateMatch = true;
        if (this.startDate && this.endDate) {
          dateMatch = offer.issueDate >= this.startDate && offer.expiryDate <= this.endDate;
        }
        
        // Filtr: Ulubione firmy
        let favoriteMatch = true;
        if (this.favoriteCompany) {
          // Załóżmy, że ulubione firmy to te, których nazwa zaczyna się od "Firma"
          favoriteMatch = offer.companyName.startsWith("Firma");
        }
        
        // Filtr: Firma konkretna
        let companyMatch = true;
        if (this.selectedCompany) {
          companyMatch = offer.companyName === this.selectedCompany;
        }
        
        return searchMatch && dateMatch && favoriteMatch && companyMatch;
      });
      
      return filtered;
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
    }
  }
};
</script>

<style scoped>
.offer-container {
  background-color: rgb(40, 167, 69);
  height: 100vh;
  display: flex;
  align-items: center;
  flex-direction: column;
  padding-top: 80px;
}

.search-form {
  margin-top: -20px;
  margin-bottom: 10px;
  width: 30%;
}

.filter-separator {
  height: 0px; /* Przerwa między polem wyszukiwania a filtrami */
}

.date-filter, .favorite-companies, .specific-company {
  margin-bottom: 10px; /* Odstęp między filtrami */
}

.offer-list-container {
  overflow-y: auto;
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
  border: 5px solid #28a745; /* Zielona ramka o szerokości 2px */
}
</style>
