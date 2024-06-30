import { api } from '@lib/axios'

export interface GetMonthRevenueResponse {
  revenue: number
  diffFromLastMonth: number
}

export async function getMonthRevenue(): Promise<GetMonthRevenueResponse> {
  const response = await api.get<GetMonthRevenueResponse>(
    'api/v1/metrics/month-revenue',
  )

  return response.data
}
