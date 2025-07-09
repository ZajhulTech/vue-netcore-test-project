<template>
  <div class="kk-input">
    <label>{{ label }}</label>
    <input
      :type="type"
      :value="modelValue"
      @input="$emit('update:modelValue', $event.target.value)"
    />
  </div>
</template>

<script setup>
defineProps({
  modelValue: String,
  label: String,
  type: {
    type: String,
    default: 'text'
  }
});

defineEmits(['update:modelValue']);
</script>

<style scoped>
.kk-input {
  margin-bottom: 1rem;
  text-align: left;
}
.kk-input label {
  display: block;
  margin-bottom: 0.25rem;
  color: var(--kk-primary);
  font-weight: bold;
}
.kk-input input {
  width: 100%;
  padding: 0.75rem;
  border: 1px solid #ccc;
  border-radius: 4px;
}
</style>
