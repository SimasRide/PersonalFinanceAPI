
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=financialoverview;Username=postgres;Password=MINHA PASSWORD" --project .\WebApplication1\WebApplication1.csproj
user-secrets set: guarda a configuração de desenvolvimento fora do repositório.
ConnectionStrings:DefaultConnection: nome da ligação que o teu backend procura.
Host=localhost;Port=5432: 

# Editor docker
docker exec -it financialoverview-db sh

# pSQL
psql -U "$POSTGRES_USER" -d financialoverview



- Invoke-RestMethod: envia um pedido HTTP e interpreta a resposta.
- -Uri: indica o endereço do endpoint de contas.
- -Method Get: pede a consulta dos dados, sem criar nem alterar registos.

Atalhos base guardar :

Editar e Guardar código

|---|---|
| `Ctrl + S` | Guardar o ficheiro atual. |
| `Ctrl + K`, depois `S` | Guardar todos os ficheiros abertos. |
| `Ctrl + Z` | Desfazer a última alteração. |
| `Ctrl + Y` | Refazer uma alteração desfeita. |
| `Ctrl + C` | Copiar o texto selecionado no editor. |
| `Ctrl + X` | Cortar o texto selecionado. |
| `Ctrl + V` | Colar. |
| `Ctrl + A` | Selecionar todo o conteúdo. |
| `Ctrl + F` | Procurar no ficheiro atual. |
| `Ctrl + H` | Procurar e substituir no ficheiro atual. |
| `Ctrl + Shift + F` | Procurar em todo o projeto. |
| `Alt + ↑ / ↓` | Mover a linha ou seleção para cima ou para baixo. |
| `Shift + Alt + ↑ / ↓` | Duplicar a linha ou seleção. |
| `Ctrl + K`, depois `Ctrl + C` | Comentar as linhas selecionadas. |
| `Ctrl + K`, depois `Ctrl + U` | Retirar os comentários das linhas selecionadas. |
| `Shift + Alt + F` | Formatar o código, se houver um formatador configurado. |
| `Ctrl + Space` | Mostrar sugestões de código. |

NAVEGAR NO VS CODE

|---|---|
| `Ctrl + Shift + P` | Abrir a paleta de comandos, como `Developer: Reload Window`. |
| `Ctrl + P` | Procurar e abrir um ficheiro pelo nome. |
| `Ctrl + Shift + E` | Abrir o explorador de ficheiros. |
| `Ctrl + Shift + M` | Mostrar erros e avisos no painel Problems. |
| `Ctrl + B` | Mostrar ou esconder a barra lateral. |
| `Ctrl + J` | Mostrar ou esconder o painel inferior. |
| `Ctrl + Tab` | Alternar entre ficheiros abertos. |
| `Ctrl + W` | Fechar a aba atual. |
| `F12` | Ir para a definição de uma classe ou método. |
| `F2` | Renomear um símbolo e atualizar as referências reconhecidas. |
| `Ctrl + .` | Mostrar correções e ações disponíveis para o código. |