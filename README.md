# Automobile Rental Management API 🚗

API desenvolvida em **.NET 8** para gerenciamento de aluguéis de veículos, com foco em escalabilidade, testes automatizados e uso de tecnologias modernas como mensageria e armazenamento em nuvem simulado localmente.

---

## 🚀 Subindo a aplicação

Para rodar toda a infraestrutura de forma local, basta executar:

```bash
docker compose up -d
```

Esse comando irá subir:

- **PostgreSQL** – Banco de dados relacional principal
- **MongoDB** – Armazenamento de notificações
- **RabbitMQ** – Fila de mensagens para notificações
- **Azurite** – Simulador local do Azure Blob Storage para armazenamento de imagens

---

## 🧪 Executando os testes

Após subir os containers, execute todos os testes para validar as regras de negócio da aplicação:

```bash
dotnet test
```

> Serão executados testes unitários e testes de integração automaticamente.

---

## ▶️ Rodando o projeto

Após subir os containers e executar os testes, você pode rodar o projeto com:

```bash
dotnet run --project AutomobileRentalManagementAPI
```

---

## 📤 Upload de Imagem da CNH (via base64)

Para o upload da CNH, envie uma string **base64** representando a imagem.

A imagem será armazenada no Azurite Blob Storage local. Exemplo de retorno de URL:

```
http://127.0.0.1:10000/devstoreaccount1/images/{nome-unico-da-imagem}.jpg
```

---

## 🧰 Tecnologias utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/)
- [MongoDB](https://www.mongodb.com/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite) (via Docker)
- [xUnit](https://xunit.net/) para testes
- [Docker Compose](https://docs.docker.com/compose/) para orquestração

---
