# Getting Started
This is a multi-project solution dotnet template for developing an enterprise-level Web API with.NET 6 ASP.NET Core, following Clean Architecture principles and API best practices.

# Prerequisites
You will need the following tools:
* [Visual Studio Code or Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (version 17.0.0 Preview 7.0 or later)
* [.NET Core SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)

## Step 1: Install Template
1. Open command terminal and enter the folloing command to install .Net 6 Web Template
```
dotnet new --install Net6WebTemplate::1.0.0-beta.1
```
2. using the dotn cli list the installed templates with the following command
```
dotnet new --list 
```
Then reveiw the list to ensure `NET 6 Web API Template` was installed successfully.
```
These templates matched your input:

Template Name                                Short Name           Language    Tags
-------------------------------------------  -------------------  ----------  -----------------------------------------
ASP.NET Core Empty                           web                  [C#],F#     Web/Empty
NET 6 Web API Template                       net6webapi           [C#]        .NET 6 Web API Template/Cloud/Service/Web
```

## Step 2: Create Project with Template
1.Navigate to the location you would like to create your new project and execute the following command.
```
dotnet new net6webapitemplate -o "MyProject"`
```

This should create the following project structure

MyProject\
	\src
		\MyProject.Api
		\MyProject.Application
		\MyProject.Domain
		\MyProject.infrastructure
		\MyProject.Persistence
	\tests
		\MyProject.UnitTests
		\MyProject.IntegrationTests
	.gitattributes
	.gitignore


## Step 3: Setup Database
The Visual Studio 2022 installation also installs the SQL Server Express edition that will 
allow you to use a lightweight version of the SQL Server database engine for your development.
You may also install a database engine of your choosing.

1. Install Entity Framework Core CLI tools with the following command 
```
dotnet tool install --global dotnet-ef
```
2. Create inital database migration
```
dotnet ef migrations script 0 InitialCreate

```
3. Push migration to development database 
```
dotnet ef database update --context MyProjectDbContext
```


## Step 4: Launch Application
1. Navigate to the project directory, then run the following commands to build your application
```
dotnet restore 
dotnet build --no-restore --configuration Release
```
2. Launch application with the following command
```
dotnet run MyProject.Api.dll
```