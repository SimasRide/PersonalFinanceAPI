# Financial Overview

Aplicação de finanças pessoais com backend ASP.NET Core (.NET 10), frontend React + TypeScript e PostgreSQL.

## O que já está disponível

- Registo e login com ASP.NET Core Identity
- Autenticação JWT com expiração de 2 horas
- Rotas da API protegidas por defeito
- Frontend responsivo com formulários, validação e feedback de erros
- Listagem de contas ligada à API, sem dados mock
- Segredos fora do repositório

## Arranque com Docker

1. Copia `.env.example` para `.env`.
2. Preenche `POSTGRES_PASSWORD` e `JWT_KEY` com valores fortes.
3. Executa:

```bash
docker compose up --build
```

Frontend: http://localhost:5173  
Backend: http://localhost:5287

## Arranque local

Na pasta `WebApplication1`, guarda a configuração fora do Git:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=financialoverview;Username=postgres;Password=<a-tua-password>"
dotnet user-secrets set "Jwt:Key" "<uma-chave-aleatoria-com-pelo-menos-32-caracteres>"
```

Como o Identity adiciona tabelas de utilizadores, cria e aplica a migração:

```powershell
dotnet ef migrations add AddIdentity
dotnet ef database update
dotnet run --urls "http://localhost:5287"
```

Depois, na pasta `frontend`:

```bash
npm install
npm run dev
```

## Endpoints de autenticação

- `POST /api/auth/register`
- `POST /api/auth/login`

Os restantes endpoints exigem o header `Authorization: Bearer <token>`.

## Segurança

Nunca publiques `.env`, connection strings ou chaves JWT. A credencial PostgreSQL anteriormente presente no histórico deve ser substituída na base de dados antes de qualquer publicação.
