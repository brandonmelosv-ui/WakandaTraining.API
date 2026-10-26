# WakandaTraining.API

API desenvolvida em **C# com ASP.NET Core** para o projeto Wakanda Training. O backend disponibiliza endpoints HTTP para operações de usuários e cursos, utiliza autenticação com **JWT (JSON Web Token)** e persiste os dados em **SQLite**.

> Projeto de estudo e portfólio desenvolvido para praticar construção de APIs, integração com banco de dados e autenticação. A API ainda precisa ser hospedada para ficar acessível pela internet; executá-la localmente não a torna pública.

## Tecnologias

- C#
- ASP.NET Core Web API (.NET 10)
- Entity Framework Core
- SQLite
- JWT Bearer Authentication
- HTTP / JSON

## Funcionalidades

- Cadastro e login de usuários
- Geração e validação de tokens JWT
- Endpoint de perfil protegido por autenticação
- Endpoints relacionados a cursos
- Persistência de dados usando SQLite
- Migrações do Entity Framework Core

## Estrutura do projeto

```text
WakandaTraining.API/
├── WakandaTraining.API.slnx
└── WakandaTraining.API/
    ├── Controllers/       # Endpoints HTTP
    ├── Data/              # Contexto do banco de dados
    ├── Migrations/        # Migrações do Entity Framework Core
    ├── Models/            # Modelos da aplicação
    ├── Properties/        # Configurações de execução local
    ├── Views/             # Página administrativa
    ├── Program.cs         # Configuração da aplicação e serviços
    └── appsettings.json   # Configuração não secreta
```

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Visual Studio ou Visual Studio Code (opcional)

## Executar localmente

1. Clone este repositório:

   ```bash
   git clone https://github.com/brandonmelosv-ui/WakandaTraining.API.git
   cd WakandaTraining.API
   ```

2. Configure a chave JWT como segredo local. No terminal, na pasta raiz do repositório, execute:

   ```bash
   dotnet user-secrets init --project .\WakandaTraining.API\WakandaTraining.API.csproj
   dotnet user-secrets set "Jwt:Key" "COLOQUE_AQUI_UMA_CHAVE_ALEATORIA_FORTE" --project .\WakandaTraining.API\WakandaTraining.API.csproj
   ```

   Substitua o texto de exemplo por uma chave aleatória forte. Não publique a chave no GitHub nem a inclua no `appsettings.json`.

3. Restaure as dependências e execute a API:

   ```bash
   dotnet restore .\WakandaTraining.API\WakandaTraining.API.csproj
   dotnet run --project .\WakandaTraining.API\WakandaTraining.API.csproj
   ```

4. Confira no terminal o endereço local informado pela aplicação. Use esse endereço para testar os endpoints HTTP. O arquivo `WakandaTraining.API.http` também contém uma requisição de exemplo.

A conexão padrão usa SQLite (`Data Source=wakanda.db`). O banco local não é incluído no repositório; a aplicação poderá criá-lo ou atualizá-lo conforme a configuração de inicialização e as migrações do projeto.

## Segurança

- A chave JWT deve ser mantida em **User Secrets** durante o desenvolvimento local ou em um gerenciador de segredos no ambiente hospedado.
- Não envie arquivos de banco de dados local, credenciais ou configurações secretas ao repositório.
- Tokens JWT devem ser tratados como credenciais: não os compartilhe publicamente.

## Integração com o aplicativo

O aplicativo mobile em Flutter/Dart é mantido em um repositório separado. A comunicação entre o aplicativo e esta API ocorre por HTTP, com dados em JSON. Para usar o aplicativo fora do ambiente local, será necessário configurar uma URL de API acessível pelo dispositivo e hospedar o backend.

## Status

Em desenvolvimento. Este repositório contém o código-fonte da API; a publicação no GitHub não representa hospedagem ou disponibilidade pública dos endpoints.

---

Desenvolvido por [brandonmelosv-ui](https://github.com/brandonmelosv-ui) como projeto de aprendizado e portfólio.
