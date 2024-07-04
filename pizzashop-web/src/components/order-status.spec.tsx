import { render } from '@testing-library/react'

import { OrderStatus } from './order-status'

describe('Order Status', () => {
  it('Deve mostrar o texto correto quando o status do pedido é "Pendente"', () => {
    const wrapper = render(<OrderStatus status="Pending" />)

    const statusText = wrapper.getByText('Pendente')
    const badgeElement = wrapper.getByTestId('badge')

    expect(statusText).toBeVisible()
    expect(badgeElement).toHaveClass('bg-slate-400')
  })

  it('Deve mostrar o texto correto quando o status do pedido é "Canceled"', () => {
    const wrapper = render(<OrderStatus status="Canceled" />)

    const statusText = wrapper.getByText('Cancelado')
    const badgeElement = wrapper.getByTestId('badge')

    expect(statusText).toBeVisible()
    expect(badgeElement).toHaveClass('bg-rose-500')
  })

  it('Deve mostrar o texto correto quando o status do pedido é "Delivering"', () => {
    const wrapper = render(<OrderStatus status="Delivering" />)

    const statusText = wrapper.getByText('Em entrega')
    const badgeElement = wrapper.getByTestId('badge')

    expect(statusText).toBeVisible()
    expect(badgeElement).toHaveClass('bg-amber-500')
  })

  it('Deve mostrar o texto correto quando o status do pedido é "Delivered"', () => {
    const wrapper = render(<OrderStatus status="Delivered" />)

    const statusText = wrapper.getByText('Entregue')
    const badgeElement = wrapper.getByTestId('badge')

    expect(statusText).toBeVisible()
    expect(badgeElement).toHaveClass('bg-emerald-500')
  })
})
