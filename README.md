# WIP: .NET 7 + Angular Template 

## Preparation of Env

### Run MS SQL in Container:

`docker run -e ACCEPT_EULA=Y -e MSSQL_SA_PASSWORD=again@1234  -p 1433:1433 --name mssql --hostname mssql -d   mcr.microsoft.com/mssql/server:2022-latest`

### Install .NET 7

### Install global EF tool:

`dotnet tool install --global dotnet-ef`

### Install Angular cli

'npm install -g @angular/cli'

Use update-migration-script.ps1 to generate db migration script.