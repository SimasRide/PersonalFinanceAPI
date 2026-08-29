import React, { useEffect, useState } from 'react'
import { getAccounts, AccountResponseDto } from '../api/client'

export default function AccountsList() {
  const [accounts, setAccounts] = useState<AccountResponseDto[] | null>(null)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
	getAccounts()
	  .then(setAccounts)
	  .catch((err) => setError(err.message || 'Erro'))
  }, [])

  if (error) return <div>Erro: {error}</div>
  if (!accounts) return <div>Carregando...</div>

  return (
	<div>
	  <h2>Contas</h2>
	  <ul>
		{accounts.map((a) => (
		  <li key={a.id}>
			<strong>{a.name}</strong> — {a.type} — {a.initialBalance} {a.currency}
		  </li>
		))}
	  </ul>
	</div>
  )
}
