# Shreyas.Grpc.AspNetCore.Server.AutomaticServiceRegistration

Automatic registration and mapping of Grpc services to `IApplicationBuilder` in ASP.NET Core

## Installation

Install the [AutomaticServiceRegistration NuGet Package](https://www.nuget.org/packages/Shreyas.Grpc.AspNetCore.Server.AutomaticServiceRegistration/).

### Package Manager Console

```powershell
Install-Package Shreyas.Grpc.AspNetCore.Server.AutomaticServiceRegistration
```

### .NET Core CLI

```bash
dotnet add package Shreyas.Grpc.AspNetCore.Server.AutomaticServiceRegistration
```

## Usage

This library contains only one extension method that you need to call in order to register all Grpc services that you have in your project. 

Method name - `GrpcServicesEndpointRegistration.MapGrpcServicesFromAssemblies`

## Examples

```csharp
// Here app is instance of WebApplication (IApplicableBuilder)

app.MapGrpcServicesFromAssemblies(Assembly.GetExecutingAssembly());
```

Above method will register all Grpc services which will be there in current executing assembly. It also accepts multiple assemblies to register services.
