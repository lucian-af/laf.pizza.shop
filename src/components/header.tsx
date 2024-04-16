import { Home, Pizza, UtensilsCrossed } from 'lucide-react'

import { NavLink } from './nav-link'
import { Separator } from './ui/separator'

export function Header() {
  return (
    <div className="border-b">
      <div className="flex h-16 items-center gap-6 px-6">
        <Pizza className="h-6 w-6" />

        <Separator orientation="vertical" className="h-6" />

        <nav className="flex items-center space-x-4 lg:space-x-6">
          <NavLink to="/">
            <Home className="h-6 w-6" />
            <span className="hidden md:flex">Início</span>
          </NavLink>

          <NavLink to="/orders">
            <UtensilsCrossed className="h-6 w-6" />
            <span className="hidden md:flex">Pedidos</span>
          </NavLink>
        </nav>
      </div>
    </div>
  )
}
