<template>
  <header class="app-header">
    <img src="@/assets/logo.png" alt="Krispy Kreme Logo" class="logo" />
    <nav class="nav">
      <router-link to="/sales" class="nav-link">🛒 Ventas</router-link>
      <button @click="logout" class="nav-link logout-btn">❌ Cerrar sesión</button>
    </nav>
  </header>
</template>

<script setup>
import { useRouter } from 'vue-router';

const router = useRouter();
const logout = () => {
  localStorage.removeItem('token');
  router.push('/login');
};
</script>

<style scoped>
.app-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: var(--kk-primary);
  color: white;
  padding: 1rem 2rem;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
}

.logo {
  height: 50px;
  max-width: 160px;
  object-fit: contain;
}

.nav {
  display: flex;
  gap: 1rem;
}

.nav-link {
  color: white !important;
  text-decoration: none;
  font-weight: bold;
  background: transparent;
  border: none;
  cursor: pointer;
  transition: color 0.2s ease;
}

.logout-btn:hover,
.nav-link:hover {
  text-decoration: none; /* quitar línea */
  color: --kk-primary !important; /* o var(--kk-accent) si tienes */
}
</style>
