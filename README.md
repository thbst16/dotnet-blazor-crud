# dotnet-blazor-crud
[![Build Status](https://beckshome.visualstudio.com/dotnet-blazor-crud/_apis/build/status/thbst16.dotnet-blazor-crud?branchName=master)](https://beckshome.visualstudio.com/dotnet-blazor-crud/_build/latest?definitionId=10&branchName=master)
![Docker Image Version (latest by date)](https://img.shields.io/docker/v/thbst16/blazor-crud?logo=docker)
![Uptime Robot ratio (7 days)](https://img.shields.io/uptimerobot/ratio/7/m784732895-fc1b844a033d7645bb9eefe9?logo=http)

Blazor CRUD is a demo application built with the [Blazor](https://blazor.net) framework using the client-side hosting model with WebAssembly in the browser invoking .NET Core REST APIs secured by a JWT service. To browse the two components of the application, follow the links below. For authenticated pages and APIs, use the credentials (admin / admin).
* [Blazor CRUD Application](https://becksblazor.azurewebsites.net/) - A client side hosted WASM application built using Blazor. The application highlights CRUD data entry for entities, data pagination, client-side validation using Data Annotations, and authentication and authorization using JWT tokens.
* [Blazor CRUD REST API](https://becksblazor.azurewebsites.net/swagger/index.html) - A REST API for CRUD with non-read API calls secured with JWT. The API includes a call to authenticate users and receive a JWT bearer token.

Blazor CRUD uses the following DevOps environment and tools to support a CI / CD process:
* [GitHub Source Code Repository](https://github.com/thbst16/BlazorCrud) - All source code is stored in the GitHub repository, which is where you currently find yourself.
* [Azure DevOps for CI/CD](https://beckshome.visualstudio.com/BlazorCRUD/_build) - Azure DevOps is used for continunous integration and continuous delivery (CI/CD). Builds and deployments are initiated with every cheackin to the main brach of the solution in GitHub.
* [DockerHub for Container Storage](https://hub.docker.com/r/thbst16/blazor-crud) - DockerHub is where BlazorCrud tagged Docker images are stored after being created in the CI/CD pipeline. The can also be pulled and run locally by using the command "docker pull thbst16/blazor-crud".

# Screens

### Home page with BlazorCRUD features
![BlazorCrud Home Page](https://s3.amazonaws.com/s3.beckshome.com/20220213-blazorcrud-home.jpg)
### Login page
![BlazorCrud Login Page](https://s3.amazonaws.com/s3.beckshome.com/20220213-blazorcrud-login.jpg)
### Paginated results and search
![BlazorCrud Search Page](https://s3.amazonaws.com/s3.beckshome.com/20220213-blazorcrud-search.jpg)
### Edit page for complex objects with validations
![BlazorCrud Data Edit](https://s3.amazonaws.com/s3.beckshome.com/20220213-blazorcrud-edit.jpg)

# Features

* Online demo site to explore the application
* CI/CD Using Azure DevOps
* Docker container generation as a single deployable Docker image
* Entity lists with pagination and search
* Data entry forms with validations
* Complex data entry with object graph validations
* File upload and download using JavaScript Interop
* Batch processing of JSON files
* REST interfaces with Swagger documentation
* Javascript Web Token (JWT) authentication
* Data generation to pre-populate thousdands of entity records

# Motivation and Credits

I would not have got this project completed without the vast knowledge of experienced peers at my disposal. Referenced below are items that were specifically helpful with specific functional attributes of the solution.

* [Blazor Navigation - Go Back](https://stackoverflow.com/questions/62561926/blazor-navigation-manager-go-back)
* [Blazor Server Crud in VS Code](https://dev.to/rineshpk/blazor-server-crud-app-using-visual-studio-code-2b2g)
* [Blazor Web Assembly - User Registration and Login](https://jasonwatmore.com/post/2020/11/09/blazor-webassembly-user-registration-and-login-example-tutorial#main-layout-razor)
* [Building a Blazor Paging Component](https://gunnarpeipman.com/blazor-pager-component/)
* [Integration Fluent Validation with Blazor](https://blog.stevensanderson.com/2019/09/04/blazor-fluentvalidation/)
* [Lifelike Test Data Generation with Bogus](http://dontcodetired.com/blog/post/Lifelike-Test-Data-Generation-with-Bogus)
* [User Registration and Login with Example API](https://jasonwatmore.com/post/2022/01/07/net-6-user-registration-and-login-tutorial-with-example-api#users-controller-cs)
