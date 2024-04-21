<template>
    <div class="offer-container">
      <!-- Formularz wyszukiwania nad tablicą ofert -->
      <div class="search-form">
        <input v-model="searchQuery" class="form-control" type="search" placeholder="Search" aria-label="Search">
      </div>
      <!-- Białe tło z listą ofert -->
      <div class="offer-list-container">
        <div class="offer-list">
          <ul>
            <li v-for="(offer, index) in paginatedOffers" :key="index" class="offer-item">
              <div class="offer-details">
                <div class="offer-image">
                  <img src="../offers/flamingo.jpg" alt="Offer Image">
                </div>
                <div class="offer-content">
                  <h3>{{ offer.title }}</h3>
                  <p>{{ offer.description }}</p>
                </div>
              </div>
            </li>
          </ul>
        </div>
  
        <!-- Paginacja -->
        <nav aria-label="Page navigation example" class="pagination-container">
          <ul class="pagination justify-content-center">
            <li v-for="(page, index) in pageCount" :key="index" class="page-item" @click="changePage(page)">
              <a class="page-link" href="#">{{ page }}</a>
            </li>
          </ul>
        </nav>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        currentPage: 1,
        pageSize: 5, // liczba ofert na stronie
        searchQuery: '', // zmienna do przechowywania wyszukiwanej frazy
        offers: [ // przykładowe oferty
          { title: 'Oferta 1', description: 'Opis oferty 1', image: 'path/do/zdjecia1.jpg' },
          { title: 'Oferta 2', description: 'Opis oferty 2', image: 'path/do/zdjecia2.jpg' },
          { title: 'Oferta 3', description: 'Opis oferty 3', image: 'path/do/zdjecia3.jpg' },
          { title: 'Oferta 4', description: 'Opis oferty 4', image: 'path/do/zdjecia4.jpg' },
          { title: 'Oferta 5', description: 'Opis oferty 5', image: 'path/do/zdjecia5.jpg' },
          { title: 'Oferta 6', description: 'Opis oferty 6', image: 'path/do/zdjecia6.jpg' },
          // Dodaj więcej ofert, jeśli potrzebujesz
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
        // Filtruj oferty na podstawie wyszukiwanej frazy
        return this.offers.filter(offer =>
          offer.title.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
          offer.description.toLowerCase().includes(this.searchQuery.toLowerCase())
        );
      },
      pageCount() {
        return Math.ceil(this.filteredOffers.length / this.pageSize);
      }
    },
    methods: {
      changePage(page) {
        this.currentPage = page;
      }
    }
  };
  </script>
  
  <style scoped>
  .offer-container {
    background-color: #28a745; /* Kolor tła poza tablicą */
    height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column; /* Ustawienie kolumnowo dla flexbox */
  }
  
  .search-form {
    margin-bottom: 20px; /* Dodatkowy margines na dole pomiędzy formularzem wyszukiwania a listą ofert */
    width: 30%; /* Ustawienie szerokości formularza na 75% */
  }
  
  .offer-list-container {
    width: 75%;
    overflow: auto; /* Dodanie przewijania, jeśli lista ofert przekracza wysokość */
  }
  
  .offer-list {
    background-color: #73c987;
    padding: 20px;
    overflow-y: auto; /* Dodanie przewijania, jeśli lista ofert przekracza wysokość */
  }
  
  .offer-item {
    list-style-type: none;
    margin-bottom: 20px;
    background-color: #28a745;
  }
  
  img{
    width:50px;
    height:50px;
  }

  .offer-details {
    display: flex;
    justify-content: space-between; /* Rozłożenie elementów na całej szerokości */
    align-items: center; /* Wyśrodkowanie w pionie */
  }
  
  .offer-details .offer-content {
    flex-grow: 1; /* Elastyczność dla treści, aby zajęła jak najwięcej miejsca */
    text-align: center; /* Wyśrodkowanie tekstu na środku */
  }
  
  .offer-details .offer-image {
    margin-right: 20px; /* Odstęp od treści */
    border: 4px solid black; /* Czarna ramka */
    padding: 5px; /* Dodatkowy padding dla ramki */
  }
  .offer-details .offer-image img {
    max-height: 100%; /* Maksymalna wysokość obrazka */
    max-width: 100%; /* Maksymalna szerokość obrazka */
  }
  </style>
  