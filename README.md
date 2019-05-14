# BlazorCrud
[![Build Status](https://beckshome.visualstudio.com/BlazorCRUD/_apis/build/status/thbst16.BlazorCrud?branchName=master)](https://beckshome.visualstudio.com/BlazorCRUD/_build/latest?definitionId=4&branchName=master)

Blazor CRUD is a demo application built with the [Blazor](https://blazor.net) framework using the client-side hosting model with WebAssembly in the browser invoking .NET Core REST APIs secured by a JWT service. To browse the two components of the application, follow the links below. For authenticated pages and APIs, use the credentials (user@beckshome.com / Password123).
* [Blazor CRUD Application](https://becksblazor.azurewebsites.net/) - A client-side hosted WASM application built using Blazor. The application highlights CRUD data entry for entities, data pagination, client-side validation using Data Annotations, and authentication and authorization using JWT tokens.
* [Blazor CRUD REST API](https://becksapi.azurewebsites.net/index.html) - A REST API for CRUD with non-read API calls secured with JWT. The API includes a call to authenticate users and receive a JWT bearer token.

Blazor CRUD uses the following DevOps environment and tools to support a CI / CD process:
* [GitHub Source Code Repository](https://github.com/thbst16/BlazorCrud) - All source code is stored in the GitHub repository, which is where you currently find yourself.
* [Azure DevOps for CI/CD](https://beckshome.visualstudio.com/BlazorCRUD/_build) - Azure DevOps is used for continunous integration and continuous delivery (CI/CD). Builds and deployments are initiated with every cheackin to the main brach of the solution in GitHub.

# Features

* Online Demo Site to Explore the Application
* CI/CD Using Azure DevOps
* Dashboard Page
* Tables with Pagination
* Data Entry Forms with Validations
* File Upload and Download Using JavaScript Interop
* REST Interfaces with Swagger Documentation
* Javascript Web Token (JWT) Authentication
* Automation of Azure Infrastructure Setup
* Data Generation to Pre-Populate Thousdands of Entity Records

# Open Source Used

* [Blazorstrap](https://github.com/chanan/BlazorStrap) Bootstrap 4 components for Blazor.
* [ChartJs.Blazor](https://github.com/mariusmuntean/ChartJs.Blazor) Blazor component that wraps ChartJs widgets for dashboard.
* [BlazorStorage](https://github.com/cloudcrate/BlazorStorage) for local and session storage in the browser.
* [Blazor.Toastr](https://github.com/sotsera/sotsera.blazor.toaster) for Toastr-style notifications.
* [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle) for Swagger document generation.
* [AutoMapper](https://github.com/AutoMapper/AutoMapper) for object-object mappings.
* [Bogus](https://github.com/bchavez/Bogus) for data generation.
* [Azure Fluent Management](https://github.com/Azure/azure-libraries-for-net) for Azure automation rom C#.