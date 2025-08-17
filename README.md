📌 Clinic Manager API

API RESTful para gestão de clínica médica, desenvolvida em .NET 8 com CQRS + MediatR, autenticação JWT e persistência em PostgreSQL.
Este projeto segue boas práticas de arquitetura limpa, com separação em camadas, repositórios e validações robustas.

🚀 Tecnologias Utilizadas

.NET 8 / ASP.NET Core
Entity Framework Core + PostgreSQL
CQRS + MediatR
FluentValidation
JWT Bearer Authentication
Swagger / OpenAPI
Docker
xUnit / MSTest / NUnit

📂 Estrutura do Projeto
📂 ClinicManager
 ┣ 📂 src
 ┃ ┣ 📂 ClinicManager.API              -> API principal
 ┃ ┣ 📂 ClinicManager.Application      -> CQRS (Commands / Queries / Handlers)
 ┃ ┣ 📂 ClinicManager.Core             -> Entidades, Interfaces, Responses
 ┃ ┗ 📂 ClinicManager.Infrastructure   -> Persistência, Repositórios, DbContext
 ┣ 📂 tests                            -> Testes unitários/integrados
 ┣ 📄 README.md
 ┣ 📄 docker-compose.yml
 ┗ 📄 .gitignore

📋 Funcionalidades

✅ Cadastro e gestão de Pacientes, Médicos, Clientes e Serviços
✅ Monitoramento e métricas clínicas
✅ Autenticação JWT (Login, proteção de endpoints)
✅ Paginação em consultas (ex: usuários, monitoramentos)
✅ Validação com FluentValidation
✅ Logging de requests/responses no middleware
✅ Arquitetura CQRS para separação de leitura e escrita
✅ Padrão Repository + Unit of Work

⚙️ Como Rodar Localmente

1. Clone o repositório
git clone https://github.com/seuusuario/ClinicManager.git
cd ClinicManager
2. Configure o banco de dados
No arquivo appsettings.json, configure a connection string do PostgreSQL:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=clinicdb;Username=postgres;Password=senha"
}
3. Rode as migrations
dotnet ef database update --project ClinicManager.Infrastructure
4. Execute a API
dotnet run --project ClinicManager.API

📸 Demonstração

Swagger com todos os endpoints disponíveis
Exemplo de fluxo:
Criar usuário
Autenticar e obter token JWT
Acessar endpoints autenticados

(Adicione prints ou GIFs do Swagger/Insomnia aqui)

📖 Arquitetura

O projeto segue princípios de Clean Architecture:
API Layer → Exposição via Controllers
Application Layer → CQRS (Commands & Queries + Handlers)
Core Layer → Entidades, Interfaces e contratos
Infrastructure Layer → Repositórios, EF Core, DbContext


📌 Autor

👤 Vinicius Vasconcelos









