import { api } from '@lib/axios'

export interface GetOrdersFilters {
  pageIndex?: number | null
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

export async function getOrders({ pageIndex }: GetOrdersFilters) {
  const response = await api.post<GetOrdersResponse>('/api/v1/orders', {
    pageIndex,
  })

  try {
    return response.data
  } catch (error) {
    console.log(error)
  }
}
