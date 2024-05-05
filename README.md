# laf.pizza.shop

Sistema para gerenciamento de pedidos de uma pizzaria.

# 1 - API

## Pré-requisitos

Antes de começar, verifique se você tem estes recursos instalados em sua máquina:

- **.NET SDK**: [Download e instalação](https://dotnet.microsoft.com/download)
- **PostgreSQL**: [Download e instalação](https://www.postgresql.org/download/)
  - Você também pode usar o **PostgreSQL** em um container Docker. [Passo a passo aqui!](https://hub.docker.com/_/postgres)

## Configuração

1. Clone este repositório para a sua máquina local:

    ```bash
    git clone https://github.com/lucian-af/laf.pizza.shop.git
    ```

2. Navegue até o diretório do projeto:

    ```bash
    cd seu-repo
    ```

3. Abra o arquivo `appsettings.json` e configure a conexão com o banco de dados PostgreSQL:

    ```json
    {
      "ConnectionStrings": {
        "PizzaShop": "Host=seu-host;Port=5432;Database=seu-banco-de-dados;Username=seu-usuario;Password=sua-senha"
      }
    }
    ```


4. Para usar dados fakes e testar a aplicação, use esse parâmetro no arquivo `appsettings.json`:

    ```json
    {
      "PizzaShopConfigs": {
        "Mode": "presentation"
      }
    }
    ```

5. Você pode usar o comando abaixo para executar as migrations de forma manual, ou iniciar a API (Passo 5) que as migrations serão executadas automaticamente:

    ```bash
    dotnet ef database update
    ```

## Executando a API

1. Execute o seguinte comando para iniciar a API:

    ```bash
    dotnet run
    ```

2. Acesse a API em `http://localhost:5000` no seu navegador para visualizar a documentação da API ou use sua ferramenta de teste de API.

## Rotas

- Acesse a pasta `Endpoints` no projeto para visualizar as rotas disponíveis.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir uma **issue** ou enviar um **pull request**.

## Licença

Este projeto está licenciado sob a Licença MIT - consulte o arquivo [LICENSE](LICENSE) para obter detalhes.

---

Lembre-se de personalizar as informações acima com os detalhes específicos do seu projeto. Boa sorte com a sua API! 🚀