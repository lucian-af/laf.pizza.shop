import { api } from '@lib/axios'

export interface GetManagedRestaurantResponse {
  id: string
  name: string
  description: string | null
}

export async function getManagedRestaurant() {
  const { data } = await api.get<GetManagedRestaurantResponse>(
    '/api/v1/restaurant/managed-restaurant',
  )
  return data
}
