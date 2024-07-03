import { Pizza } from 'lucide-react'
import { Link, useRouteError } from 'react-router-dom'

export function Error() {
  const error = useRouteError() as Error

  return (
    <div className="flex h-screen flex-col items-center justify-center gap-2">
      <Pizza className="h-16 w-16 text-amber-600 dark:text-amber-400" />
      <h1 className="text-center text-4xl font-bold">
        Whoops, algo aconteceu...
      </h1>
      <p className="text-accent-foreground">
        Um erro aconteceu na aplicação, abaixo você encontra mais detalhes:
      </p>
      <pre>{error?.message || JSON.stringify(error)}</pre>
      <pre className="text-accent-foreground">
        Voltar para o{' '}
        <Link to="/" className="text-amber-600 dark:text-amber-400">
          Dashboard
        </Link>
      </pre>
    </div>
  )
}
