<!-- src/components/ui/KkButton.vue -->
<template>
  <button :class="['kk-btn', color]" @click="$emit('click')">
    <slot />
  </button>
</template>

<script setup>
defineProps({ color: { type: String, default: 'primary' } });
</script>

<style scoped>
.kk-btn {
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 0.375rem;
  font-weight: bold;
  color: white;
  cursor: pointer;
  transition: background-color 0.3s;
}
.primary {
  background-color: var(--kk-primary);
}
.primary:hover {
  background-color: #bb1c1f;
}
</style>
