<template>
  <NavBar />
  <div class="company-container">
    <!-- Formularz wyszukiwania -->
    <div class="search-form">
      <input v-model="searchQuery" class="form-control" type="search" placeholder="Search" aria-label="Search">
    </div>
    <!-- Lista firm -->
    <div class="company-list-container">
      <div class="company-list">
        <ul>
          <li v-for="(company, index) in companies" :key="company.id" class="company-item">
            <div class="company-details">
              <div class="company-content">
                <div class="company-text">
                  <div class="company-header">
                    <span v-if="company.favourite" class="favorite-star">★</span>
                    <h3 class="company-name" @click="goToCompany(company.id)">{{ company.name }}</h3>
                  </div>
                  <p class="company-location">{{ company.localization }}</p>
                  <p class="company-description">{{ company.description }}</p>
                </div>
              </div>
              <div class="company-image">
                <img :src="company.imageUrl" :alt="'company.image' + (index + 1)">
              </div>
            </div>
          </li>
        </ul>
      </div>
    </div>
    <!-- Wybór liczby firm na stronie -->
    <div class="company-per-page">
      <label for="companyPerPage">Liczba firm na jednej stronie:</label>
      <select v-model="pageSize" id="companyPerPage">
        <option v-for="option in pageSizeOptions" :value="option">{{ option }}</option>
      </select>
    </div>
    <!-- Paginacja -->
    <nav aria-label="Page navigation example">
      <ul class="pagination justify-content-center">
       <li class="page-item" :class="{ disabled: currentPage === 1 }">
          <button class="page-link" @click="changePage(currentPage - 1)" :disabled="currentPage === 1">Previous</button>
       </li>
       <li class="page-item" v-for="page in totalPages" :key="page" :class="{ active: currentPage === page }">
         <a class="page-link" href="#" @click="changePage(page)">{{ page }}</a>
       </li>
       <li class="page-item" :class="{ disabled: currentPage === totalPages }">
        <button class="page-link" @click="changePage(currentPage + 1)" :disabled="currentPage === totalPages">Next</button>
       </li>
      </ul>
    </nav>
  </div>
</template>

<script>
import axios from '../../../config.js';
import NavBar from '@/components/NavBar.vue';

export default {
  components: {
    NavBar
  },
  data() {
    return {
      currentPage: 1,
      totalPages: 1,
      pageSize: 5,
      pageSizeOptions: [5, 10, 15],
      searchQuery: '',
      companies: [],
    };
  },
  methods: {
  changePage(page) {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.fetchCompanies();
      console.log('Idź do strony:', page);
    }
  },
  goToCompany(companyid)
  {
    this.$router.push({ path: `/company-view:${companyid}` });
  },
  fetchCompanies() {
    const token = localStorage.getItem('jwt');
    axios.get('/companies',{
      params: {
          pageNumber: this.currentPage,
          pageSize: this.pageSize,
          search: (this.searchQuery || '').trim(),
        },
        headers: {
          'Authorization': `Bearer ${token}`
        }
    })
      .then(response => {
        this.companies = response.data.items;
        this.currentPage = response.data.pageIndex;
        this.totalPages = response.data.totalPages;
      });
    }
},
  watch: {
    currentPage() {
      this.fetchCompanies();
    },
    searchQuery() {
      this.fetchCompanies();
    },
    pageSize() {
      this.fetchCompanies();
    }
  },
  mounted() {
    this.fetchCompanies();
  }
};
</script>

<style scoped>
.company-container {
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
  width: 30%;
}

.company-list-container {
  width: 100%;
}

.company-list {
  background-color: #ddd;
  padding: 25px;
  margin: 0 auto;
  width: 75%;
}

.company-item {
  list-style-type: none;
  margin-bottom: 10px;
  background-color: white;
  padding: 15px;
  border-radius: 5px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.company-details { 
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.company-content {
  flex-grow: 1;
}

.company-text {
  text-align: center;
}

.company-header {
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
}

.favorite-star {
  color: white;
  position: absolute;
  left: 10px;
}

.company-text h3, .company-text p {
  margin: 2px;
}

.company-rating {
  padding: 0px 10px;
  border-radius: 5px;
  text-align: center;
}

.company-name {
  background-color: #28a745;
  color: white;
  padding: 5px 10px;
  border-radius: 5px;
  text-align: center;
  flex: 1;
  margin-left: 30px; /* Dodatkowy odstęp na gwiazdkę */
}

.company-image img {
  width: 110px;
  height: 110px;
  border-radius: 10px;
  border: 5px solid #28a745; /* Zielona ramka o szerokości 5px */
}

.company-per-page {
  margin: 5px 0 5px;
}


.pagination .page-item.disabled .page-link {
  background-color: transparent;
  color: #ccc;
  border: none;
}

.pagination .page-item.active .page-link {
  background-color: rgba(255, 255, 255, 0.15);
  color: black;
  border:none;
}
</style>
