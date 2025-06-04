<h1 align="center">CNAB - API</h1>

<p align="center">
  <a href="https://learn.microsoft.com/pt-br/dotnet/"><img alt="DotNet 6" src="https://img.shields.io/badge/.NET-5C2D91?logo=.net&logoColor=white&style=for-the-badge" /></a>
  <a href="https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/"><img alt="C#" src="https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white&style=for-the-badge" /></a>
  <a href="https://www.postgresql.org/"><img alt="PostgreSQL" src="https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white" /></a>
  <a href="https://www.docker.com/"><img alt="Docker" src="https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white" /></a>
  <a href="LICENSE"><img alt="License: MIT" src="https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge" /></a>
</p>

## 💻 Project

This repository contains a Web API designed to manage `CNAB` data by handling records and supporting file uploads for data normalization.

The project was developed for academic purposes, based on the following challenge: [CNAB Challenge](https://github.com/PauloAlves8039/dotnet-desafio-cnab).

## ✅ Technical Decisions

- `Web API`:  I chose to build an API to provide more flexibility for developing a front-end application using a modern SPA framework.
- `Clean Architecture`: The goal of applying this architecture was to make the API scalable and well-structured for adding new features.
- `Rich Domain Model`: This approach was used to organize domain classes and align them closely with the application's business rules.

## 🚀 Technologies and Tools

This project was built using the following stack:

- **Backend:**  
  - `.NET 8.0`
  - `ASP.NET Core WebAPI`
  - `C#`
  - `Entity Framework Core`
  - `Clean Architecture`
  - `Rich Domain Model`

- **Testing:**  
  - `XUnit`
  - `Moq`
  - `FluentAssertions`

- **Infrastructure:**  
  - `PostgreSQL`
  - `Docker`
  - `Inversion of Control`

## 💾 How to Run Locally

```bash
# Clone the repository
git clone https://github.com/PauloAlves8039/dotnet-cnab-api.git

# Navigate to the project folder
cd src/CNAB.WebAPI

# Restore dependencies
dotnet restore

# Run the project
dotnet run
```

## 🧪 How to Run (Docker)

```bash
# Clone the repository
git clone https://github.com/PauloAlves8039/dotnet-cnab-api.git

# Navigate to the project folder
cd src/CNAB.WebAPI

# Run the command to build the image and start the container
docker-compose up --build
```

## ⬇️ How to Use

```bash
# After creating and starting the containers, restore the database tables using the following commands:

# Run migrations created with ApplicationDbContext
dotnet ef database update --context CNAB.Infra.Data.Context.ApplicationDbContext --startup-project ../CNAB.WebAPI

# Run migrations created with IdentityApplicationDbContext
dotnet ef database update --context CNAB.Infra.Data.Context.IdentityApplicationDbContext --startup-project ../CNAB.WebAPI

# Commands used to create the migrations:

# For ApplicationDbContext
dotnet ef migrations add InitialCreate --context CNAB.Infra.Data.Context.ApplicationDbContext --startup-project ../CNAB.WebAPI

# For IdentityApplicationDbContext
dotnet ef migrations add AddIdentity --context CNAB.Infra.Data.Context.IdentityApplicationDbContext --startup-project ../CNAB.WebAPI

```

## 🔒 Authentication and Authorization
```bash
# After setting up the API to access the endpoints, you must create a user with the following email:

admin@localhost

# You can set any password you prefer.

Example: YourPassword@2015

# Note: Once created, the admin@localhost user will have access to all API endpoints, including an admin-only section.
# All other users will have access to the API, except for the admin-only section.

```

## 🌎 URL

```bash
# Whether running locally or by container in Docker, the URL is the same, the API will be available at: 

http://localhost:8080/swagger/index.html

```

## 🎯 API Endpoints

### 🛠️ Admin

| HTTP Method | Endpoint URL                                         | Description                                  |
|-------------|------------------------------------------------------|----------------------------------------------|
| `GET`       | `http://localhost:8080/api/Admin/total-balance`      | Retrieves the total balance of all transactions. |
| `GET`       | `http://localhost:8080/api/Admin/store-count`        | Returns the total count of registered stores.   |
| `GET`       | `http://localhost:8080/api/Admin/transaction-count`  | Returns the total count of transactions.        |

---

### 📄 CNAB

| HTTP Method | Endpoint URL                                         | Description                   |
|-------------|------------------------------------------------------|-------------------------------|
| `POST`      | `http://localhost:8080/CNAB/upload-cnab-file`        | Uploads a CNAB file for processing. |

---

### 🏬 Store

| HTTP Method | Endpoint URL                                         | Description                           |
|-------------|------------------------------------------------------|---------------------------------------|
| `GET`       | `http://localhost:8080/api/Store`                    | Lists all stores.                     |
| `GET`       | `http://localhost:8080/api/Store/{id}`               | Gets details of a specific store by ID. |
| `POST`      | `http://localhost:8080/api/Store`                    | Creates a new store.                  |
| `PUT`       | `http://localhost:8080/api/Store/{id}`               | Updates an existing store by ID.      |
| `DELETE`    | `http://localhost:8080/api/Store/{id}`               | Deletes an existing store by ID.      |

---

### 💳 Transaction

| HTTP Method | Endpoint URL                                              | Description                              |
|-------------|-----------------------------------------------------------|------------------------------------------|
| `GET`       | `http://localhost:8080/api/Transaction`                   | Lists all transactions.                  |
| `GET`       | `http://localhost:8080/api/Transaction/{id}`              | Gets details of a specific transaction.  |
| `POST`      | `http://localhost:8080/api/Transaction`                   | Creates a new transaction.               |
| `PUT`       | `http://localhost:8080/api/Transaction/{id}`              | Updates an existing transaction.         |
| `DELETE`    | `http://localhost:8080/api/Transaction/{id}`              | Deletes an existing transaction.         |

---

### 👤 User

| HTTP Method | Endpoint URL                                     | Description          |
|-------------|--------------------------------------------------|----------------------|
| `POST`      | `http://localhost:8080/api/User/register`        | Registers a new user. |
| `POST`      | `http://localhost:8080/api/User/login`           | Logs in a user.       |


## 👤 Author

<a href="https://github.com/PauloAlves8039">
  <img src="https://avatars.githubusercontent.com/u/57012714?v=4" width=70 />
</a>

**[Paulo Alves](https://github.com/PauloAlves8039)**

## 📝 License

This project is licensed under the [MIT License](LICENSE).
