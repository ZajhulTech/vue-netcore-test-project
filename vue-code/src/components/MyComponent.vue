
<template>
  <div>
    <h1>{{ t('hello_world.title') }}</h1>
    <button @click="increment">{{ t('hello_world.increment') }}: {{ count }}</button>
    <p>{{ t('hello_world.description') }}</p>
  </div>
</template>

<style>

</style>

<script setup>
  import { ref } from 'vue'
  import { useStore } from 'vuex'
  import { useRoute } from 'vue-router'
  import { useI18n } from 'vue-i18n'
  import { useHead } from '@vueuse/head'
  import { useMeta } from 'vue-meta'
  import { useTitle } from '@vueuse/core'
  import { usePage } from '@inertiajs/vue3'

  const store = useStore()
  const route = useRoute()
  const { t } = useI18n()
  const head = useHead()
  const meta = useMeta()
  const title = useTitle()
  const page = usePage()
  const count = ref(0)
  const increment = () => {
    count.value++
  }
</script>



