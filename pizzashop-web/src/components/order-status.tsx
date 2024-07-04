export type OrderStatus =
  | 'Pending'
  | 'Canceled'
  | 'Processing'
  | 'Delivering'
  | 'Delivered'

interface OrderStatusProps {
  status: OrderStatus
}

const orderStatusMap: Record<OrderStatus, string> = {
  Pending: 'Pendente',
  Canceled: 'Cancelado',
  Delivered: 'Entregue',
  Delivering: 'Em entrega',
  Processing: 'Em preparo',
}

export function OrderStatus({ status }: OrderStatusProps) {
  return (
    <div className="flex items-center gap-2">
      {status === 'Pending' && (
        <span
          data-testid="badge"
          className="h-2 w-2 rounded-full bg-slate-400"
        />
      )}

      {status === 'Canceled' && (
        <span
          data-testid="badge"
          className="h-2 w-2 rounded-full bg-rose-500"
        />
      )}

      {status === 'Delivered' && (
        <span
          data-testid="badge"
          className="h-2 w-2 rounded-full bg-emerald-500"
        />
      )}

      {['Processing', 'Delivering'].includes(status) && (
        <span
          data-testid="badge"
          className="rounded-fullld-500 h-2 w-2 bg-amber-500"
        />
      )}
      <span className="font-medium text-muted-foreground">
        {orderStatusMap[status]}
      </span>
    </div>
  )
}
