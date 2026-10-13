import { useState } from 'react';
import { FormEvent } from 'react';

function LoginPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    alert(` No account has been created for email ${email} yet.`);
  }

  return (
    <form onSubmit={handleSubmit}>
      <h1>Login</h1>

      <label htmlFor="email">Email:</label>
      <input type="email" id="email" name="email" value={email} onChange={(e) => setEmail(e.target.value)} required />

      <label htmlFor="password">Password:</label>
      <input type="password" id="password" name="password" value={password} onChange={(e) => setPassword(e.target.value)} required />

      <button type="submit">Login</button>
    </form>
  );
}

export default LoginPage;