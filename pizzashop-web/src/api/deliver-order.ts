import { api } from '@lib/axios'

export async function deliverOrder(orderId: string) {
  try {
    await api.patch(`/api/v1/orders/${orderId}/deliver`)
  } catch (error) {
    console.log(error)
  }
}
