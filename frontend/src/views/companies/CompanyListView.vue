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
            <li v-for="(company, index) in paginatedCompanies" :key="index" class="company-item">
              <div class="company-details">
                <div class="company-content">
                  <div class="company-text">
                    <div class="company-header">
                      <span v-if="company.favorite" class="favorite-star">★</span>
                      <h3 class="company-name">{{ company.name }}</h3>
                    </div>
                    <p class="company-location">{{ company.location }}</p>
                    <p class="company-description">{{ company.description }}</p>
                    <p class="company-rating">Ocena: {{ company.rating.toFixed(2) }}</p>
                  </div>
                </div>
                <div class="company-image">
                  <img :src="company.image" :alt="'Company Image ' + (index + 1)">
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
        companies: [
          { name: 'Firma A', location: 'ul. Krucza 50, Warszawa', description: 'Opis firmy A', rating: 5.0, favorite: true, image: "../src/views/companies/companyA.jpg" },
          { name: 'Firma B', location: 'ul. Wawelska 12, Kraków', description: 'Opis firmy B', rating: 4.5, favorite: false, image: "../src/views/companies/companyB.jpg" },
          { name: 'Firma C', location: 'ul. Długa 23, Gdańsk', description: 'Opis firmy C', rating: 3.75, favorite: true, image: "../src/views/companies/companyC.jpg" },
          { name: 'Firma D', location: 'ul. Piłsudskiego 10, Wrocław', description: 'Opis firmy D', rating: 2.5, favorite: false, image: "../src/views/companies/companyD.jpg" },
          { name: 'Firma E', location: 'ul. Ratajczaka 5, Poznań', description: 'Opis firmy E', rating: 1.25, favorite: true, image: "../src/views/companies/companyE.jpg" },
          // Dodatkowe firmy, aby umożliwić testowanie paginacji
          { name: 'Firma F', location: 'ul. Lipowa 14, Łódź', description: 'Opis firmy F', rating: 4.0, favorite: false, image: "../src/views/companies/companyF.jpg" },
          { name: 'Firma G', location: 'ul. Krzywa 6, Katowice', description: 'Opis firmy G', rating: 3.5, favorite: true, image: "../src/views/companies/companyG.jpg" },
          { name: 'Firma H', location: 'ul. Leśna 7, Szczecin', description: 'Opis firmy H', rating: 2.0, favorite: false, image: "../src/views/companies/companyH.jpg" },
        ]
      };
    },
    computed: {
      filteredCompanies() {
        return this.companies.filter(company =>
          company.name.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
          company.location.toLowerCase().includes(this.searchQuery.toLowerCase())
        );
      },
      paginatedCompanies() {
        const startIndex = (this.currentPage - 1) * this.pageSize;
        const endIndex = startIndex + this.pageSize;
        return this.filteredCompanies.slice(startIndex, endIndex);
      },
      pageCount() {
        return Math.ceil(this.filteredCompanies.length / this.pageSize);
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
  .company-container {
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
  
  .pagination {
    margin-bottom: 10px
  }
  </style>
  