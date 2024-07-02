import { api } from '@lib/axios'

export type GetDailyRevenueInPeriodResponse = {
  dayWithMonth: string
  revenue: number
}[]

export interface GetDailyRevenueInPeriodQuery {
  from?: Date
  to?: Date
}

export async function getDailyRevenueInPeriod({
  from,
  to,
}: GetDailyRevenueInPeriodQuery): Promise<GetDailyRevenueInPeriodResponse> {
  const response = await api.get<GetDailyRevenueInPeriodResponse>(
    'api/v1/metrics/daily-revenue-in-period',
    {
      params: {
        from,
        to,
      },
    },
  )

  return response.data
}
