<template>
  <MainLayout>
    <div class="add-sale-view">
      <h2 class="title">🧾 Nueva Venta</h2>

      <div class="form-group">
        <label>Descripción</label>
        <KkInput v-model="description" type="text" placeholder="Ej. Compra en tienda" />
        <p v-if="descriptionError" class="error-message">{{ descriptionError }}</p>
      </div>

      <div class="products-section">
        <h3>Productos</h3>

        <table class="product-table">
          <thead>
            <tr>
              <th>Producto</th>
              <th>Precio</th>
              <th>Cantidad</th>
              <th>Subtotal</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item, index) in selectedItems" :key="item.productId">
              <td>{{ getProductName(item.productId) }}</td>
              <td>${{ getProductPrice(item.productId).toFixed(2) }}</td>
              <td>
                <KkInput type="number" min="1" v-model.number="item.quantity" />
              </td>
              <td>
                ${{ (item.quantity * getProductPrice(item.productId)).toFixed(2) }}
              </td>
              <td>
                <KkButton @click="removeItem(index)" title="eliminar">❌</KkButton>
              </td>
            </tr>
          </tbody>
        </table>
        <p v-if="productsError" class="error-message">{{ productsError }}</p>

        <div class="product-select">
          <select v-model="selectedProductId">
            <option disabled value="">Selecciona un producto</option>
            <option v-for="product in products" :key="product.id" :value="product.id">
              {{ product.name }} — ${{ product.price }}
            </option>
          </select>
          <KkButton @click="addProduct" title="Agregar">➕</KkButton>
        </div>
      </div>

      <div class="total-section">
        <p><strong>Total: </strong> ${{ totalAmount.toFixed(2) }}</p>
      </div>

      <div class="actions">
        <KkButton :disabled="isSubmitting" @click="submitSale" title="Guardar Venta">💾</KkButton>
      </div>
    </div>
  </MainLayout>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';
import MainLayout from '@/components/layout/MainLayout.vue';
import { getProducts } from '@/services/productService';
import { createSale } from '@/services/salesService';
import KkButton from '@/components/atomic/KkButton.vue';
import KkInput from '@/components/atomic/KkInput.vue';
import { useToast } from "vue-toastification";

const isSubmitting = ref(false);
const toast = useToast();

const description = ref('');
const selectedItems = ref([]);
const selectedProductId = ref('');
const products = ref([]);

const descriptionError = ref('');
const productsError = ref('');

const getProductName = id => products.value.find(p => p.id === id)?.name || '';
const getProductPrice = id => products.value.find(p => p.id === id)?.price || 0;

const totalAmount = computed(() =>
  selectedItems.value.reduce((sum, item) => {
    return sum + item.quantity * getProductPrice(item.productId);
  }, 0)
);

const addProduct = () => {
  if (!selectedProductId.value) return;

  const exists = selectedItems.value.some(i => i.productId === selectedProductId.value);
  if (!exists) {
    selectedItems.value.push({ productId: selectedProductId.value, quantity: 1 });
  }
  selectedProductId.value = '';
};

const removeItem = index => {
  selectedItems.value.splice(index, 1);
};

const submitSale = async () => {
  descriptionError.value = '';
  productsError.value = '';

  if (!description.value.trim()) {
    descriptionError.value = 'Debe ingresar una descripción.';
  }

  if (!selectedItems.value.length) {
    productsError.value = 'Debe agregar al menos un producto.';
  }

  if (descriptionError.value || productsError.value) {
    return;
  }

  if (isSubmitting.value) return;

  isSubmitting.value = true;

  const saleData = {
    description: description.value,
    details: selectedItems.value.map(item => ({
      productId: item.productId,
      quantity: item.quantity,
      unitPrice: getProductPrice(item.productId),
    })),
  };

  try {
    const token = localStorage.getItem('token');
    if (!token) {
      toast.error('Debe iniciar sesión para guardar una venta.');
      return;
    }

    await createSale(saleData, token);
    toast.success('Venta guardada correctamente');
    description.value = '';
    selectedItems.value = [];
  } catch (err) {
    toast.error('Error al guardar la venta.');
  } finally {
    isSubmitting.value = false;
  }
};

onMounted(async () => {
  products.value = await getProducts();
});
</script>

<style scoped>
.add-sale-view {
  padding: 2rem;
  max-width: 800px;
  margin: auto;
}

.title {
  margin-bottom: 1rem;
}

.form-group {
  margin-bottom: 1rem;
}

.product-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 1rem;
}

.product-table th,
.product-table td {
  padding: 0.5rem;
  border-bottom: 1px solid #ddd;
}

.product-select {
  margin-top: 1rem;
  display: flex;
  gap: 1rem;
}

.total-section {
  margin-top: 1rem;
  font-size: 1.2rem;
}

.submit-button {
  margin-top: 1.5rem;
  background-color: var(--kk-primary);
  color: var(--kk-white);
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 4px;
  font-weight: bold;
  cursor: pointer;
}

.input-error {
  border: 1.5px solid #e74c3c !important; /* rojo fuerte */
  border-radius: 4px;
}

.error-message {
  color: #e74c3c;
  font-size: 0.9rem;
  margin-top: 0.25rem;
}

.actions {
  display: flex;
  justify-content: flex-end;
  margin-top: 1rem;
}
</style>
