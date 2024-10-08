import { api } from '@lib/axios'

export async function cancelOrder(orderId: string) {
  await api.patch(`/api/v1/orders/${orderId}/cancel`)
}
