<template>
    <NavBar />
    <div class="company-container">
      <div class="company-main">
        <div class="company-details">
          <div class="company-left">
            <h3 class="company-name">{{ company.companyName }}</h3>
            <p>Typ: <span class="company-type">{{ company.companyType }}</span></p>
            <p>Data założenia: {{ company.foundingDate }}</p>
            <p>Lokalizacja: {{ company.location }}</p>
            <p>Ocena: 4/5</p>
          </div>
          <div class="company-image">
            <img :src="company.image" :alt="'Company Image ' + (index + 1)">
          </div>
        </div>
        <div class="company-description">
          {{ company.description }}
        </div>
        <div class="comments-section">
          <h4>Komentarze</h4>
          <ul>
            <li v-for="comment in comments" :key="comment.id">
              <strong>{{ comment.user }}:</strong> {{ comment.text }}
            </li>
          </ul>
          <input v-model="newComment" placeholder="Dodaj komentarz" />
          <button @click="addComment">Dodaj</button>
        </div>
        <div class="reviews-section">
          <h4>Opinie</h4>
          <ul>
            <li v-for="review in reviews" :key="review.id">
              <strong>{{ review.user }}:</strong> {{ review.text }} <span class="stars">{{ '★'.repeat(review.rating) }}</span>
            </li>
          </ul>
          <input v-model="newReview" placeholder="Dodaj opinię" />
          <div>
            <label for="rating">Ocena: </label>
            <select v-model="newRating" id="rating">
              <option v-for="n in 5" :key="n" :value="n">{{ n }}</option>
            </select>
          </div>
          <button @click="addReview">Dodaj</button>
        </div>
      </div>
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
      company: {
        companyName: 'Flaming sp zoo',
        companyType: 'zoologiczny',
        foundingDate: '1995-08-15',
        location: 'Rzeszów',
        image: "../src/views/companies/flamingo.jpg",
        description: `Flaming sp zoo to firma specjalizująca się w sprzedaży egzotycznych ptaków i akcesoriów zoologicznych. 
                      Nasze produkty cechuje wysoka jakość i profesjonalne doradztwo.`
      },
      comments: [
        { id: 1, user: 'marianczello', text: 'Boze kocham flamingi!' }
      ],
      newComment: '',
      reviews: [
        { id: 1, user: 'krzysztof', text: 'Super sa i w ogole.', rating: 5 }
      ],
      newReview: '',
      newRating: 1,
      currentUser: 'ZalogowanyUżytkownik'  // Przykładowy zalogowany użytkownik
    };
  },
  methods: {
    addComment() {
      if (this.newComment.trim()) {
        this.comments.push({ id: this.comments.length + 1, user: this.currentUser, text: this.newComment });
        this.newComment = '';
      }
    },
    addReview() {
      if (this.newReview.trim()) {
        this.reviews.push({ id: this.reviews.length + 1, user: this.currentUser, text: this.newReview, rating: this.newRating });
        this.newReview = '';
        this.newRating = 1;
      }
    }
  }
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
  margin-top: 0;
}

.company-main {
  background-color: #ddd;
  color: white;
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 80%;
  padding: 25px;
  padding-top: 55px;
  text-align: center;
  box-sizing: border-box;
}

.company-details {
  width: 100%;
  display: flex;
  justify-content: space-between;
  background-color: white;
  font-weight: bold;
  font-size: 20px;
  padding: 10px;
  box-sizing: border-box;
  margin-bottom: 20px;
}

.company-left {
  width: 70%;
  background-color: white;
  color: black;
  text-align: left;
}

.company-image {
  width: 30%;
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
  padding: 20px;
  box-sizing: border-box;
  margin-bottom: 20px;
}

.comments-section, .reviews-section {
  background-color: white;
  color: black;
  width: 100%;
  padding: 20px;
  box-sizing: border-box;
  margin-bottom: 20px;
}

.comments-section h4, .reviews-section h4 {
  margin-bottom: 10px;
}

.comments-section ul, .reviews-section ul {
  list-style-type: none;
  padding: 0;
}

.comments-section li, .reviews-section li {
  margin-bottom: 10px;
}

.comments-section input, .reviews-section input {
  width: calc(100% - 22px);
  padding: 10px;
  margin-bottom: 10px;
  box-sizing: border-box;
}

.comments-section button, .reviews-section button {
  padding: 10px 15px;
  background-color: #28a745;
  color: white;
  border: none;
  cursor: pointer;
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
}

.stars {
  color: gold;
}
</style>
  