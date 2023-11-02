# API de processos de estoque.
Está trata-se da api utilizada para processos de estoque dos usuarios no projeto distribuido chamado **Icarus**.



## Tecnologias utilizadas no projeto.
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) ![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white) ![RabbitMQ](https://img.shields.io/badge/Rabbitmq-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)



## Endpoint para autenticação

#### Realiza get em todos os produtos.

```http
  GET api/produtos/${pagina}/${resultado}
```

| Header | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` | **Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Pagina` | `int` | Parametro para mudança de paginas. |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Resultado` | `int` | Parametro para mudança quantidade de resultados por pagina. |

#### Filtrar produtos por nome.

```http
  GET api/pesquisar/nome/{pagina?}/{resultado?}
```

| Header | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` | **Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Pagina` | `int` | Parametro para mudança de paginas. |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Resultado` | `int` | Parametro para mudança quantidade de resultados por pagina. |


#### Filtrar produto por id.

```http
  GET api/produtos/{id}
```

| Header | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` | **Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Id` | `int` | Parametro para selecionar produto. |



#### Criar novo produto.

```http
  POST api/produtos/novo_produto
```

| Header | Tipo     | Descrição                         |
| :-------- | :------- | :-------------------------------- |
| `Authorization`      | `Authorization` |**Autenticação**. Jwt token |


#### Atualizar produto.

```http
  POST api/produtos/atualizar_produto/{id}
```

| Header | Tipo     | Descrição                         |
| :-------- | :------- | :-------------------------------- |
| `Authorization`      | `Authorization` |**Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Id` | `int` | Parametro para selecionar produto. |


#### Deletar produto.

```http
  POST api/produtos/produto_delete/{id}
```

| Header | Tipo     | Descrição                         |
| :-------- | :------- | :-------------------------------- |
| `Authorization`      | `Authorization` |**Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Id` | `int` | Parametro para selecionar produto. |



## Deployment dotnet

Para rodar este projeto utilizando dotnet realize os seguintes comandos:

```bash
  cd ~/icarus.estoque
```

```bash
  dotnet restore
```

```bash
  cd estoque.service/
```

```bash
  dotnet run
```


## Deployment docker

Para rodar este projeto utilizando docker realize os seguintes comandos:

```bash
  docker run --name=container_estoque -p 5112:5112 k4im/estoque:v0.1
```
