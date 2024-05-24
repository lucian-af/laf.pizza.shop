import { api } from '@lib/axios'

export interface GetProfileResponse {
  id: string
  name: string
  email: string
  phone: string | null
  role: 'manager' | 'customer'
}

export async function getProfile() {
  const { data } = await api.get<GetProfileResponse>('/api/v1/user/me')
  return data
}
