<template>
  <NavBar />
  <div class="offer-container">
    <div class="offer-main">
      <div class="offer-company">
        <div class="offer-left">
          <h3 class="offer-name">{{ offer.offerName }}</h3>
          <p class="company-name">{{ offer.companyName }}</p>
          <p>Typ: <span class="offer-type">{{ offer.companyType }}</span></p>
          <p>Data wystawienia: {{ offer.issueDate }}</p>
          <p>Data ważności: {{ offer.expiryDate }}</p>
          <p>Ocena: 4/5</p>
          <p>Lokalizacja: Rzeszów</p>
        </div>
        <div class="offer-image">
          <img :src="offer.image" :alt="'Offer Image ' + (index + 1)">
        </div>
      </div>
      <div class="offeropis">
        Odkryj świat flamingów w naszym sklepie zoologicznym!
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
import axios from '../../../config.js';
import NavBar from '@/components/NavBar.vue';

export default {
  components: {
    NavBar
  },
  data() {
    return {
      offer: {},
      comments: [],
      reviews: [],
      newComment: '',
      newReview: '',
      newRating: 1,
      currentUser: 'ZalogowanyUżytkownik'  // Przykładowy zalogowany użytkownik
    };
  },
  methods: {
    fetchOffer() {
      axios.get(`/offer/${offerId}`) // Adjust URL according to your API
        .then(response => {
          this.offer = response.data;
          this.comments = response.data.comments;
          this.reviews = response.data.reviews;
        })
        .catch(error => console.error('Error fetching offer data:', error));
    },
    addComment() {
      if (this.newComment.trim()) {
        axios.post('/api/comments', {
          text: this.newComment,
          offerId: this.offer.id,
          userId: this.currentUser
        })
        .then(response => {
          this.comments.push(response.data);
          this.newComment = '';
        })
        .catch(error => console.error('Error adding comment:', error));
      }
    },
    addReview() {
      if (this.newReview.trim()) {
        axios.post('/api/reviews', {
          text: this.newReview,
          rating: this.newRating,
          offerId: this.offer.id,
          userId: this.currentUser
        })
        .then(response => {
          this.reviews.push(response.data);
          this.newReview = '';
          this.newRating = 1;
        })
        .catch(error => console.error('Error adding review:', error));
      }
    }
  },
  created() {
    this.fetchOffer(); // Pobierz dane oferty podczas inicjalizacji
  }
};
</script>

<style scoped>
body {
  margin: 0;
  padding: 0;
}

.offer-container {
  background-color: rgb(40, 167, 69);
  min-height: 100vh;
  display: flex;
  align-items: center;
  flex-direction: column;
  margin-top: 0;
}

.offer-main {
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

.offer-company {
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

.offer-left {
  width: 70%;
  background-color: white;
  color: black;
  text-align: left;
}

.offer-image {
  width: 30%;
}

.offer-image img {
  width: 100%;
  height: auto;
  border-radius: 10px;
}

.offeropis {
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

.stars {
  color: gold;
}
</style>
