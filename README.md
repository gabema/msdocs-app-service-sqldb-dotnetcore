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

# Modifications to Tutorial to get it to run on free tier services

1. Upgrade to .NET 8
1. Upgrade to Entity Framework Core 8
1. Use a local in memory cache instead of the Azure Redis Cache since there is no free tier option.
1. Add a Github action for CI/CD deployment of App service and db migration script
1. Instead of using the `web app database` option we will create the needed resources manually
    1. Create the Resource Group **rg**-*name*-*env*-*region*
    1. Create the Azure SQL Service
        1. Apply the Preview Offer for a free DB Instance!
        1. Setup Database Name **db**-*name*-*env*-*region*
        1. Setup the Server name **sql**-*name*-*env*-*region*
        1. Select SQL And Microsoft Entra Authentication
    1. Create the App Service **app**-*name*-*env*-*region*
        1. Create the App service Plan **asp**-*name*-*env*-*region*
        1. Create an App Insights instance **appi**-*name*-*env*-*region*
1. Add a Managed Identity to the Resource Group
    1. Create a new `User Assigned Managed Identity`
    1. Add it to the resource group
    1. Add the `Contributor` Role of the Managed Identity to the groupo
1. In Web App > Deployment Center
    1. Select Source: GitHub
    1. Specify the Managed Identity created in the previous step
1. In Visual Studio Publish via the provided YML file


## Warnings about using a local cache instead of a distributed cache

1. This is important since we are running on the free tiers of both the App Service and Azure SQL instances to save the redundant database lookups but if scaling beyond a since app service instance
could lead to inconsistencies if the App Service is scaled up.
