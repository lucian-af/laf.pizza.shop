import { Button } from '@components/ui/button'
import { Input } from '@components/ui/input'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@components/ui/table'
import { ArrowRight, Search, X } from 'lucide-react'
import { Helmet } from 'react-helmet-async'

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
      </div>
      <div className="space-y-2.5">
        <form className="flex items-center gap-2">
          <span className="text-sm font-semibold">Filtros:</span>
          <Input placeholder="Nome do cliente" className="h-8 w-[320px]" />
        </form>

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
              {Array.from({ length: 15 }).map((_, i) => {
                return (
                  <TableRow key={i}>
                    <TableCell>
                      <Button variant="outline" size="xs">
                        <Search className="h-3 w-3" />
                        <span className="sr-only">Detalhes do pedido</span>
                      </Button>
                    </TableCell>
                    <TableCell className="font-mono text-xs font-medium">
                      765dfabe-1797-4548-9e07-b4cb5d5f5a8b
                    </TableCell>
                    <TableCell className="text-muted-foreground">
                      há 15 minutos
                    </TableCell>
                    <TableCell>
                      <div className="flex items-center gap-2">
                        <span className="h-2 w-2 rounded-full bg-slate-400" />
                        <span className="font-medium text-muted-foreground">
                          Pendente
                        </span>
                      </div>
                    </TableCell>
                    <TableCell className="font-medium">
                      Lucian Alves Ferreira
                    </TableCell>
                    <TableCell className="font-medium">R$ 47.690,00</TableCell>
                    <TableCell>
                      <Button variant="outline" size="xs">
                        <ArrowRight className="mr-2 h-3 w-3" />
                        Aprovar
                      </Button>
                    </TableCell>
                    <TableCell>
                      <Button variant="ghost" size="xs">
                        <X className="mr-2 h-3 w-3" />
                        Cancelar
                      </Button>
                    </TableCell>
                  </TableRow>
                )
              })}
            </TableBody>
          </Table>
        </div>
      </div>
    </>
  )
}
