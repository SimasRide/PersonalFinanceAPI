import {useState} from 'react';
import {FormEvent} from 'react';

function LoginPage() {
const [email, setEmail] = useState('');
const [password, setPassword] = useState('');
  
 function handleSubmit(event: FormEvent<HTMLFormElement>) {
  event.preventDefault();
alert(`Formulario submetido para ${email}. Ainda não foi criada uma conta.`);
}
  return (
    <form onSubmit={handleSubmit}>
      <h1>Login</h1>

      <label htmlFor="username">Username:</label>
      <input type="text" id="username" name="username" required />
      <label htmlFor="password">Password:</label>

      <input type="password" id="password" name="password" required />
    
      <button type="submit">Login</button>
    </form>
  );
}
export default LoginPage;