// Importa o React e as ferramentas para apresentar a aplicação no navegador.
import React from "react";
//Url navegação
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import LoginPage from "./pagesone/LoginPage";
import RegisterPage from "./pagesone/RegisterPage";

// Define as rotas da aplicação e a página apresentada em cada endereço.
function App() {
  return (
    <Routes>

      <Route path="/" element={<LoginPage />} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
    </Routes>
  );
}

// Liga a aplicação ao elemento HTML com o id "root".
// O ! indica ao TypeScript que esse elemento existe.
ReactDOM.createRoot(document.getElementById("root")!).render(

    <BrowserRouter>
      <App />
    </BrowserRouter>
);