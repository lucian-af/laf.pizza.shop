import { api } from '@lib/axios'

export interface RegisterRestaurantBody {
  restaurantName: string
  managerName: string
  email?: string
  phone: string
}

export async function registerRestaurant({
  email,
  managerName,
  phone,
  restaurantName,
}: RegisterRestaurantBody) {
  await api.post('/api/v1/restaurant', {
    email,
    managerName,
    restaurantName,
    phone,
  })
}
