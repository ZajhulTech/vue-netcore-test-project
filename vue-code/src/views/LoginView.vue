<template>

    <LoginForm @login-success="onLoginSuccess" />

</template>

<script setup>
import LoginForm from '../components/login/LoginForm.vue';
import '../styles/login.css';
import { useRouter } from 'vue-router';
import { onMounted } from 'vue';

const router = useRouter();

function onLoginSuccess() {
  router.push('/sales');
}


</script>

