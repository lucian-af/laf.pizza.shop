import { api } from '@lib/axios'

export interface GetDayOrdersAmountResponse {
  amount: number
  diffFromYesterday: number
}

export async function getDayOrdersAmount(): Promise<GetDayOrdersAmountResponse> {
  const response = await api.get<GetDayOrdersAmountResponse>(
    'api/v1/metrics/day-orders-amount',
  )

  return response.data
}
