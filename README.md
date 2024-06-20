# Trybe Hotel

## Descrição do Projeto

O Trybe Hotel é uma aplicação que oferece um sistema completo para gerenciar informações sobre cidades, hotéis, quartos, clientes e reservas. Este projeto inclui funcionalidades para cadastro e login de clientes, além da capacidade de buscar hotéis próximos com base em um endereço fornecido.

## Tecnologias Utilizadas

![.NET Core](https://img.shields.io/badge/.NET%20Core-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![ASP.NET](https://img.shields.io/badge/ASP.NET-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![SQLServer](https://img.shields.io/badge/SQLServer-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![xUnit](https://img.shields.io/badge/xUnit-512BD4?style=for-the-badge&logo=xunit&logoColor=white)

## Executando a Aplicação Localmente

Para executar a aplicação localmente, siga os passos abaixo:

1. Dentro do diretório `src`, execute o comando para restaurar as dependências do projeto:

   ```bash
   dotnet restore
   ```

2. Inicie os serviços utilizando Docker Compose:

   ```bash
   docker-compose up -d
   ```

3. Após os serviços estarem em execução, navegue para o diretório `src/TrybeHotel` e execute o comando para aplicar as migrações do banco de dados:

   ```bash
   dotnet ef database update
   ```

A documentação da API está disponível em [http://localhost:8080/swagger](http://localhost:8080/swagger).

## Observações

-   Além das etapas descritas anteriormente, o projeto utiliza Docker Compose para facilitar a execução do ambiente de desenvolvimento. Certifique-se de que o Docker e o Docker Compose estejam instalados em sua máquina antes de prosseguir com as etapas acima.
