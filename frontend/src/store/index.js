import { createStore } from "vuex";

export default createStore({
  state: {
    showBox: false,
    message: "",
    messageDuration: 1500,
  },
  mutations: {
    setShowBox(state, value) {
      state.showBox = value;
    },
    setMessage(state, message) {
      state.message = message;
    },
    setMessageDuration(state, duration) {
      state.messageDuration = duration;
    },
  },
  actions: {
    showNotification({ commit }, { message, duration = 1500 }) {
      commit("setMessage", message);
      commit("setShowBox", true);
      commit("setMessageDuration", duration);

      setTimeout(() => {
        commit("setShowBox", false);
        commit("setMessage", ""); // Wyczyść komunikat po ukryciu
        commit("setMessageDuration", 1500); // Powrót do domyślnego czasu trwania
      }, duration);
    },
  },
});
