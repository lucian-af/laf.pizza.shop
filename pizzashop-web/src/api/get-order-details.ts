import { api } from '@lib/axios'

export interface GetOrderDetailsResponse {
  orderId: string
  createdAt: string
  status: 'Pending' | 'Canceled' | 'Processing' | 'Delivering' | 'Delivered'
  customerName: string
  customerPhone: string
  customerEmail: string
  total: number
  orderItems: {
    orderItemId: string
    productName: string
    price: number
    quantity: number
  }[]
}

export async function getOrderDetails(orderId: string) {
  const response = await api.get<GetOrderDetailsResponse>(
    `/api/v1/orders/details/${orderId}`,
  )

  try {
    return response.data
  } catch (error) {
    console.log(error)
  }
}
