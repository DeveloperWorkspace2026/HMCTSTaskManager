## HMCTS Task Manager
Project Overview

This project was developed as part of the DTS Developer Technical Test.

The application allows HMCTS caseworkers to manage their daily tasks. Users can create tasks, view existing tasks, update task status, and delete tasks when they are no longer required.

## The project consists of:

ASP.NET Core Web API
SQL Server Database
HTML, CSS, JavaScript and jQuery Frontend
Entity Framework Core
Swagger API Documentation
xUnit Unit Tests
Features
Backend API

## The API provides the following functionality:

Create a new task
Retrieve all tasks
Retrieve a task by ID
Update task status
Delete a task

## Each task contains:

Title
Description
Status
Due Date
Frontend

## The frontend allows users to:

Create tasks
View all tasks
Update task status
Delete tasks

## ## Technologies Used
## Backend
C#
ASP.NET Core Web API
Entity Framework Core
SQL Server

## Frontend
HTML
CSS
JavaScript
jQuery

## Testing
xUnit
Moq

## Database

The application uses SQL Server to store task information.

Before running the application, update the connection string in:

appsettings.json

Example:

"ConnectionStrings": {
    "DefaultConnection": "Server=LAPTOP-1FJO6CGB\\SQLEXPRESS02;Database=HMCTSTaskDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }

Run the database migration:

Update-Database

## Swagger URL:

https://localhost:7292/swagger

## Frontend URL:

https://localhost:7292

## Unit Tests

Unit tests have been added for the service layer.

The following functionality is tested:

Create task
Get all tasks
Get task by ID
Update task status
Delete task

To run tests:

Test → Run All Tests

## Project Structure
Controllers
DTOs
Models
Data
Repositories
Services
Middleware
wwwroot
Tests
