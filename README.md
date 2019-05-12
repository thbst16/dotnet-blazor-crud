# BlazorCrud
[![Build Status](https://beckshome.visualstudio.com/BlazorCRUD/_apis/build/status/thbst16.BlazorCrud?branchName=master)](https://beckshome.visualstudio.com/BlazorCRUD/_build/latest?definitionId=4&branchName=master)

Blazor CRUD is a demo application built with the [Blazor](https://blazor.net) framework using the client-side hosting model with WebAssembly and .NET Core REST APIs secured by a JWT service. To browse the two components of the application, follow the links below. For authenticated pages and APIs, use the credentials (user@beckshome.com / Password123).
* [Blazor CRUD Application](http://becksblazor.azurewebsites.net/) - A client-side hosted WASM application built using Blazor. The application highlights CRUD data entry for entities, data pagination, client-side validation using Data Annotations, and authentication and authorization using JWT tokens.
* [Blazor CRUD REST API](http://becksapi.azurewebsites.net) - A REST API for CRUD with non-read API calls secured with JWT. The API includes a call to authenticate users and receive a JWT bearer token.

Blazor CRUD uses the following DevOps environment and tools to support a CI / CD process:
* [GitHub Source Code Repository](https://github.com/thbst16/BlazorCrud) - All source code is stored in the GitHub repository, which is where you currently find yourself.
* [Azure DevOps for CI/CD](https://beckshome.visualstudio.com/BlazorCRUD/_build) - Azure DevOps is used for continunous integration and continuous delivery (CI/CD). In it's current format, Azure DevOps is being used exclusively as a build server.
