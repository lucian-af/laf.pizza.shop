import { api } from '@lib/axios'

export async function approveOrder(orderId: string) {
  try {
    await api.patch(`/api/v1/orders/${orderId}/approve`)
  } catch (error) {
    console.log(error)
  }
}
