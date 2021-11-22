<img align="left" width="116" height="116" src="https://raw.githubusercontent.com/marlonajgayle/Net6ApiWebTemplate/develop/src/Content/.template.config/icon.png" />

# .NET 6 Web Api Template
[![Build](https://github.com/marlonajgayle/Net6WebApiTemplate/actions/workflows/dotnet.yml/badge.svg?branch=develop)](https://github.com/marlonajgayle/Net6WebApiTemplate/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/marlonajgayle/Net6WebApiTemplate/actions/workflows/codeql-analysis.yml/badge.svg?branch=develop)](https://github.com/marlonajgayle/Net6WebApiTemplate/actions/workflows/codeql-analysis.yml)

This is a multi-project solution dotnet template for creating an enterprise-level Web API using .NET 6 ASP.NET Core using the
Clean Architecture principles and API best practicies.

## Purpose
This template was designed to create a standard to create .NET 6 Web APIs with enterprise-level considerations.

The solution template consist of scafolding for API versioning, CQRS, email, localization, logging, IP rate limiting, JWT, Open API, validation, unit and integration testing.

## Getting Started
Use the instructions provided below to get the solution up and running.

### Prerequisites
You will need the following tools:
* [Visual Studio Code or Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (version 17.0.0 Preview 7.0 or later)
* [.NET Core SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)

### Instructions
1. Install the latest [.NET Core 6 SDK](https://dotnet.microsoft.com/download). 
2. Run `dotnet new --install Net6WebApiTemplate::1.0.0-preview.1` to install the project template
3. Then navigate to the location you would like to create to project
4. Run `dotnet new net6webapitemplate -o "MyProject"` to create a new project

### Docker Setup
ASP.NET Core Web API uses HTTPS and relies on certificates for trust, identity and encryption. 
To run Net5WebTemplate application Docker over HTTPS during development do the following:
1. Generate certificate using 'dotnet dev-certs' (for localhost use Only!).

Note: Update the docker-compose file with dev-cert password used.

On Windows using Linux Containers
```
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx  -p your_password
dotnet dev-certs https --trust
````
When using PowerShell, replace %USERPROFILE% with $env:USERPROFILE.

On macOS or Linux
```
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p { password here }
dotnet dev-certs https --trust
```
2. Build and run Docker containers run Docker compose located in the solution directory
```
docker-compose -f 'docker-compose.yml' up --build
```

### Database Setup
To setup the SQL Server database following the instrcutions below:
1. Reveiw the connection string in appsettings.Local.json and update the database name.
2. Run `dotnet ef migrations add Initial --context <ProjectName>DbContext` to add migation with EF Core 
3. Run `dotnet ef database update Initial` to create application database.

## Contributions
- [Demar-j](https://github.com/Demar-j) - .
- [mahousen](https://github.com/mahousen) - .

## Credits
This solution's structure was heavily infuenced by [Jason Taylor's](https://github.com/jasontaylordev) Clean Architecture model.
Icon made by [Flat Icons](https://www.flaticon.com/authors/flat-icons) from [www.flaticon.com](https://www.flaticon.com/)


## Versions
The [main](https://github.com/marlonajgayle/Net6WebApiTemplate/main) branch is running .NET 6.0

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/marlonajgayle/Net6WebApiTemplate/main/LICENSE.md) [main](https://github.com/marlonajgayle/Net6WebApiTemplate/main) branch is running .NET 6.0
file for details.