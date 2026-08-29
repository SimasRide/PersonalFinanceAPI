import React from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import App from './App'
import AccountsList from './pages/AccountsList'
import './styles.css'

const root = createRoot(document.getElementById('root')!)
root.render(
  <React.StrictMode>
	<BrowserRouter>
	  <Routes>
		<Route path="/" element={<App />}> 
		  <Route index element={<AccountsList />} />
		</Route>
	  </Routes>
	</BrowserRouter>
  </React.StrictMode>
)
