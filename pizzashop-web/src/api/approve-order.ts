import { api } from '@lib/axios'

export async function approveOrder(orderId: string) {
  await api.patch(`/api/v1/orders/${orderId}/approve`)
}
