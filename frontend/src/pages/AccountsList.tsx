import { useEffect, useState } from 'react'
import { AccountResponseDto, getAccounts } from '../api/client'

export default function AccountsList() {
  const [accounts, setAccounts] = useState<AccountResponseDto[] | null>(null)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    getAccounts().then(setAccounts).catch((err) =>
      setError(err.response?.status === 401 ? 'A tua sessão expirou. Entra novamente.' : 'Não foi possível carregar as contas.')
    )
  }, [])

  if (error) return <div className="state-message">{error}</div>
  if (!accounts) return <div className="state-message">A carregar contas…</div>

  return (
    <section className="accounts-page">
      <h1>As tuas contas</h1>
      <p>Uma visão simples do teu dinheiro.</p>
      <div className="accounts-grid">
        {accounts.map((account) => (
          <article className="account-card" key={account.id}>
            <strong>{account.name}</strong>
            <p>{account.type}</p>
            <p>{new Intl.NumberFormat('pt-PT', { style: 'currency', currency: account.currency }).format(account.initialBalance)}</p>
          </article>
        ))}
      </div>
    </section>
  )
}
