import { FormEvent, useState } from 'react'
import { AxiosError } from 'axios'
import { Link, Navigate, useNavigate } from 'react-router-dom'
import { auth } from '../api/client'

type Props = { mode: 'login' | 'register' }

export default function AuthPage({ mode }: Props) {
  const navigate = useNavigate()
  const isRegister = mode === 'register'
  const [displayName, setDisplayName] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')
  const [submitting, setSubmitting] = useState(false)

  if (auth.token()) return <Navigate to="/" replace />

  async function submit(event: FormEvent) {
    event.preventDefault()
    setError('')
    setSubmitting(true)

    try {
      if (isRegister) await auth.register(displayName, email, password)
      else await auth.login(email, password)
      navigate('/')
    } catch (exception) {
      const response = (exception as AxiosError<{ message?: string; errors?: string[] }>).response?.data
      setError(response?.message ?? response?.errors?.[0] ?? 'Ocorreu um erro. Tenta novamente.')
    } finally {
      setSubmitting(false)
    }
  }

  return (
    <section className="auth-layout">
      <div className="auth-copy">
        <span className="eyebrow">O teu dinheiro, mais claro</span>
        <h1>Decide melhor.<br /><span>Vive mais leve.</span></h1>
        <p>Acompanha contas, movimentos e objetivos num único lugar, com uma visão simples das tuas finanças.</p>
        <div className="feature-list">
          <span>Visão centralizada</span>
          <span>Dados protegidos</span>
          <span>Sem complicações</span>
        </div>
      </div>

      <div className="auth-card">
        <h2>{isRegister ? 'Criar conta' : 'Bem-vindo de volta'}</h2>
        <p className="card-intro">
          {isRegister ? 'Começa a organizar as tuas finanças.' : 'Introduz os teus dados para continuar.'}
        </p>

        <form className="auth-form" onSubmit={submit}>
          {isRegister && (
            <label className="field">
              Nome
              <input value={displayName} onChange={(e) => setDisplayName(e.target.value)}
                autoComplete="name" minLength={2} required placeholder="O teu nome" />
            </label>
          )}
          <label className="field">
            Email
            <input type="email" value={email} onChange={(e) => setEmail(e.target.value)}
              autoComplete="email" required placeholder="nome@exemplo.pt" />
          </label>
          <label className="field">
            Palavra-passe
            <input type="password" value={password} onChange={(e) => setPassword(e.target.value)}
              autoComplete={isRegister ? 'new-password' : 'current-password'} minLength={8} required
              placeholder="Mínimo de 8 caracteres" />
          </label>
          {isRegister && <span className="password-note">Usa maiúscula, minúscula e número.</span>}
          {error && <div className="form-error" role="alert">{error}</div>}
          <button className="primary-button" disabled={submitting}>
            {submitting ? 'A processar…' : isRegister ? 'Criar a minha conta' : 'Entrar'}
          </button>
        </form>

        <p className="switch-auth">
          {isRegister ? 'Já tens conta? ' : 'Ainda não tens conta? '}
          <Link to={isRegister ? '/login' : '/register'}>{isRegister ? 'Entrar' : 'Criar conta'}</Link>
        </p>
      </div>
    </section>
  )
}
