// useState guarda valores que podem mudar durante a utilização da página.
// "type FormEvent" importa apenas o tipo do evento.
import { useState, type FormEvent } from "react";

import "./App.css";

function App() {

  const [loginEmail, setLoginEmail] = useState("");

 
  const [loginPassword, setLoginPassword] = useState("");

  /*Envio  a partir de formHTML*/
  function handleLogin(event: FormEvent<HTMLFormElement>) {
    // Impede o recarregamento da página.
    event.preventDefault();

    // Apagar depois 
    console.log({
      email: loginEmail,
      password: loginPassword,
    });
  }

  return (
    // Tudo o que pertence ao formulário fica dentro desta tag.
    <form onSubmit={handleLogin}>

      {/* EMAIL */}
      <label htmlFor="login-email">
        Email:
      </label>

      <input
        id="login-email"
        name="email"
        type="email"
        value={loginEmail}
        onChange={(event) =>
          setLoginEmail(event.target.value)
        }
        required
      />

      {/*PASSWORD*/}

      <label htmlFor="login-password">
        Password:
      </label>

      <input
        id="login-password"
        name="password"
        type="password"
        value={loginPassword}
        onChange={(event) =>
          setLoginPassword(event.target.value)
        }
        required
      />

      {/*  Sem necessidade de repetir onSubmit= visto que ja está no form acima */}
      <button type="submit">
        Log In
      </button>
    </form>
  );
}

export default App;