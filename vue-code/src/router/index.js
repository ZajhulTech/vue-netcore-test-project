// router/index.js (o similar)
import { createRouter, createWebHistory } from 'vue-router';
import LoginView from '../views/LoginView.vue';
import VentasView from '../views/SalesView.vue';
import AddSaleView from '../views/AddSaleView.vue';

const routes = [
   {
    path: '/',
    name: 'principal',
    component: LoginView,
    meta: { requiresAuth: true },
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginView,
  },
  {
    path: '/sales',
    name: 'Sales',
    component: VentasView,
  },
   {
    path: '/sales/new',
    name: 'AddSales',
    component: AddSaleView,
    meta: { requiresAuth: true },
  },
  // otras rutas...
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token')

  if (to.meta.requiresAuth && !token) {
    // No hay token y la ruta requiere autenticación
    next('/login') // redirige a login
  } else {
    next() // sigue la navegación normalmente
  }
})

export default router;
