# Agent Achieve Software End-User Manual

## 1. Product Overview and Deployment Instructions

* **Product Description**

    Agent Achieve is a web-based goal setting, task management, and progress tracking system designed specifically for real estate professionals. The application empowers individual agents and team managers to streamline their workflows, enhance productivity, and focus on achieving their real estate objectives.

* **Tools and Frameworks**

    * **[Visual Studio 2022](https://visualstudio.microsoft.com/vs/):** Integrated Development Environment (IDE).
    * **[SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads):** Free edition of Microsoft SQL Server, suitable for local development.
    * **[.NET 8](https://dotnet.microsoft.com/en-us/download):** Core development framework.
    * **[Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor):** Web UI framework for C# (Part of .NET).
    * **[Telerik UI for Blazor](https://www.telerik.com/blazor-ui):** Suite of UI components for Blazor applications.
    * **[NuGet Packages](https://www.nuget.org/):** Package manager for .NET and should automatically be installed when restoring the solution (key dependencies).
        * **[Entity Framework Core](https://github.com/dotnet/efcore):** Object-relational mapper (ORM) for interacting with databases.
        * **[xUnit](https://xunit.net/):** Unit testing framework for .NET.
        * **[bUnit](https://bunit.dev/):** Testing library specifically designed for Blazor components.
        * **[Moq](https://github.com/devlooped/moq):** Mocking framework for creating test doubles.
        * **[AutoMapper](https://github.com/AutoMapper/AutoMapper):** Library for simplifying object-to-object mapping.

* **External Services**

    * **[Azure SQL Database](https://azure.microsoft.com/en-us/products/azure-sql/database/):** Cloud-based relational database service.
    * **[Azure App Service](https://azure.microsoft.com/en-us/products/app-service/):** Platform for hosting web applications.
    * **[Azure DevOps](https://azure.microsoft.com/en-us/services/devops/) (Repos & Pipelines):** Provides source control (Azure Repos) and CI/CD capabilities (Azure Pipelines).
    * **[Google Authentication](https://developers.google.com/identity) (API integration):** Handles user authentication via Google accounts. 

* **Deployment Instructions**

    * **Development Setup**

        * **Tool Installation:** Ensure you have the following tools setup.
            * **[.NET 8](https://dotnet.microsoft.com/en-us/download):** Download and run the latest installer. 
            * **[Visual Studio 2022](https://visualstudio.microsoft.com/vs/):** Download any version (Community, Professional, or Enterprise) and run the installer.
            * **[SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads):** Download and run the latest installer. 

        * **Environment Configuration**
            * **[Google Authentication](https://developers.google.com/identity) (API integration):** A Google OAuth 2.0 Client ID and secret must be created for your application.
                * Follow the instructions in this article to Create the Google OAuth 2.0 Client ID and secret and setup Authorized Redirect URIs for local and production endpoints. [https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-8.0](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-8.0)
                * **Note:** The PORT used in Authorized Redirect URIs will match your `launchsettings.json`. 
            * **SQL Server Express Database Setup**
                * Ensure SQL Server Express is successfully installed.
                * Create the database and apply migrations by executing  `Update-Database` in the Package Manager Console (select the AgentAchieve.Infrastructure as Default project).
                * Verify the database and tables were created in Microsoft SQL Server Management Studio.
            * **[Telerik UI for Blazor](https://www.telerik.com/blazor-ui):** needs to be configured depending on if you have a license/account or not.
                * **If you have a Telerik license/account.**
                    * Follow the instructions in this article to setup the NuGet feed for Telerik. [https://docs.telerik.com/blazor-ui/installation/nuget](https://docs.telerik.com/blazor-ui/installation/nuget)
                * **If you don't have a Telerikc license/account.**
                    * Uninstall the licensed version and switch to the trial in NuGet Package Manager.
                    * Update `App.razor` to reference trial .js and .css. 

    * **Azure Setup**

        * **Prerequisites:** An Azure Subscription.

        * **Environment Configuration**
            * **[Google Authentication](https://developers.google.com/identity) (API integration):** See above.
            * **[Telerik UI for Blazor](https://www.telerik.com/blazor-ui):**  needs to be configured depending on if you have a license/account or not. See above for code setup. Assuming you are hosting code in Azure DevOps Reops and plan to deploy via Pipelines do the additional steps below.
                * **If you have a Telerik license/account.**
                    * Follow the instructions in this article to create a Telerik NuGet Key. [https://docs.telerik.com/reporting/getting-started/installation/using-nuget-keys](https://docs.telerik.com/reporting/getting-started/installation/using-nuget-keys)
                    * Create Service Connections in Azure DevOps and add the Telerik NuGet Key information.
                * **If you don't have a Telerikc license/account.**
                    * Remove the `NuGetAuthenticate@1` step in `azure-pipelines.yml`. 

            * **[Azure SQL Database](https://azure.microsoft.com/en-us/products/azure-sql/database/)**
                * Follow the instructions in this article to create a new Azure SQL Server and a single Database in Azure Portal. [https://learn.microsoft.com/en-us/azure/azure-sql/database/single-database-create-quickstart?view=azuresql&tabs=azure-portal](https://learn.microsoft.com/en-us/azure/azure-sql/database/single-database-create-quickstart?view=azuresql&tabs=azure-portal)
                * Record the `ADO.NET (SQL authentication)` connection string.

            * **[Azure App Service](https://azure.microsoft.com/en-us/products/app-service/)**
                * Follow the instructions in this article to create a new Azure App Service in Azure Portal with below key settings.  [https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net70&pivots=development-environment-azure-portal](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net70&pivots=development-environment-azure-portal)
                    * Publish: Code
                    * Runtime stack: .NET 8 (LTS)
                    * Operating System: Windows 
                *  Add configuration for `GoogleClientId` and `GoogleClientSecret` that was created/recorded in prior step and add `DefaultConnection` string recorded when creating Azure SQL Database in prior step by clicking `New Application settings` or `New connection string` respectively and entering the information.

            * **Azure DevOps Pipeline**
                * Update `azure-pipelines.yml`
                    * Ensure that task: `NuGetAuthenticate@1` is using the nuGetServiceConnections name created in above step.
                    * Update `AzureWebApp@1` settings to pick a subscription that has permissions to your Azure App Service created in prior step and select App name from dropdown. 
            * **Deploy Code.** 
                * Run your `azure-pipelines.yml` to deploy to your Azure App and apply database migrations. 

## 2. Application Features and User Guide

* **Live Demo site:** [https://agentachievedev.azurewebsites.net/](https://agentachievedev.azurewebsites.net/)