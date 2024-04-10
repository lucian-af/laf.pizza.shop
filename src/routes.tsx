import { Dashboard } from '@pages/app/dashboard'
import { SignIn } from '@pages/auth/sign-in'
import { createBrowserRouter } from 'react-router-dom'

export const router = createBrowserRouter([
  { path: '/', element: <Dashboard /> },
  { path: '/signin', element: <SignIn /> },
])
