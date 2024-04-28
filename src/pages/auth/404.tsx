import { Pizza } from 'lucide-react'
import { Link } from 'react-router-dom'

export function NotFound() {
  return (
    <div className="flex h-screen flex-col items-center justify-center gap-2">
      <Pizza className="h-16 w-16 text-amber-600 dark:text-amber-400" />
      <h1 className="text-center text-4xl font-bold">Página não encontrada</h1>
      <p className="text-accent-foreground">
        Voltar para o{' '}
        <Link to="/" className="text-amber-600 dark:text-amber-400">
          Dashboard
        </Link>
      </p>
    </div>
  )
}
