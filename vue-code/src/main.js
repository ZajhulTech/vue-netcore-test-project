import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import './styles/variables.css';
import './styles/theme.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';

import Toast from 'vue-toastification'

import 'vue-toastification/dist/index.css'
const app = createApp(App)
app.use(router)
app.use(Toast)
app.mount('#app')

//createApp(App).use(router).mount('#app');
