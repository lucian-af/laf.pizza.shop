import { Button } from '@components/ui/button'
import { Input } from '@components/ui/input'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@components/ui/select'
import { Search, X } from 'lucide-react'

export enum StatusOrdersEnum {
  ALL,
  PENDING,
  CANCELED,
  PROCESSING,
  DELIVERING,
  DELIVERED,
}

class StatusOrdersUtil {
  public static getDescription(status: StatusOrdersEnum) {
    switch (status) {
      case StatusOrdersEnum.ALL:
        return 'Todos status'
      case StatusOrdersEnum.PENDING:
        return 'Pendente'
      case StatusOrdersEnum.CANCELED:
        return 'Cancelado'
      case StatusOrdersEnum.PROCESSING:
        return 'Em preparo'
      case StatusOrdersEnum.DELIVERING:
        return 'Em entrega'
      case StatusOrdersEnum.DELIVERED:
        return 'Entregue'
    }
  }
}

export function OrderTableFilters() {
  return (
    <form className="flex flex-wrap items-center gap-2">
      <span className="text-sm font-semibold">Filtros:</span>
      <Input placeholder="ID do pedido" className="h-8 w-auto" />
      <Input placeholder="Nome do cliente" className="h-8 w-[320px] flex-1" />
      <Select
        defaultValue={StatusOrdersUtil.getDescription(StatusOrdersEnum.ALL)}
      >
        <SelectTrigger className="h-8 w-[180px]">
          <SelectValue />
        </SelectTrigger>
        <SelectContent>
          {Array.from({ length: 6 }).map((_, i) => (
            <SelectItem
              key={i}
              value={StatusOrdersUtil.getDescription(i as StatusOrdersEnum)}
            >
              {StatusOrdersUtil.getDescription(i as StatusOrdersEnum)}
            </SelectItem>
          ))}
        </SelectContent>
      </Select>

      <Button type="submit" variant="secondary" size="xs">
        <Search className="mr-2 h-4 w-4" />
        Filtrar resultados
      </Button>
      <Button type="button" variant="outline" size="xs">
        <X className="mr-2 h-4 w-4" />
        Remover filtros
      </Button>
    </form>
  )
}
