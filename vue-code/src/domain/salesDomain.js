import { getSales } from '@/services/saleService'

export async function fetchPaginatedSales(pageIndex, pageSize) {
  const response = await getSales({ pageIndex, pageSize })

  // Lógica de negocio: por ejemplo, formatear fechas
  return response?.payload?.raw?.map(sale => ({
    ...sale,
    date: new Date(sale.date).toLocaleDateString(),
    amount: sale.amount.toFixed(2)
  }))
}
