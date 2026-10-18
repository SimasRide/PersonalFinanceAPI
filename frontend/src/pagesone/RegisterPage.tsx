import { useState } from "react";
import type { FormEvent } from "react";

function RegisterPage() {

const [username, setUsername] = useState('');
 const [email, setEmail] = useState('');
 const [password, setPassword] = useState('');
  
 async function handleSubmit(event: FormEvent<HTMLFormElement>) {
  event.preventDefault();

  try {
    // Confirma no terminal do backend se a porta é mesmo 5287.
    const response = await fetch(
      "http://localhost:5287/api/auth/register",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          username,
          email,
          password,
        }),
      }
    );

    // Lê como texto primeiro, porque alguns erros não trazem JSON.
    const text = await response.text();
    let data: {
      message?: string;
      errors?: string[] | Record<string, string[]>;
    } = {};

    try {
      data = text ? JSON.parse(text) : {};
    } catch {
      // Se a resposta não for JSON, usamos uma mensagem genérica.
    }

    if (!response.ok) {
      // Trata erros do Identity e erros de validação do formulário.
      const errors = Array.isArray(data.errors)
        ? data.errors
        : Object.values(data.errors ?? {}).flat();

      alert(
        errors.length > 0
          ? errors.join("\n")
          : data.message ?? `Erro ao registar: HTTP ${response.status}`
      );
      return;
    }

    alert("Conta criada com sucesso!");

    // Limpa a password após um registo bem-sucedido.
    setPassword("");
  } catch {
    // Falhas de ligação ou CORS chegam aqui.
    alert(
      "Não foi possível contactar a API. Verifica se o backend está a correr e confirma o endereço."
    );
  }
}
 return (
   <form onSubmit={handleSubmit}>
     <h1>Register</h1>
      <label htmlFor="username">Username:</label>
<input 
type="text"
id="username"
name="username"
value={username}
onChange={(e) => setUsername(e.target.value)}
required
/>

    <label htmlFor="email">Email:</label>
<input 
type="email"
id="email"
name="email"
value={email}
onChange={(e) => setEmail(e.target.value)}
required
/>

<label htmlFor="password">Password:</label>
<input 
type="password"
id="password"
name="password"
value={password}
onChange={(e) => setPassword(e.target.value)}
required
/>

<button type="submit">Register</button>
 </form>
 );
}

export default RegisterPage;