# DotNetAPI

This repository contains a .NET API project created as part of a Udemy course. The project focuses on implementing authentication, creating CRUD (Create, Read, Update, Delete) endpoints, connecting to a database, and more.

## Table of Contents

1. [Introduction](#introduction)
2. [Getting Started](#getting-started)
   - [Prerequisites](#prerequisites)
   - [Installation](#installation)
3. [Project Structure](#project-structure)
4. [Authentication Setup](#authentication-setup)
5. [Database Connection](#database-connection)
6. [Endpoints](#endpoints)

## Introduction

This .NET API project, named DotNetAPI, is designed to demonstrate key concepts learned during a Udemy course. Commits in this repository cover the implementation of JWT-based authentication, CRUD operations, database connectivity, CORS policy setup, and the creation of custom controllers.

## Getting Started

### Prerequisites

- .NET SDK
- Any additional prerequisites specific to your project

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/haiderinam7/DotNetAPI.git
   ```

2. Navigate to the project directory:

   ```bash
   cd DotNetAPI
   ```

   ...

## Project Structure

- `Controllers/`: Folder containing API controllers.
- `Models/`: Folder for data models.
- `DTOs/`: Folder for Data Transfer Objects (DTOs).
- `Services/`: Folder for any additional services.

## Authentication Setup

1. Create JWT:

   ```bash
   dotnet add package System.IdentityModel.Tokens.Jwt
   ```

   ...

2. Setup Auth Controller:

   ...

3. Setup Password Key:

   ...

## Database Connection

1. Install Packages:

   ```bash
   dotnet add package dapper
   ```

   ...

## Endpoints

- Create User DTO Model
- Create PUT and POST Endpoints
- Add Controller Endpoints
- Create Delete Endpoint
- ...
