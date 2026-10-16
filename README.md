# DashBord

DashBord is an ASP.NET Core MVC web application for browsing and managing products. It includes product details, account registration and sign-in, and a checkout flow.

## Requirements

- .NET SDK compatible with the project's `net7.0` target
- SQL Server (local or remote) with a database configured for the application

> .NET 7 is out of support. The project currently targets .NET 7 and uses .NET 7 Entity Framework packages; plan to upgrade it to a supported .NET version for ongoing development.

## Configure the database

The application reads the `DefaultConnection` connection string for both its product data and ASP.NET Identity data. Set it locally with .NET user secrets rather than committing credentials to `appsettings.json`:

```powershell
dotnet user-secrets init --project .\DashBord.csproj
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your SQL Server connection string>" --project .\DashBord.csproj
```

The SQL Server and database must be reachable, and the database must have the schema expected by the application.

## Run locally

From the repository root, restore and run the main project explicitly. The solution contains a backup project as well, so specifying `DashBord.csproj` avoids an ambiguous-project error.

```powershell
dotnet restore .\DashBord.csproj
dotnet run --project .\DashBord.csproj --launch-profile http
```

Open [http://localhost:5116](http://localhost:5116). Stop the application with `Ctrl+C`.

The `https` launch profile is also available at `https://localhost:7286` (and HTTP at `http://localhost:5116`).

## Main technologies

- ASP.NET Core MVC and Razor Pages
- ASP.NET Core Identity
- Entity Framework Core with SQL Server
- MailKit

## Project layout

- `Controllers/` — MVC and API controllers
- `Models/` — product, cart, customer, invoice, and payment models
- `Views/` — Razor views
- `Areas/Identity/` — Identity pages, user model, and Identity database context
- `Migrations/` — Entity Framework Core migrations
- `wwwroot/` — static assets
- `DashBord.csproj` — primary application project
- `DashBord - Backup.csproj` — backup project file
