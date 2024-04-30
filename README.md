---
languages:
- csharp
- aspx-csharp
page_type: sample
description: "A sample application you can use to follow along with Tutorial: Deploy an ASP.NET Core and Azure SQL Database app to Azure App Service."
products:
- azure
- aspnet-core
- azure-app-service
---

# .NET Core MVC sample for Azure App Service

This is a sample application that you can use to follow along with the tutorial at 
[Tutorial: Deploy an ASP.NET Core and Azure SQL Database app to Azure App Service](https://learn.microsoft.com/azure/app-service/tutorial-dotnetcore-sqldb-app). 

Modifications made:
1. Removed Distributed / Redis Cache as there's not a free way to deploy one of those.
1. Upgraded to .NET 8
1. EF Core 8
1. Setup CI/CD via App Services Deployment Center


## Getting started

### Run from Visual Studio

1. git clone https://github.com/Azure-Samples/msdocs-app-service-sqldb-dotnetcore.git
2. cd msdocs-app-service-sqldb-dotnetcore
3. Open *DotNetCoreSqlDb.sln* in Visual Studio.
4. Start debugging.

### Run from command line

Run the following commands in a terminal:

```
git clone https://github.com/Azure-Samples/msdocs-app-service-sqldb-dotnetcore.git
cd msdocs-app-service-sqldb-dotnetcore
dotnet ef database update
dotnet run
```

### Deploy to Azure Web App

1. Open *DotNetCoreSqlDb.sln* in Visual Studio.
1. Deploy to the Azure Web App instance
1. Run any database migrations