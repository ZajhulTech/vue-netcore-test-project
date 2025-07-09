<template>
  <div class="login-container">
    <div class="login-card">
      <h2 class="title">Iniciar Sesión</h2>
      <form @submit.prevent="handleLogin" class="login-form">
         <KkInput v-model="username" label="Usuario" />
         <p v-if="usernameError" class="error-message">{{ usernameError }}</p>
         <KkInput v-model="password" type="password" label="Contraseña" />
         <p v-if="passwordError" class="error-message">{{ passwordError }}</p>
         <KkButton :disabled="loading" @click="handleLogin" title="🔑 Ingresar">
          <span v-if="loading">⏳</span>
          <span v-else>🔑 Ingresar</span>
        </KkButton>

        <!-- Mensaje de error -->
        <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, defineEmits } from 'vue';
import { login } from '../../services/authService';
import KkButton from '@/components/atomic/KkButton.vue';
import KkInput from '@/components/atomic/KkInput.vue';
import { useToast } from "vue-toastification";

const emit = defineEmits(['login-success']);
const toast = useToast();

const username = ref('');
const password = ref('');
const errorMessage = ref('');
const usernameError = ref('');
const passwordError = ref('');
const loading = ref(false);

const handleLogin = async () => {
  errorMessage.value = '';
  usernameError.value = '';
  passwordError.value = '';
  loading.value = true;

  if (!username.value.trim()) {
    usernameError.value = 'El usuario es requerido.';
  }
  if (!password.value.trim()) {
    passwordError.value = 'La contraseña es requerida.';
  }

  if (usernameError.value || passwordError.value) {
    loading.value = false;
    return;
  }

  try {
    const res = await login(username.value, password.value);
    localStorage.setItem('token', res.payload.token);
    toast.success("Inicio de sesión exitoso");
    emit('login-success');
  } catch (e) {
    errorMessage.value = 'Login inválido. Por favor, verifica tus credenciales.';
  } finally {
    loading.value = false;
  }
};
</script>


<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background-color: var(--kk-bg);
}

.login-card {
  background-color: var(--kk-white);
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
  width: 90%;
  max-width: 400px;
  text-align: center;
}

.title {
  margin-bottom: 1.5rem;
  color: var(--kk-primary);
}

.login-form input {
  width: 100%;
  padding: 0.75rem;
  margin-bottom: 1rem;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.login-form button {
  width: 100%;
  padding: 0.75rem;
  background-color: var(--kk-primary);
  color: var(--kk-white);
  border: none;
  border-radius: 4px;
  font-weight: bold;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.login-form button:hover {
  background-color: var(--kk-secondary);
}
.error-message {
  color: #e74c3c; /* rojo para error */
  margin-top: 0.5rem;
  font-size: 0.9rem;
  text-align: center;
}
</style>
