import axios from 'axios'
import { env } from 'src/env'

export const api = axios.create({
  baseURL: env.VITE_API_URL,
  withCredentials: true,
})
