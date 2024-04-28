import { AppLayout } from '@pages/_layouts/app'
import { AuthLayout } from '@pages/_layouts/auth'
import { Dashboard } from '@pages/app/dashboard/dashboard'
import { Orders } from '@pages/app/orders/orders'
import { NotFound } from '@pages/auth/404'
import { SignIn } from '@pages/auth/sign-in'
import { SignUp } from '@pages/auth/sign-up'
import { createBrowserRouter } from 'react-router-dom'

export const router = createBrowserRouter([
  {
    path: '/',
    element: <AppLayout />,
    errorElement: <NotFound />,
    children: [
      { path: '/', element: <Dashboard /> },
      { path: '/orders', element: <Orders /> },
    ],
  },
  {
    path: '/',
    element: <AuthLayout />,
    children: [
      { path: '/sign-in', element: <SignIn /> },
      { path: '/sign-up', element: <SignUp /> },
    ],
  },
])
