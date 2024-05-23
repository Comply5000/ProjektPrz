import axios from 'axios';

axios.defaults.baseURL = 'https://projekt-prz.comply.ovh/api';
axios.defaults.withCredentials = true; // Ustawienie withCredentials na true

export default axios;