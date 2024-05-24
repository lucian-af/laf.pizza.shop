import { api } from '@lib/axios'

export interface SignInBody {
  email: string
}

export async function signIn({ email }: SignInBody) {
  await api.post('/api/v1/authenticate', { email })
}
