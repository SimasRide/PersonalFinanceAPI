import { useState } from "react";
import type { FormEvent } from "react";

function RegisterPage() {

const [username, setUsername] = useState('');
 const [email, setEmail] = useState('');
 const [password, setPassword] = useState('');
  
 function handleSubmit(event: FormEvent<HTMLFormElement>) {
  event.preventDefault();
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