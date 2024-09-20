# mos-register-user-func

Microservice Order System - Register Microservice
 
[See Wiki for details about the Register User Function](https://github.com/HammerheadShark666/mos-register-az-function/wiki)

This project is an **Azure Function** designed to register customer details. It is built using **.NET 8**, interacts with an **SQL Server** database for storage, and sends messages to an **Azure Service Bus** for further processing. The function is set up with a **CI/CD pipeline** for seamless deployment.

## Features

- **Customer Registration**: Accepts customer data and saves it to the SQL Server database.
- **Azure SQL Database**: Used for persistent storage of customer information.
- **Azure Service Bus**: Publishes messages for further processing, ensuring reliable communication between services.
- **Scalable Serverless Architecture**: Utilizes Azure Functions for on-demand execution and scaling.
- **CI/CD Pipeline**: Automated build and deployment using **Azure DevOps** or **GitHub Actions**.

---

## Technologies Used

- **.NET 8**
- **C#**
- **Azure Functions**
- **SQL Server** (Azure SQL)
- **Azure Service Bus**
- **GitHub Actions** for CI/CD

---
