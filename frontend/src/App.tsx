import React from 'react'
import { Outlet, Link } from 'react-router-dom'

export default function App() {
  return (
	<div style={{ padding: 16, fontFamily: 'Segoe UI, Roboto, Arial' }}>
	  <header style={{ marginBottom: 16 }}>
		<h1>Financial Overview</h1>
		<nav>
		  <Link to="/">Contas</Link>
		</nav>
	  </header>

	  <main>
		<Outlet />
	  </main>

	  <footer style={{ marginTop: 24, fontSize: 12, color: '#666' }}>
		API: variável VITE_API_URL ou http://localhost:5287
	  </footer>
	</div>
  )
}
