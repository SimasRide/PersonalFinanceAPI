import axios from 'axios'

const baseURL = import.meta.env.VITE_API_URL ?? 'http://localhost:5287'

export const api = axios.create({
  baseURL,
  headers: {
	'Content-Type': 'application/json'
  }
})

export interface AccountResponseDto {
  id: number
  name: string
  type: string
  initialBalance: number
  currency: string
}

export async function getAccounts() {
  const resp = await api.get<AccountResponseDto[]>('/api/accounts')
  return resp.data
}

export async function createAccount(payload: Partial<AccountResponseDto>) {
  const resp = await api.post<AccountResponseDto>('/api/accounts', payload)
  return resp.data
}
