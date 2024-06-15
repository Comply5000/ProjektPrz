<template>
  <div>
    <div class="floating-box" v-if="showBox">
      <p>{{ message }}</p>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  computed: {
    ...mapState(["showBox", "message", "messageDuration"]),
  },

  watch: {
    showBox(newValue) {
      if (newValue) {
        setTimeout(() => {
          this.$store.commit("setShowBox", false);
          this.$store.commit("setMessage", ""); // Wyczyść komunikat po ukryciu
        }, this.messageDuration);
      }
    },
  },
};
</script>

<style scope>
.floating-box {
  color: green;
  position: fixed;
  font-size: 18px;
  bottom: 20px;
  right: 20px;
  background-color: white;
  border: 1px solid gray;
  padding: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
  padding-bottom: 0px;
}
</style>
