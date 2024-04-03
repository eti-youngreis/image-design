import { Navigate, createBrowserRouter } from 'react-router-dom'
import Layout from '../layouts/MainLayout'
import Login from '../pages/Login'
export const router = createBrowserRouter([
    {
        path: '',
        element: <Layout />

    },
    {
        path: 'login',
        element: <Login/>
    }
])