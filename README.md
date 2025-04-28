# Customers Management API

API para gerenciamento de clientes, desenvolvida com foco em boas práticas de arquitetura, padrões de projeto e testes automatizados.

## 📚 Tecnologias Utilizadas
- ASP.NET Core 9
- SQL Server
- Docker & Docker Compose
- AutoMapper
- xUnit para testes
- FluentValidation

## 🖥️ Pré-requisitos

Antes de rodar o projeto, você precisa ter instalado:

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/) (opcional)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (caso prefira rodar sem docker)
- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/) ou [Visual Studio Code](https://code.visualstudio.com/) com extensões C#

## 🚀 Como Rodar o Projeto (com Docker)

1. Clone o repositório:
   ```bash
   git clone https://github.com/ojunkes/customers-management-api.git

2. Navegue até o diretório raiz do projeto:
   ```bash
   cd customers-management-api

3. Abra um terminal no diretório raiz e execute o seguinte comando:
   ```bash
   docker-compose up -d
   
4. Uma vez que os containers estejam rodando, acesse a documentação da API via Swagger:
   ```bash
   http://localhost:8080/swagger/index.html

Agora você pode interagir com a API usando o Swagger ou um cliente de API de sua preferência (como Postman ou Insomnia).
