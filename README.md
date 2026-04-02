# gRPC-02 - Servicio gRPC de Mantenimiento

Proyecto de ejemplo en .NET 10 que implementa un servicio gRPC con CRUD básico para productos, más servicios adicionales de saludo y manejo de personas.

## 📁 Estructura del proyecto

- `src/GrpcMantenimiento/` - proyecto principal.
  - `Protos/` - definiciones de servicios gRPC (`greet.proto`, `person.proto`, `product.proto`).
  - `Services/` - implementaciones de servicios gRPC (`GreeterService.cs`, `PersonService.cs`, `ProductService.cs`).
  - `Data/` - contexto Entity Framework Core (`GrpcDbContext.cs`).
  - `Migrations/` - migraciones de EF Core para SQLite.
  - `Models/` - modelo de dominio (`Product.cs`).
  - `Program.cs` - configuración y arranque del servidor gRPC.
  - `appsettings.json` / `appsettings.Development.json` - configuración de la aplicación.

## 🚀 Requisitos previos

- .NET SDK 10 (`dotnet --version` debe ser 10.x).
- SQL Server local o SQLite (el proyecto ya contiene migraciones para SQLite).

## ▶️ Ejecución rápida

Desde la raíz de la solución:

```bash
cd src/GrpcMantenimiento
dotnet restore
dotnet build
dotnet ef database update
dotnet run
```

El servidor gRPC se iniciará en `https://localhost:5000` (puede variar según `launchSettings.json`).

## 🔧 Endpoints gRPC

Definidos en Protos:

- `GreeterService` (saludo): `SayHello`, `SayHelloWithDeadline`.
- `PersonService` (personas): CRUD básico de persona (crear, leer, listar, etc.).
- `ProductService` (productos): CRUD de productos usando EF Core sobre SQLite.

## 🛠️ Uso desde cliente gRPC

Puedes usar:

- [grpcurl](https://github.com/fullstorydev/grpcurl)
- .NET gRPC client
- Postman (gRPC mode)

Ejemplo con `grpcurl`:

```bash
grpcurl -insecure -proto Protos/product.proto -d '{"id":1}' localhost:5000 ProductService/GetProduct
```

## 🗃️ Base de datos

- `Data/GrpcDbContext.cs` define contexto EF Core.
- `Migrations/` contiene migración `20260402162340_creationSQLiteDB`.
- Cadena de conexión en `appsettings.json` (ajusta a tu entorno).

## 🧪 Pruebas

No hay pruebas unitarias incluidas en este repositorio. Implementar pruebas de servicios gRPC con xUnit/Moq se recomienda en el futuro.

## 📝 Checklist antes de subir a GitHub

- Incluir `.gitignore` para bin/ obj/ *.db *~
- Verificar que `appsettings` no contenga credenciales sensibles.
- Añadir descripción del proyecto y etiquetas en el repositorio.

## 📌 Recursos

- gRPC .NET: https://learn.microsoft.com/aspnet/core/grpc
- EF Core SQLite: https://learn.microsoft.com/ef/core/providers/sqlite

---

## Autor

Rafael Bolaños Herrera
Basado del curso de Udemy:
ASP.NET Core 8 en Arquitectura gRPC - Taller Sistemas

