import { login } from '@/services/authService'

export async function loginAndStoreToken(credentials) {
  const response = await login(credentials)
  const token = response?.token

  if (!token) throw new Error('Token no recibido')

  // Lógica de negocio: guardar en localStorage
  localStorage.setItem('auth_token', token)

  return response
}
