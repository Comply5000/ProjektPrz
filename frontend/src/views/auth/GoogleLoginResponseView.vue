<!-- GoogleCallback.vue -->
<template>
    <div>
      <p>Logging in...</p>
    </div>
  </template>
  
  <script>
  import axios from '../../../config.js';
  import {SaveUserRoles} from '../../services/UserService.js';
  
  export default {
    mounted() {
      this.handleGoogleResponse();
    },
    methods: {
      handleGoogleResponse() {
        const sessionId = this.$route.query.sessionId;
        
        axios.get(`/user-identity/get-session-data/${sessionId}`)
            .then(response => {
                localStorage.setItem('jwt', response.data.accessToken);
                localStorage.setItem('email', response.data.email);
                localStorage.setItem('isExternal', response.data.isExternal);
                localStorage.setItem('userId', response.data.userId);
                SaveUserRoles(response.data.roles);
                this.$router.push('/');
            });
        
      }
    }
  }
  </script>
  