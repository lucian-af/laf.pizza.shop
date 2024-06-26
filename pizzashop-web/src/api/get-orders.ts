import { api } from '@lib/axios'

export interface GetOrdersFilters {
  pageIndex?: number | null
  customerName?: string | null
  orderId?: string | null
  status?: string | null
}

export interface GetOrdersResponse {
  orders: {
    orderId: string
    createdAt: string
    status: 'Pending' | 'Canceled' | 'Processing' | 'Delivering' | 'Delivered'
    customerName: string
    total: number
  }[]
  pageIndex: number
  perPage: number
  totalCount: number
}

export async function getOrders({
  pageIndex,
  customerName,
  orderId,
  status,
}: GetOrdersFilters) {
  const response = await api.post<GetOrdersResponse>('/api/v1/orders', {
    pageIndex,
    customerName,
    orderId,
    status,
  })

  try {
    return response.data
  } catch (error) {
    console.log(error)
  }
}
