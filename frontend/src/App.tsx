import { Link, Outlet, useNavigate } from 'react-router-dom'
import { auth } from './api/client'

export default function App() {
  const navigate = useNavigate()
  const loggedIn = Boolean(auth.token())

  function logout() {
    auth.clear()
    navigate('/login')
  }

  return (
    <div className="app-shell">
      <header className="topbar">
        <Link className="brand" to="/">
          <span className="brand-mark">F</span>
          <span>Financial Overview</span>
        </Link>
        <nav>
          {loggedIn ? (
            <>
              <Link to="/">Contas</Link>
              <button className="nav-button" onClick={logout}>Sair</button>
            </>
          ) : (
            <Link to="/login">Entrar</Link>
          )}
        </nav>
      </header>
      <main className="page"><Outlet /></main>
    </div>
  )
}
