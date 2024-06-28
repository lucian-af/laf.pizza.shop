import { api } from '@lib/axios'

export async function cancelOrder(orderId: string) {
  try {
    await api.patch(`/api/v1/orders/${orderId}/cancel`)
  } catch (error) {
    console.log(error)
  }
}
