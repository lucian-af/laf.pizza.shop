import { Home, Pizza, UtensilsCrossed } from 'lucide-react'

import { AccountMenu } from './account-menu'
import { NavLink } from './nav-link'
import { ThemeToggle } from './themes/theme-toggle'
import { Separator } from './ui/separator'

export function Header() {
  return (
    <div className="border-b">
      <div className="flex h-16 items-center gap-2 px-6 md:gap-6">
        <Pizza className="h-6 w-6 text-amber-600 dark:text-amber-400" />

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
        <div className="ml-auto flex items-center gap-2">
          <ThemeToggle />
          <AccountMenu />
        </div>
      </div>
    </div>
  )
}
