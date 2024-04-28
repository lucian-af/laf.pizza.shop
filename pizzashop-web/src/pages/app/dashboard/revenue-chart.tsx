import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from '@components/ui/card'
import {
  CartesianGrid,
  Line,
  LineChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from 'recharts'
import colors from 'tailwindcss/colors'

const data = [
  {
    date: '19/04',
    revenue: 1910,
  },
  {
    date: '20/04',
    revenue: 913,
  },
  {
    date: '21/04',
    revenue: 865,
  },
  {
    date: '22/04',
    revenue: 4110,
  },
  {
    date: '23/04',
    revenue: 965,
  },
  {
    date: '24/04',
    revenue: 732,
  },
  {
    date: '25/04',
    revenue: 540,
  },
]

export function RevenueChart() {
  return (
    <Card className="col-span-9 lg:col-span-6">
      <CardHeader className="flex-row items-center justify-between pb-8">
        <div className="space-y-1">
          <CardTitle className="text-base font-medium">
            Receita no período
          </CardTitle>
          <CardDescription>Receita diária no período</CardDescription>
        </div>
      </CardHeader>

      <CardContent>
        <ResponsiveContainer width="100%" height={240}>
          <LineChart data={data} style={{ fontSize: 12 }}>
            <XAxis
              dataKey="date"
              dy={16}
              stroke="#888"
              axisLine={false}
              tickLine={false}
            />

            <YAxis
              stroke="#888"
              width={67}
              axisLine={false}
              tickLine={false}
              tickFormatter={(value: number) =>
                value.toLocaleString('pt-BR', {
                  style: 'currency',
                  currency: 'BRL',
                })
              }
            />
            <CartesianGrid vertical={false} className="stroke-muted" />

            <Line
              type="linear"
              strokeWidth={2}
              dataKey="revenue"
              stroke={colors.amber[500]}
            />

            <Tooltip
              formatter={(value: number) =>
                value.toLocaleString('pt-BR', {
                  style: 'currency',
                  currency: 'BRL',
                })
              }
            />
          </LineChart>
        </ResponsiveContainer>
      </CardContent>
    </Card>
  )
}
