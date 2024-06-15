<template>
  <NavBar />
</template>

<script>
import axios from "../../../config.js";
import NavBar from "@/components/NavBar.vue";
import { handleErrors } from "../../../errorHandler.js";

export default {
  name: "forgot-password",
  components: {
    NavBar,
  },
  data() {
    return {
      errors: [],
    };
  },
  mounted() {
    const token = this.$route.query.token;
    const userId = this.$route.query.userId;
    axios
      .post(`/user-identity/confirm-account`, {
        token: token,
        userId: userId,
      })
      .then((response) => {
        this.$store.dispatch("showNotification", {
          message: "Konto aktywowane. Wróć do panelu logowania",
        });
        this.$router.push("/sign-in");
      })
      .catch((error) => {
        this.$store.dispatch("showNotification", {
          message: "Coś poszło nie tak.",
        });
        this.$router.push("/sign-in");
      });
  },
};
</script>
