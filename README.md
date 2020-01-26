# BlazorCrud
[![Build Status](https://beckshome.visualstudio.com/BlazorCRUD/_apis/build/status/thbst16.BlazorCrud?branchName=master)](https://beckshome.visualstudio.com/BlazorCRUD/_build/latest?definitionId=4&branchName=master)

Blazor CRUD is a demo application built with the [Blazor](https://blazor.net) framework using the client-side hosting model with WebAssembly in the browser invoking .NET Core REST APIs secured by a JWT service. To browse the two components of the application, follow the links below. For authenticated pages and APIs, use the credentials (user@beckshome.com / Password123).
* [Blazor CRUD Application](https://becksblazor.azurewebsites.net/) - A client side hosted WASM application built using Blazor. The application highlights CRUD data entry for entities, data pagination, client-side validation using Data Annotations, and authentication and authorization using JWT tokens.
* [Blazor CRUD REST API](https://becksblazor.azurewebsites.net/swagger/index.html) - A REST API for CRUD with non-read API calls secured with JWT. The API includes a call to authenticate users and receive a JWT bearer token.

Blazor CRUD uses the following DevOps environment and tools to support a CI / CD process:
* [GitHub Source Code Repository](https://github.com/thbst16/BlazorCrud) - All source code is stored in the GitHub repository, which is where you currently find yourself.
* [Azure DevOps for CI/CD](https://beckshome.visualstudio.com/BlazorCRUD/_build) - Azure DevOps is used for continunous integration and continuous delivery (CI/CD). Builds and deployments are initiated with every cheackin to the main brach of the solution in GitHub.

# Features

* Online Demo Site to Explore the Application
* CI/CD Using Azure DevOps
* Dashboard Page
* Entity Lists with Pagination and Search
* Modal Data Entry Forms with Validations
* Complex Data Entry with Object Graph Validations
* File Upload and Download Using JavaScript Interop
* Batch processing of JSON files (
[Patients](https://raw.githubusercontent.com/thbst16/BlazorCrud/master/BlazorCrud.Shared/Data/SampleData/patients.json),
[Organizations](https://raw.githubusercontent.com/thbst16/BlazorCrud/master/BlazorCrud.Shared/Data/SampleData/organizations.json),
[Claims](https://raw.githubusercontent.com/thbst16/BlazorCrud/master/BlazorCrud.Shared/Data/SampleData/claims.json)
) for data upload.
* REST Interfaces with Swagger Documentation
* Javascript Web Token (JWT) Authentication
* Automation of Azure Infrastructure Setup
* Data Generation to Pre-Populate Thousdands of Entity Records

# Open Source Used

* [AutoMapper](https://github.com/AutoMapper/AutoMapper) for object-object mappings.
* [Azure Fluent Management](https://github.com/Azure/azure-libraries-for-net) for Azure automation from C#.
* [BlazorInputFile](https://github.com/SteveSandersonMS/BlazorInputFile) for file upload.
* [BlazorStorage](https://github.com/cloudcrate/BlazorStorage) for local and session storage in the browser.
* [BlazorStrap](https://github.com/chanan/BlazorStrap) Bootstrap 4 components for Blazor.
* [Blazor.Toastr](https://github.com/sotsera/sotsera.blazor.toaster) for Toastr-style notifications.
* [Bogus](https://github.com/bchavez/Bogus) for data generation.
* [ChartJs.Blazor](https://github.com/mariusmuntean/ChartJs.Blazor) Blazor component that wraps ChartJs widgets for dashboard.
* [FluentValidation](https://github.com/JeremySkinner/FluentValidation) For entity validation, including complex object graph validations. 
* [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle) for Swagger document generation.