import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-vue';

// https://vite.dev/config/
export default defineConfig({
    plugins: [plugin()],
    server: {
        port: 55508,
    }
})
