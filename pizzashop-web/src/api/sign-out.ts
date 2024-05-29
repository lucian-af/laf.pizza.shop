import { api } from '@lib/axios'

export async function signOut() {
  await api.post('/api/v1/authenticate/sign-out')
}
