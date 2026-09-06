import axios from 'axios'

const baseURL = import.meta.env.VITE_API_URL ?? 'http://localhost:5287'
const TOKEN_KEY = 'financial-overview-token'

export const api = axios.create({
  baseURL,
  headers: { 'Content-Type': 'application/json' }
})

api.interceptors.request.use((config) => {
  const token = localStorage.getItem(TOKEN_KEY)
  if (token) config.headers.Authorization = `Bearer ${token}`
  return config
})

export interface AuthResponse {
  token: string
  expiresAt: string
  user: { id: string; displayName: string; email: string }
}

export interface AccountResponseDto {
  id: number
  name: string
  type: string
  initialBalance: number
  currency: string
}

export const auth = {
  token: () => localStorage.getItem(TOKEN_KEY),
  save: (data: AuthResponse) => localStorage.setItem(TOKEN_KEY, data.token),
  clear: () => localStorage.removeItem(TOKEN_KEY),
  async login(email: string, password: string) {
    const { data } = await api.post<AuthResponse>('/api/auth/login', { email, password })
    auth.save(data)
    return data
  },
  async register(displayName: string, email: string, password: string) {
    const { data } = await api.post<AuthResponse>('/api/auth/register', { displayName, email, password })
    auth.save(data)
    return data
  }
}

export async function getAccounts() {
  const { data } = await api.get<AccountResponseDto[]>('/api/accounts')
  return data
}

export async function createAccount(payload: Partial<AccountResponseDto>) {
  const { data } = await api.post<AccountResponseDto>('/api/accounts', payload)
  return data
}
