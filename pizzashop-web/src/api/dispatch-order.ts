import { api } from '@lib/axios'

export async function dispatchOrder(orderId: string) {
  try {
    await api.patch(`/api/v1/orders/${orderId}/dispatch`)
  } catch (error) {
    console.log(error)
  }
}
