# Persistence Layer
The persistence layer is responsible for defining and managing interactions with a database or datastore.

## Performance Considerations
Database perfromance is a large and complext topic as such please make reference to EF Core documentation to ensure the 
best performance can be achieved and avoid common pitfalls. Please see [EF Core Performance](https://docs.microsoft.com/en-US/ef/core/performance)

Entity Framework Core CLI tools manage database migrations.

## Installing tools
Visual Studio 2019 Package Manager Console
```
Install-Package Micorsoft.EntityFrameworkCore.Tools
```

Update EF Core Tools
```
Update-Package Micorsoft.EntityFrameworkCore.Tools
```

## Create Migrations
Creates a new migration.
```
Add-Migration <Description> -Context Net6WebApiTemplateDbContext -OutputDir Migrations
```

## Remove Migrations
Removes the last added migration (rolls back the code changes that were done for the migration).
```
Remove-Migration -Context Net6WebApiTemplateDbContext
```

Revert all migrations in database
```
Upadte-Database 0 -Context Net6WebApiTemplateDbContext
```

## Apply Migrations
Applies the created migrations to the database.
```
Update-Database -Context Net6WebApiTemplateDbContext
```

## Drop Database
Delete database
```
Drop-Database -Context Net6WebApiTemplateDbContext
```