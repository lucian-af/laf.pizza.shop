import { approveOrder } from '@api/approve-order'
import { cancelOrder } from '@api/cancel-order'
import { deliverOrder } from '@api/deliver-order'
import { dispatchOrder } from '@api/dispatch-order'
import { GetOrdersResponse } from '@api/get-orders'
import { OrderStatus } from '@components/order-status'
import { Button } from '@components/ui/button'
import { TableCell, TableRow } from '@components/ui/table'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { formatDistanceToNow } from 'date-fns'
import { ptBR } from 'date-fns/locale'
import { ArrowRight, Search, X } from 'lucide-react'
import { useState } from 'react'

import { OrderDetails } from './order-details'

export interface OrderTableRowProps {
  order: {
    orderId: string
    createdAt: string
    status: OrderStatus
    customerName: string
    total: number
  }
}

export function OrderTableRow({ order }: OrderTableRowProps) {
  const [isDetailsOpen, setIsDetailsOpen] = useState(false)
  const queryClient = useQueryClient()

  function updateStatusOrder(orderId: string, status: OrderStatus) {
    const orderListCache = queryClient.getQueriesData<GetOrdersResponse>({
      queryKey: ['orders'],
    })

    orderListCache.forEach(([cacheKey, cacheData]) => {
      if (!cacheData) return

      queryClient.setQueryData<GetOrdersResponse>(cacheKey, {
        ...cacheData,
        orders: cacheData.orders.map((order) => {
          if (order.orderId === orderId) {
            return { ...order, status }
          }

          return order
        }),
      })
    })
  }

  const { mutateAsync: cancelOrderFn, isPending: isCancelingOrder } =
    useMutation({
      mutationFn: cancelOrder,
      onSuccess(_, orderId) {
        updateStatusOrder(orderId, 'Canceled')
      },
    })

  const { mutateAsync: aproveOrderFn, isPending: isApprovingOrder } =
    useMutation({
      mutationFn: approveOrder,
      onSuccess(_, orderId) {
        updateStatusOrder(orderId, 'Processing')
      },
    })

  const { mutateAsync: deliverOrderFn, isPending: isDeliveringOrder } =
    useMutation({
      mutationFn: deliverOrder,
      onSuccess(_, orderId) {
        updateStatusOrder(orderId, 'Delivered')
      },
    })

  const { mutateAsync: dispatchOrderFn, isPending: isDispatchingOrder } =
    useMutation({
      mutationFn: dispatchOrder,
      onSuccess(_, orderId) {
        updateStatusOrder(orderId, 'Delivering')
      },
    })

  return (
    <TableRow>
      <TableCell>
        <OrderDetails
          orderId={order.orderId}
          open={isDetailsOpen}
          onOpenChange={setIsDetailsOpen}
        >
          <Button variant="outline" size="xs">
            <Search className="h-3 w-3" />
            <span className="sr-only">Detalhes do pedido</span>
          </Button>
        </OrderDetails>
      </TableCell>

      <TableCell className="font-mono text-xs font-medium">
        {order.orderId}
      </TableCell>

      <TableCell className="text-muted-foreground">
        {formatDistanceToNow(order.createdAt, {
          locale: ptBR,
          addSuffix: true,
        })}
      </TableCell>

      <TableCell>
        <OrderStatus status={order.status} />
      </TableCell>

      <TableCell className="font-medium">{order.customerName}</TableCell>

      <TableCell className="font-medium">
        {order.total.toLocaleString('pt-BR', {
          style: 'currency',
          currency: 'BRL',
        })}
      </TableCell>

      <TableCell>
        {order.status === 'Pending' && (
          <Button
            variant="outline"
            size="xs"
            onClick={() => aproveOrderFn(order.orderId)}
            disabled={isApprovingOrder}
          >
            <ArrowRight className="mr-2 h-3 w-3" />
            Aprovar
          </Button>
        )}

        {order.status === 'Processing' && (
          <Button
            variant="outline"
            size="xs"
            onClick={() => dispatchOrderFn(order.orderId)}
            disabled={isDispatchingOrder}
          >
            <ArrowRight className="mr-2 h-3 w-3" />
            Em entrega
          </Button>
        )}

        {order.status === 'Delivering' && (
          <Button
            variant="outline"
            size="xs"
            onClick={() => deliverOrderFn(order.orderId)}
            disabled={isDeliveringOrder}
          >
            <ArrowRight className="mr-2 h-3 w-3" />
            Entregue
          </Button>
        )}
      </TableCell>

      <TableCell>
        <Button
          disabled={
            !['Pending', 'Processing'].includes(order.status) ||
            isCancelingOrder
          }
          variant="ghost"
          size="xs"
          onClick={() => cancelOrderFn(order.orderId)}
        >
          <X className="mr-2 h-3 w-3" />
          Cancelar
        </Button>
      </TableCell>
    </TableRow>
  )
}
