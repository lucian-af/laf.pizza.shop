import { Pagination } from '@components/pagination'
import {
  Table,
  TableBody,
  TableHead,
  TableHeader,
  TableRow,
} from '@components/ui/table'
import { Helmet } from 'react-helmet-async'

import { OrderTableFilters } from './order-table-filters'
import { OrderTableRow } from './order-table-row'

type TableHeaderConfig = {
  headers: Array<{
    description: string | null
    width: string | null
  }>
}

export function Orders() {
  const tableHeaderConfig: TableHeaderConfig = {
    headers: [
      { description: null, width: '64' },
      { description: 'Identificador', width: '140' },
      { description: 'Realizado há', width: '180' },
      { description: 'Status', width: '140' },
      { description: 'Cliente', width: null },
      { description: 'Total do pedido', width: '140' },
      { description: null, width: '164' },
      { description: null, width: '132' },
    ],
  }

  return (
    <>
      <Helmet title="Pedidos" />
      <div className="flex flex-col gap-4">
        <h1 className="text-3xl font-bold tracking-tight">Pedidos</h1>
        <div className="space-y-2.5">
          <OrderTableFilters />

          <div className="rounded-md border">
            <Table>
              <TableHeader>
                <TableRow>
                  {tableHeaderConfig.headers.map((header) => {
                    return (
                      <TableHead
                        key={header.description}
                        className={header.width ? `w-[${header.width}px]` : ''}
                      >
                        {header.description}
                      </TableHead>
                    )
                  })}
                </TableRow>
              </TableHeader>
              <TableBody>
                {Array.from({ length: 15 }).map((_, i) => (
                  <OrderTableRow key={i} />
                ))}
              </TableBody>
            </Table>
          </div>

          <Pagination totalCount={105} pageIndex={0} perPage={10} />
        </div>
      </div>
    </>
  )
}
