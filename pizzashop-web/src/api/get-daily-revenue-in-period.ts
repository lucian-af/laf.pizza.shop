import { api } from '@lib/axios'

export type GetDailyRevenueInPeriodResponse = {
  dayWithMonth: string
  revenue: number
}[]

export async function getDailyRevenueInPeriod(): Promise<GetDailyRevenueInPeriodResponse> {
  const response = await api.get<GetDailyRevenueInPeriodResponse>(
    'api/v1/metrics/daily-revenue-in-period',
  )

  return response.data
}
