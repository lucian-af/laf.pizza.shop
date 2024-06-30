import { api } from '@lib/axios'

export interface GetMonthCanceledOrdersAmountResponse {
  amount: number
  diffFromLastMonth: number
}

export async function getMonthCanceledOrdersAmount(): Promise<GetMonthCanceledOrdersAmountResponse> {
  const response = await api.get<GetMonthCanceledOrdersAmountResponse>(
    'api/v1/metrics/month-canceled-orders-amount',
  )

  return response.data
}
