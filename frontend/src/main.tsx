import React from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom'
import App from './App'
import { auth } from './api/client'
import AuthPage from './pages/AuthPage'
import AccountsList from './pages/AccountsList'
import './styles.css'

function ProtectedRoute({ children }: { children: React.ReactElement }) {
  return auth.token() ? children : <Navigate to="/login" replace />
}

createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<App />}>
          <Route index element={<ProtectedRoute><AccountsList /></ProtectedRoute>} />
          <Route path="login" element={<AuthPage mode="login" />} />
          <Route path="register" element={<AuthPage mode="register" />} />
        </Route>
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
)
