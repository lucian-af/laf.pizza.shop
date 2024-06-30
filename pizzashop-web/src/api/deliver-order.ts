import { api } from '@lib/axios'

export async function deliverOrder(orderId: string) {
  await api.patch(`/api/v1/orders/${orderId}/deliver`)
}
