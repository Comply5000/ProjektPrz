import axios from 'axios';

axios.defaults.baseURL = 'https://api-projekt-prz.comply.ovh/api';
//axios.defaults.baseURL = 'https://localhost:7226/api';
axios.defaults.withCredentials = true; // Ustawienie withCredentials na true

export default axios;