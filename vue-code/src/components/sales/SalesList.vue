<template>
  <div class="sales-list">
 <div class="sales-header">
  <h2 class="title">🛒 Lista de Ventas</h2>
  <KkButton @click="$router.push('/sales/new')" title="Agregar Venta">➕</KkButton>
</div>

    <div class="table-wrapper">
      <table class="sales-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Descripción</th>
            <th>Monto</th>
            <th>Fecha</th>
            <th>Detalle</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="sale in sales" :key="sale.saleId">
            <td>{{ sale.saleId }}</td>
            <td>{{ sale.description }}</td>
            <td>${{ sale.amount.toFixed(2) }}</td>
            <td>{{ new Date(sale.date).toLocaleString() }}</td>
            <td>
              <div v-if="sale.detail && sale.detail.length">
                <div v-for="detail in sale.detail" :key="detail.productName" class="sale-detail">
                  🧾 {{ detail.productName }} — Cant: {{ detail.quantity }} — Precio: ${{ detail.unitPrice }} — Subtotal: ${{ detail.subtotal }}
                </div>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

   <div class="pagination">
  <KkButton :disabled="pageIndex === 1" @click="prevPage" title="Anterior">⏮️</KkButton>
  <span>Página {{ pageIndex }} de {{ totalPages }}</span>
  <KkButton :disabled="pageIndex >= totalPages" @click="nextPage" title="Siguiente">⏭️</KkButton>
</div>
  </div>
</template>


<script setup>
  import { ref, onMounted } from 'vue';
  import { getSales } from '@/services/salesService';
  import KkButton from '@/components/atomic/KkButton.vue';

  const sales = ref([]);
  const pageIndex = ref(1);
  const totalPages = ref(1);
  const pageSize = 5;
  const hasMore = ref(false);

  const loadSales = async () => {
    const response = await getSales(pageIndex.value, pageSize);
    sales.value = response.payload.raw;
    totalPages.value = response.payload.totalPages;
  };

  const prevPage = () => {
    if (pageIndex.value > 1) {
      pageIndex.value--;
      loadSales();
    }
  };

  const nextPage = () => {
    if (pageIndex.value < totalPages.value) {
      pageIndex.value++;
      loadSales();
    }
  };


  onMounted(() => {
    loadSales();
  });
</script>

<style scoped>
.sales-list {
  padding: 2rem;
}

.title {
  margin-bottom: 1.5rem;
  font-size: 1.5rem;
  color: var(--kk-primary, #333);
}

.sales-table {
  width: 100%;
  border-collapse: collapse;
  font-size: 0.95rem;
}

.sales-table th,
.sales-table td {
  padding: 1rem;
  text-align: left;
}

.sales-table thead th {
  border-bottom: 2px solid #ccc;
  font-weight: 600;
}

.sales-table tbody tr {
  border-bottom: 1px solid #eee;
}

.sales-table tbody tr:last-child {
  border-bottom: none;
}

.pagination {
  margin-top: 2rem;
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 1rem;
}

button {
  background-color: var(--kk-primary, #2a9d8f);
  color: white;
  padding: 0.5rem 1rem;
  border-radius: 6px;
  border: none;
  cursor: pointer;
  font-weight: bold;
  transition: background-color 0.2s ease;
}

button:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

button:hover:enabled {
  background-color: var(--kk-secondary, #21867a);
}

.sale-detail-row {
  background-color: #f9f9f9;
}

.detail-cell {
  padding: 0.75rem 1rem;
  font-size: 0.85rem;
  color: #444;
  border-bottom: 1px solid #eee;
}


.table-wrapper {
  max-height: 500px; /* Altura fija para la tabla */
  overflow-y: auto;  /* Scroll vertical */
  border: 1px solid #ddd;
  border-radius: 4px;
}

/* Encabezado fijo */
.sales-table thead tr {
  position: sticky;
  top: 0;
  background-color: white; /* O el color de fondo que quieras */
  z-index: 1;
  border-bottom: 1px solid #ccc;
}

/* Estilo básico tabla */
.sales-table {
  width: 100%;
  border-collapse: collapse;
}

.sales-table th, .sales-table td {
  padding: 10px;
  border-bottom: 1px solid #ccc;
  text-align: left;
  font-weight: normal;
}

.sales-table th {
  font-weight: bold;
}

.sale-detail {
  font-size: 0.9rem;
  color: #555;
  margin-top: 4px;
}

/* Paginación */
.pagination {
  margin-top: 1rem;
  display: flex;
  justify-content: center;
  gap: 1rem;
}

.pagination button {
  cursor: pointer;
  padding: 0.5rem 1rem;
  border: none;
  background-color: var(--kk-primary);
  color: var(--kk-white);
  border-radius: 4px;
  font-weight: bold;
}

.pagination button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.sales-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}

.title {
  margin: 0;
  font-size: 1.5rem;
  font-weight: bold;
  color: var(--kk-primary); /* o color que uses para títulos */
}

.add-sale-btn {
  background-color: var(--kk-primary);
  color: var(--kk-white);
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-weight: bold;
  transition: background-color 0.3s ease;
}

.add-sale-btn:hover {
  background-color: var(--kk-secondary);
}

</style>
