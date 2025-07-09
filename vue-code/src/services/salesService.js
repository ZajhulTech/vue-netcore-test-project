// src/services/salesService.js
import api from './api';

export const getSales = async (pageIndex, pageSize) => {
  const response = await api.get('/ventas', {
    params: { pageIndex, pageSize }
  });
  return response.data;
};

export const createSale = async (saleData, token) => {
  if (!token) {
    throw new Error('Token is required for creating a sale');
  }

  if (!saleData || !saleData.description || !saleData.details || saleData.details.length === 0) {
    throw new Error('Sale data is incomplete');
  }

  const response = await api.post('/ventas', saleData, {
    headers: {
      'x-token': token
    }
  });

  return response.data;
};
