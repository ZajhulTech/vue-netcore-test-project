<template>
  <aside class="app-sidebar">
    <ul>
      <li><router-link to="/sales">🛒 Ventas</router-link></li>
      <!-- Agrega más enlaces aquí -->
    </ul>
  </aside>
</template>

<style scoped>
.app-sidebar {
  width: 200px;
  background-color: var(--kk-secondary);
  color: white;
  padding: 1rem;
  height: 100vh;
}

.app-sidebar ul {
  list-style: none;
  padding: 0;
}

.app-sidebar a {
  color: white;
  text-decoration: none;
  display: block;
  margin: 0.5rem 0;
}
</style>
