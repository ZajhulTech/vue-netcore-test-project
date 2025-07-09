<template>
  <footer class="app-footer">
    <p>© {{ new Date().getFullYear() }} Krispy Kreme. Todos los derechos reservados.</p>
  </footer>
</template>

<style scoped>
.app-footer {
  background-color: var(--kk-primary);
  color: white;
  text-align: center;
  padding: 1rem;
  margin-top: auto;
}
</style>
