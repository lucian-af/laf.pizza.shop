import { api } from '@lib/axios'

export async function dispatchOrder(orderId: string) {
  await api.patch(`/api/v1/orders/${orderId}/dispatch`)
}
