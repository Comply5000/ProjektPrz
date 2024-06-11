<template>
  <NavBar />
  <div class="offer-container">
    <div class="offer-main">
      <div class="offer-company">
        <div class="offer-left"> 
          <h3 class="offer-name">{{ offer.name }} 
            <!--TU ISUSER() DO ZMIANY BD-->
           
          
          </h3>
          <p class="company-name">{{ offer.companyName }}</p>
          <p>Typ: <span class="offer-type">{{ getOfferTypeName(offer.type) }}</span></p>
          
          <p>Ocena: {{offer.rating}}</p>
          
        </div>
        <div class="offer-image">
          <img :src="offer.imageUrl" :alt="'Offer Image'">
        </div>
      </div>
      <div class="offeropis">
        {{offer.description}}
      </div>
      <div class="reviews-section">
        <h4>Opinie</h4>
        <ul>
          <li v-for="comment in comments" :key="comment.id">
            <strong>{{ comment.createdBy }}: </strong> {{ comment.message }} <span class="stars">{{ '★'.repeat(comment.rating) }}</span>
          </li>
        </ul>
        <div v-if="!offer.isUserCommented && isUser()" >
            <input v-model="newComment.message" placeholder="Dodaj opinię" />
            <div>
              <label for="rating">Ocena: </label>
              <select v-model="newComment.rating" id="rating">
                <option v-for="n in 5" :key="n" :value="n">{{ n }}</option>
              </select>
            </div>
            <div v-if="errors">
            <ul style="list-style-type: none; margin: 0; padding: 0; margin-top: 10px;">
              <li v-for="errorMsg in errors" :key="errorMsg" class="error-message">
                <div class="error">
                    {{ errorMsg }}
                </div>
              </li>
            </ul>
        </div>
            <button @click="addComment">Dodaj</button>
        </div>
      </div>
      
      <!--
      <div class="comments-section">
        <h4>Pytania</h4>
        <ul>
          <li v-for="comment in comments" :key="comment.id">
            <strong>{{ comment.createdBy }}:</strong> {{ comment.message }}
          </li>
        </ul>
        
        <input v-model="newQuestion" placeholder="Dodaj pytanie" />
        <button @click="addQuestion">Dodaj</button>
      </div>
      -->
    </div>
  </div>
</template>

<script>
import { CheckUserRole } from '../../services/UserService.js';
import axios from '../../../config.js';
import NavBar from '@/components/NavBar.vue';
import Space from '@/components/Space.vue';
import { handleErrors } from '../../../errorHandler.js';
export default {
  components: {
    NavBar
  },
  data() {
    return {
      offer: {
        id: null,
        name: '',
        description: ``,
        imageUrl: "",
        companyId: null,
        companyName: '',
        isUserCommented: false,
        rating: 0,
        isFavorite: false,
        type: ""
      },
      offerTypes: {
        1: 'Produkt',
        2: 'Usługa',
        3: 'Promocja',
        4: 'Program lojalnościowy'
      },
      comments: [],
      errors: [],
      newComment: {
        message: "",
        rating: 0


      }
    };
  },
  methods: {
    isUser() {
      return CheckUserRole('User');
    },
    getOfferTypeName(value) {
      return this.offerTypes[value];
    },
    toggleFavorite() {
      const token = localStorage.getItem('jwt');
      console.log(token);
      axios.put(`/offers/${this.offer.id}/add-to-favourite`,{}, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      })
      .then(response => {
          this.fetch();
      });
      // Tu możesz dodać logikę do zapisu stanu ulubionych, np. zapisu do lokalnej pamięci lub wysłania do serwera
    },
    fetchComments() {
      const offerid = this.$route.params.id;
      const token = localStorage.getItem('jwt');
      axios.get(`/comments/${offerid.slice(1)}`, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      })
      .then(response => {
          
          this.comments = response.data;
         
      });
      },
    fetch() {
      const offerid = this.$route.params.id;
      const token = localStorage.getItem('jwt');
      axios.get(`/offers/${offerid.slice(1)}`, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      })
      .then(response => {
          this.offer.name = response.data.name;
          this.offer.description = response.data.description ?? '';
          this.offer.companyId = response.data.companyId ;
          this.offer.imageUrl =  response.data.imageUrl;
          this.offer.companyName = response.data.companyName ?? '';
          this.offer.id = response.data.id;
          this.offer.isUserCommented = response.data.isUserCommented;
          this.offer.rating = response.data.rating;
          this.offer.isFavorite = response.data.favourite;
          this.offer.type = response.data.type;
      });
      },
    
      
    
    addComment() {
      this.errors = [];
      const offerid = this.$route.params.id;
      const token = localStorage.getItem('jwt');
        axios.post(`/comments/${offerid.slice(1)}`,  this.newComment, {
          headers: {
          'Authorization': `Bearer ${token}`
          }
        }
         )
        .then(response => {
          this.fetch();
          this.fetchComments();
        })
        .catch(error => {
        const errors = [];
        handleErrors(error, errors);
        this.errors = this.errors.concat(errors);
      });
      
    },/*
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
    }*/
  }
    ,mounted()
    {
      this.fetch();
      this.fetchComments();
    },
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
  padding-top: 40px;
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
.favorite-star {
  margin-left: 10px;
  cursor: pointer;
  color: gold;
}
.error {
  color: red;
}
.favorite-star .fa-star {
  font-size: 24px;
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
