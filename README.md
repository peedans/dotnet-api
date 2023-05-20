# Dotnet Api

## Getting Started
```
$ cd [ProjectFolder]/app 
$ dotnet restore
```
restores the packages listed in the project's .csproj file and their dependencies

## Initial Database

- Add a new migration with the specified name for the specified context.
  If there is a migrations folder, you can skip to Update Database

```
$ dotnet ef migrations add [MigrationName] -c [ContextName]
```

### Example 

- Use the name "InitialModel" or "InitialCreate" for the first time in migrations.

```
$ dotnet ef migrations add InitialModel -c DBMasterContext 
```
- If there are model modifications, name them according to what we have modified

```
$ dotnet ef migrations add UpdateColumnNameAuthor -c DBMasterContext
```
## Update Database

- updates the database schema to the latest migration for the specified context
```
$ dotnet ef database update -c [ContextName]
```
### Example 
```
$ dotnet ef database update -c DBMasterContext
```


## Run
- run the application and watches for changes to the source code. 
```
$ dotnet watch run
```
