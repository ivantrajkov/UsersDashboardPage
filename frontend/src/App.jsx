import { useState } from 'react'
import { Route, Router, Routes } from 'react-router-dom'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { Layout } from './ui/components/layout/Layout'
import { Home } from './ui/pages/HomePage/Home'
import DashboardPage from './ui/pages/DashboardPage/DashboardPage'
import LoginPage from './ui/pages/LoginPage/LoginPage'
import ProtectedRoute from "./security/ProtectedRoute.jsx"
import RegisterPage from "./ui/pages/RegisterPage/RegisterPage.jsx"
import UnauthorizedPage from "./ui/pages/UnauthorizedPage/UnauthorizedPage.jsx"
import LoggedOutPage from './ui/pages/LoggedOutPage/LoggedOutPage.jsx'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage/>}/>
          <Route path="/unauthorized" element={<UnauthorizedPage/>}/>
          <Route path="/loggedOut" element={<LoggedOutPage/>}/>
          <Route element={<ProtectedRoute role={"Admin"} />}>
            <Route path="/dashboard" element={<DashboardPage />} />
          </Route>

        </Route>
      </Routes>
    </>
  )
}

export default App
