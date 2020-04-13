# BlazorCrud
![Build Status](https://beckshome.visualstudio.com/BlazorCRUD/_apis/build/status/thbst16.BlazorCrud?branchName=master)
![Deployment Status](https://beckshome.vsrm.visualstudio.com/_apis/public/Release/badge/4205f4a9-c0ad-4900-aefd-9efecd2543bc/1/2)
![Docker Image Version (latest by date)](https://img.shields.io/docker/v/thbst16/blazor-crud?logo=docker)
![Uptime Robot ratio (7 days)](https://img.shields.io/uptimerobot/ratio/7/m784732895-fc1b844a033d7645bb9eefe9?logo=http)

Blazor CRUD is a demo application built with the [Blazor](https://blazor.net) framework using the client-side hosting model with WebAssembly in the browser invoking .NET Core REST APIs secured by a JWT service. To browse the two components of the application, follow the links below. For authenticated pages and APIs, use the credentials (user@beckshome.com / Password123).
* [Blazor CRUD Application](https://becksblazor.azurewebsites.net/) - A client side hosted WASM application built using Blazor. The application highlights CRUD data entry for entities, data pagination, client-side validation using Data Annotations, and authentication and authorization using JWT tokens.
* [Blazor CRUD REST API](https://becksblazor.azurewebsites.net/swagger/index.html) - A REST API for CRUD with non-read API calls secured with JWT. The API includes a call to authenticate users and receive a JWT bearer token.

Blazor CRUD uses the following DevOps environment and tools to support a CI / CD process:
* [GitHub Source Code Repository](https://github.com/thbst16/BlazorCrud) - All source code is stored in the GitHub repository, which is where you currently find yourself.
* [Azure DevOps for CI/CD](https://beckshome.visualstudio.com/BlazorCRUD/_build) - Azure DevOps is used for continunous integration and continuous delivery (CI/CD). Builds and deployments are initiated with every cheackin to the main brach of the solution in GitHub.
* [DockerHub for Container Storage](https://hub.docker.com/r/thbst16/blazor-crud) - DockerHub is where BlazorCrud tagged Docker images are stored after being created in the CI/CD pipeline. The can also be pulled and run locally by using the command "docker pull thbst16/blazor-crud".

# Features

* Online demo site to explore the application
* CI/CD Using Azure DevOps
* Docker container generation as a single deployable Docker image
* Dashboard page
* Entity lists with pagination and search
* Modal data entry forms with validations
* Complex data entry with object graph validations
* File upload and download using JavaScript Interop
* Batch processing of JSON files (
[Patients](https://raw.githubusercontent.com/thbst16/BlazorCrud/master/BlazorCrud.Shared/Data/SampleData/patients.json), 
[Organizations](https://raw.githubusercontent.com/thbst16/BlazorCrud/master/BlazorCrud.Shared/Data/SampleData/organizations.json), and
[Claims](https://raw.githubusercontent.com/thbst16/BlazorCrud/master/BlazorCrud.Shared/Data/SampleData/claims.json)
) for data upload.
* REST interfaces with Swagger documentation
* Javascript Web Token (JWT) authentication
* Automation of Azure Infrastructure Setup
* Data generation to pre-populate thousdands of entity records
* Typeahead for entity lookup on modal data entry screens (preview functionality)

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
* [Typeahead](https://github.com/Blazored/Typeahead) for typeahead lookup of key entities.