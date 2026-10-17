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

## Database setup & migrations

1. Required NuGet packages

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design

Install (Package Manager Console)

```powershell
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.Design
```

Or (dotnet CLI)

```sh
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

If using CLI commands (dotnet ef), ensure dotnet-ef is available:

```sh
dotnet tool install --global dotnet-ef
```

2. Connection strings (appsettings.json)

Make sure both contexts use correct connection strings in appsettings.json (or user secrets):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=DashBordDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

If your project uses separate connection names for ApplicationDbContext and DashbordDbContext, add both entries and ensure Program.cs registers both contexts with the correct names.

3. Create migrations and update database

Package Manager Console (Visual Studio)

```powershell
# For ApplicationDbContext
Add-Migration Initial_Application -Context ApplicationDbContext -OutputDir Migrations/Application
Update-Database -Context ApplicationDbContext

# For DashbordDbContext
Add-Migration Initial_Dashbord -Context DashbordDbContext -OutputDir Migrations/Dashbord
Update-Database -Context DashbordDbContext
```

dotnet CLI

```sh
# For ApplicationDbContext
dotnet ef migrations add Initial_Application --context ApplicationDbContext --output-dir Migrations/Application
dotnet ef database update --context ApplicationDbContext

# For DashbordDbContext
dotnet ef migrations add Initial_Dashbord --context DashbordDbContext --output-dir Migrations/Dashbord
dotnet ef database update --context DashbordDbContext
```

Notes:
- Use distinct migration names and separate output directories to keep contexts isolated.
- If you get errors, confirm the context type names and that Program.cs can construct the context (correct constructor/signatures and configuration).
- When running dotnet ef from a solution with multiple projects, you may need to add `--project` and `--startup-project` to target the correct project.

## Run locally

From the repository root, restore and run the main project explicitly. The solution contains a backup project as well, so specifying `DashBord.csproj` avoids an ambiguous-project error.

```powershell
dotnet restore .\DashBord.csproj
dotnet run --project .\DashBord.csproj --launch-profile http
```

Open [http://localhost:5116](http://localhost:5116). Stop the application with `Ctrl+C`.

The `https` launch profile is also available at `https://localhost:7286` (and HTTP at `http://localhost:5116`).

## Run with IIS Express on port 44354

To run the application under IIS Express on port 44354 (HTTPS):

- Option A — edit `Properties\launchSettings.json` and set the IIS Express profile to use `sslPort` 44354 and include `https://localhost:44354` in `applicationUrl`.

```json
"IIS Express": {
  "commandName": "IISExpress",
  "launchBrowser": true,
  "environmentVariables": { "ASPNETCORE_ENVIRONMENT": "Development" },
  "sslPort": 44354,
  "applicationUrl": "https://localhost:44354;http://localhost:44353"
}
```

- Option B — Visual Studio:
  - Right-click project → Properties → Debug.
  - Under Launch profile select "IIS Express".
  - Set App URL to `https://localhost:44354` and enable SSL.
  - Save and start debugging (select IIS Express and run).

Ensure `app.UseStaticFiles()` and `app.UseHttpsRedirection()` are enabled in Program.cs and that the port is not blocked by another process.

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
