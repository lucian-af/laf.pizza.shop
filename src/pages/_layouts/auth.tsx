import { Pizza } from 'lucide-react'
import { Outlet } from 'react-router-dom'

export function AuthLayout() {
  return (
    <div className="relative flex min-h-screen flex-col gap-2 antialiased md:grid md:grid-cols-2">
      <div className="relative left-0 top-0 flex w-full flex-col justify-between border-r border-foreground/5 bg-muted p-8 text-muted-foreground md:p-10">
        <div className="flex items-center gap-3 text-lg text-foreground">
          <Pizza className="h-5 w-5" />
          <span className="font-semibold">pizza.shop</span>
        </div>
      </div>

      <div className="relative flex min-h-min overflow-y-auto md:items-center md:justify-center">
        <Outlet />
      </div>

      <div className="absolute bottom-0 left-0 right-0 flex h-[40px] items-center justify-center border-t border-foreground/5 text-muted-foreground md:justify-start md:border-none md:px-10 md:max-sm:w-1/5">
        <footer className="text-sm">
          Painel do parceiro &copy; pízza.shop - {new Date().getFullYear()}
        </footer>
      </div>
    </div>
  )
}
