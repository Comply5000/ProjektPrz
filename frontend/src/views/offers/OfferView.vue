<template>
  <NavBar />
  <div class="offer-container">
    <div class="offer-main">
      <div class="offer-company">
        <div class="offer-left">
          <h3 class="offer-name">
            {{ offer.name }}
            <!--TU ISUSER() DO ZMIANY BD-->
          </h3>
          <p class="company-name">{{ offer.companyName }}</p>
          <p>
            Typ:
            <span class="offer-type">{{ getOfferTypeName(offer.type) }}</span>
          </p>

          <p>Ocena: {{ offer.rating }}</p>
        </div>
        <div class="offer-image">
          <img :src="offer.imageUrl" :alt="'Offer Image'" />
        </div>
      </div>
      <div class="offeropis">
        {{ offer.description }}
      </div>
      <div class="reviews-section">
        <h4>Opinie</h4>
        <ul>
          <li v-for="comment in comments" :key="comment.id">
            <strong>{{ comment.createdBy }}: </strong> {{ comment.message }}
            <span class="stars">{{ "★".repeat(comment.rating) }}</span>
            <button
              id="remove-button"
              v-if="isUserComentOrQuestion(comment.createdById) || isAdmin()"
              @click="deleteComment(comment.id)"
            >
              Usuń
            </button>
          </li>
        </ul>
        <div v-if="!offer.isUserCommented && isUser() && !isCompany()">
          <input v-model="newComment.message" placeholder="Dodaj opinię" />
          <div>
            <label for="rating">Ocena: </label>
            <select v-model="newComment.rating" id="rating">
              <option v-for="n in 5" :key="n" :value="n">{{ n }}</option>
            </select>
          </div>
          <div v-if="errors">
            <ul
              style="
                list-style-type: none;
                margin: 0;
                padding: 0;
                margin-top: 10px;
              "
            >
              <li
                v-for="errorMsg in errors"
                :key="errorMsg"
                class="error-message"
              >
                <div class="error">
                  {{ errorMsg }}
                </div>
              </li>
            </ul>
          </div>
          <button @click="addComment">Dodaj</button>
        </div>
      </div>

      <div class="comments-section">
        <h4>Pytania</h4>
        <ul>
          <li v-for="question in questions" :key="question.id">
            <strong>{{ question.createdBy }}:</strong> {{ question.message }}
            <button
              id="remove-button"
              v-if="isUserComentOrQuestion(question.createdById) || isAdmin()"
              @click="deleteQuestion(question.id)"
            >
              Usuń
            </button>
            <div class="odpowiedz" v-if="question.answer">
              Odpowiedz: {{ question.answer }}
            </div>
            <div class="odpowiedz" v-if="isCompany()">
              <input
                v-model="questionAnswer[question.id]"
                placeholder="Odpowiedz"
              />
              <button @click="sendAnswerQuestion(question.id)">
                Odpowiedz
              </button>
            </div>
            <hr />
          </li>
        </ul>
        <div v-if="isUser() && !isCompany()">
          <input v-model="newQuestion.message" placeholder="Dodaj pytanie" />
          <div v-if="errorsQuestions">
            <ul
              style="
                list-style-type: none;
                margin: 0;
                padding: 0;
                margin-top: 10px;
              "
            >
              <li
                v-for="errorMsg in errorsQuestions"
                :key="errorMsg"
                class="error-message"
              >
                <div class="error">
                  {{ errorMsg }}
                </div>
              </li>
            </ul>
          </div>
          <button @click="addQuestion">Dodaj</button>
        </div>
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
import { handleErrors } from "../../../errorHandler.js";
export default {
  components: {
    NavBar,
  },
  data() {
    return {
      offer: {
        id: null,
        name: "",
        description: ``,
        imageUrl: "",
        companyId: null,
        companyName: "",
        isUserCommented: false,
        rating: 0,
        type: "",
      },
      offerTypes: {
        1: "Produkt",
        2: "Usługa",
        3: "Promocja",
        4: "Program lojalnościowy",
      },
      comments: [],
      questions: [],
      errorsQuestions: [],
      errors: [],
      newComment: {
        message: "",
        rating: 0,
      },
      newQuestion: {
        message: "",
      },
      questionAnswer: [],
    };
  },
  methods: {
    isCompany() {
      return (
        CheckUserRole("CompanyOwner") &&
        localStorage.getItem("companyId") === this.offer.companyId
      );
    },
    isUser() {
      return CheckUserRole("User");
    },
    isUserComentOrQuestion(userId) {
      return CheckUserRole("User") && localStorage.getItem("userId") === userId;
    },
    isAdmin() {
      return CheckUserRole("Admin");
    },
    getOfferTypeName(value) {
      return this.offerTypes[value];
    },
    fetchComments() {
      const offerid = this.$route.params.id;
      const token = localStorage.getItem("jwt");
      axios
        .get(`/comments/${offerid.slice(1)}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          this.comments = response.data;
        });
    },
    fetchQuestions() {
      const offerid = this.$route.params.id;
      const token = localStorage.getItem("jwt");
      axios
        .get(`/questions/${offerid.slice(1)}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          this.questions = response.data;
        });
    },
    fetch() {
      const offerid = this.$route.params.id;
      const token = localStorage.getItem("jwt");
      axios
        .get(`/offers/${offerid.slice(1)}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          this.offer.name = response.data.name;
          this.offer.description = response.data.description ?? "";
          this.offer.companyId = response.data.companyId;
          this.offer.imageUrl = response.data.imageUrl;
          this.offer.companyName = response.data.companyName ?? "";
          this.offer.id = response.data.id;
          this.offer.isUserCommented = response.data.isUserCommented;
          this.offer.rating = response.data.rating;
          this.offer.type = response.data.type;
        });
    },

    addComment() {
      this.errors = [];
      this.errorsQuestions = [];
      const offerid = this.$route.params.id;
      const token = localStorage.getItem("jwt");
      axios
        .post(`/comments/${offerid.slice(1)}`, this.newComment, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          this.$store.dispatch("showNotification", {
            message: "Komentarz dodany pomyślnie!",
          });
          this.fetch();
          this.fetchComments();
          this.fetchQuestions();
        })
        .catch((error) => {
          const errors = [];
          handleErrors(error, errors);
          this.errors = this.errors.concat(errors);
        });
    },
    addQuestion() {
      this.errorsQuestions = [];
      this.errors = [];
      console.log(this.newQuestion);
      const offerid = this.$route.params.id;
      const token = localStorage.getItem("jwt");
      axios
        .post(`/questions/${offerid.slice(1)}`, this.newQuestion, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          this.$store.dispatch("showNotification", {
            message: "Pytanie dodane pomyślnie!",
          });
          this.newQuestion.message = "";
          this.fetch();
          this.fetchQuestions();
          this.fetchComments();
        })
        .catch((error) => {
          const errorsQuestions = [];
          handleErrors(error, errorsQuestions);
          this.errorsQuestions = this.errorsQuestions.concat(errorsQuestions);
        });
    },
    sendAnswerQuestion(questionId) {
      const token = localStorage.getItem("jwt");
      const answerObject = { answer: this.questionAnswer[questionId] };
      axios
        .post(`/questions/${questionId}/answer`, answerObject, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          this.$store.dispatch("showNotification", {
            message: "Odpowiedz dodana pomyślnie!",
          });
          this.questionAnswer = [];
          this.fetch();
          this.fetchQuestions();
          this.fetchComments();
        });
    },
    deleteComment(commentId) {
      let endpointSuffix = "";
      if (this.isAdmin()) endpointSuffix = "/admin";

      const token = localStorage.getItem("jwt");
      axios
        .delete(`/comments/${commentId}${endpointSuffix}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          this.$store.dispatch("showNotification", {
            message: "Komentarz utworzonięty pomyślnie!",
          });
          this.fetch();
          this.fetchComments();
          this.fetchQuestions();
        });
    },
    deleteQuestion(questionId) {
      let endpointSuffix = "";
      if (this.isAdmin()) endpointSuffix = "/admin";

      const token = localStorage.getItem("jwt");
      axios
        .delete(`/questions/${questionId}${endpointSuffix}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          this.$store.dispatch("showNotification", {
            message: "Pytanie usunięte pomyślnie!",
          });
          this.fetch();
          this.fetchComments();
          this.fetchQuestions();
        });
    },
  },
  mounted() {
    this.fetch();
    this.fetchComments();
    this.fetchQuestions();
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
  margin-top: 0px;
  padding-top: 40px;
}

#remove-button {
  background-color: red; /* Czerwone tło */
  color: white; /* Biały tekst */
  border: none; /* Brak obramowania */
  padding: 2px 5px; /* Bardzo małe paddingi */
  font-size: 10px; /* Bardzo mała czcionka */
  border-radius: 8px; /* Zaokrąglone krawędzie */
  margin-left: 5px;
  font-weight: bold;
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
  margin-top: 20px; /* Dodatkowe przesunięcie w dół */
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
  border-radius: 10px;
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
.odpowiedz {
  margin-top: 5px; /* Adjust this value as needed to reduce the space */
  margin-bottom: 5px; /* Add bottom margin if needed */
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

.comments-section,
.reviews-section {
  background-color: white;
  color: black;
  width: 100%;
  padding: 20px;
  box-sizing: border-box;
  margin-bottom: 20px;
}

.comments-section h4,
.reviews-section h4 {
  margin-bottom: 10px;
}

.comments-section ul,
.reviews-section ul {
  list-style-type: none;
  padding: 0;
}

.comments-section li,
.reviews-section li {
  margin-bottom: 10px;
}

.comments-section input,
.reviews-section input {
  width: calc(100% - 22px);
  padding: 10px;
  margin-bottom: 10px;
  box-sizing: border-box;
}

.comments-section button,
.reviews-section button {
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
