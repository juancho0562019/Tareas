# Tasks
<br/>
Este repositorio contiene una API web desarrollada con ASP.NET Core 8 y SqlServer como base de datos. Proporciona un conjunto de endpoints para gestionar tareas

<br/>


## Tecnologias utilizadas

* [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [AspNetCore Identity MongoDbCore](https://github.com/alexandre-spieser/AspNetCore.Identity.MongoDbCore)
* [AspNetCore Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=visual-studio)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)

## Inicio

### Configuracion Base de datos
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost; Initial Catalog=algoritmos;User ID=[Usuario]; Password=[Contraseña]; MultipleActiveResultSets=True;TrustServerCertificate=True"
  },
  
Verificar la clave **ConnectionStrings** en el archivo **appsettings.json** para configurar la conexion a SqlServer.

### Domain

Este proyecto contiene todas las entidades y lógica específicos de la capa de dominio.

### Application

Este proyecto contiene toda la lógica de la aplicación. Depende del proyecto `Domain`, pero no depende de ninguna otra capa o proyecto. Este proyecto define interfaces que son implementadas por proyectos externas. Por ejemplo, si la aplicación necesita acceder a un servicio de notificación, se agregaría una nueva interfaz a la aplicación y se crearía una implementación dentro de la infraestructura.

### Infrastructure

Este proyecto contiene clases para acceder a recursos de base de datos y es donde se ubican las implementaciones de las interfaces creadas en `Applicacion`.

### WebUI

Este proyecto depende de los proyectos `Application` e `Infrastructure`, sin embargo, la dependencia de la `Infrastructure` es solo para utilizar la inyección de dependencia. Por lo tanto, solo *Program.cs* debe hacer referencia a `Infrastructure`.

### Ejecutar migracion
Ubicarse en el proyecto `Infrastructure` y ejecutar `update-database`

### Restaurar paquetes
`dotnet restore`


### Compilar
`dotnet publish -c Release`
