// src/services/productService.js
import api from './api';

export const getProducts = async () => {
  const response = await api.get('/catalog/products');
  return response.data.payload;
};
