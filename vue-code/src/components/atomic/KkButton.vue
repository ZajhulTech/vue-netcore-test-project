<!-- src/components/ui/KkButton.vue -->
<template>
  <button
    class="kk-btn"
    :class="color"
    :title="tooltip"
    @click="$emit('click')"
  >
    <slot />
  </button>
</template>

<script setup>
defineProps({
  color: { type: String, default: 'primary' },
  tooltip: { type: String, default: '' }
});
</script>

<style scoped>
.kk-btn {
  padding: 0.5rem;
  border: none;
  border-radius: 0.5rem;
  cursor: pointer;
  transition: background-color 0.3s ease;
  display: inline-flex;
  align-items: center;
  justify-content: center;
}

/* Fondo verde suave */
.primary {
  background-color: #3bb273; /* verde suave */
  color: white;
}
.primary:hover {
  background-color: #2f8c5a;
}

/* Opcional: iconos más visibles */
.kk-btn svg,
.kk-btn i {
  font-size: 1.2rem;
}
</style>
