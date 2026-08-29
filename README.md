# Financial Overview

Este repositório contém o backend (ASP.NET Core, C#), um frontend mínimo (React + Vite, TypeScript) e um `docker-compose.yml` para desenvolvimento com PostgreSQL.

Sumário rápido
- Backend: `WebApplication1` (C#, .NET 10, EF Core)
- Frontend: `frontend` (React + Vite, TypeScript)
- DB: PostgreSQL (via Docker Compose)

Como correr localmente (sem Docker)
1. Backend
   - Ajusta a connection string no `appsettings.Development.json` ou utiliza variável de ambiente `ConnectionStrings__DefaultConnection`.
   - No terminal (pasta `WebApplication1`):
	 ```powershell
	 dotnet run --urls "http://localhost:5287"
	 ```
   - Verifica `Now listening on: http://localhost:5287` na saída.

2. Frontend
   - Copia `frontend/.env.local.example` para `frontend/.env.local` e ajusta `VITE_API_URL` se necessário.
   - No terminal (pasta `frontend`):
	 ```bash
	 npm install
	 npm run dev
	 ```
   - Acede a `http://localhost:5173`.

Como correr com Docker Compose (recomendado para dev)
1. Define a password do Postgres (opcional):
   - cria um ficheiro `.env` na raiz com `POSTGRES_PASSWORD=umaSenhaSegura`.
2. Executa:
   ```bash
   docker compose up --build
   ```
3. Acede:
   - Frontend: http://localhost:5173
   - Backend: http://localhost:5287/api/accounts

Notas de segurança rápidas
- Não comites segredos (use `.env`, `user-secrets` ou secrets manager).
- Não uses `ASPNETCORE_ENVIRONMENT=Development` em produção.
- Implementa autenticação/autorizações antes de publicar a API.
- O middleware global de erro devolve mensagens genéricas para evitar exposição de detalhes internos.

Próximos passos sugeridos (roadmap)
1. Autenticação (JWT + Identity)
2. CRUD completo de transações e categorias
3. Dashboard com gráficos
4. Testes (unit + integração)
5. CI/CD e Docker image publish

