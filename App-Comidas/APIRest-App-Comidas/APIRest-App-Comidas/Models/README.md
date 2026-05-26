# 🍕 APIRest-App-Comidas — Documentación del Proyecto

Proyecto: RESTful Web API para una aplicación de entrega de comidas.

---

## Resumen ejecutivo (breve)
API en ASP.NET Core 10 que expone endpoints CRUD para gestionar usuarios, restaurantes, productos, pedidos, cupones y entidades relacionadas. Usa Entity Framework Core con SQL Server (host en SOMEE). Es un MVP con lógica mayormente en controladores; recomendable introducir capas de servicio/repositorio y seguridad JWT antes de producción.

---

## 1) Tipo de proyecto y tech stack
- Tipo: RESTful Web API (ASP.NET Core)
- .NET target: .NET 10
- Lenguaje: C# 14.0
- ORM: Entity Framework Core
- Base de datos: SQL Server (SOMEE Cloud — APIRest-ComidasDB.mssql.somee.com)
- Hosting previsto: https://www.APIRest-Comidas.somee.com
- IDE recomendado: Visual Studio Community 2026
- Terminal preferido: PowerShell

---

## 2) Arquitectura y patrones
- Patrón actual: Capas simples; controladores contienen la mayor parte de la lógica CRUD.
- Flujo predominante: Controller -> AppDbContext (EF Core) -> BD.
- Observaciones: No hay capas Service/Repository ni autenticación/autorization implementadas. Recomendado:
  - Introducir Service y Repository para separación de responsabilidades.
  - Añadir JWT + Identity, validaciones (FluentValidation), middleware de excepciones y Swagger.

---

## 3) Estructura de carpetas (simplificada)
APIRest-App-Comidas/ ├─ Controllers/         // Roles, Usuarios, Restaurantes, Categorias, Productos, Pedidos, Valoraciones, Cupones, CuponesApartados, DetallePedidos, MetodosPago, UbicacionUsuarios ├─ Models/              // Entidades: Rol, Usuario, Restaurante, Categoria, Producto, Pedido, DetallePedido, Valoracion, Cupon, CuponApartado, MetodoPago, UbicacionUsuario │  └─ README.md         // Este archivo ├─ Data/ │  └─ AppDbContext.cs   // DbContext con DbSet<T> y OnModelCreating ├─ Program.cs └─ appsettings.json     // Cadena de conexión y logging

Rutas y nombres de archivos exactos se encuentran en el workspace abierto.

---

## 4) Modelos / entidades clave y relaciones (resumen)
- `Rol` (1:N) → `Usuario`
- `Usuario` (1:N) → `Restaurante`, `Pedido`, `Valoracion`, `UbicacionUsuario`
- `Categoria` (1:N) → `Restaurante`, `Producto`, `Cupon`
- `Restaurante` (1:N) → `Producto`, `Pedido`, `Valoracion`; FK a `Usuario`, `Categoria`
- `Producto` (N:1) → `Restaurante`, `Categoria`
- `Pedido` (1:N) → `DetallePedido`; FK a `Usuario`, `Restaurante`, `MetodoPago`, `Cupon` (opcional)
- `DetallePedido` → `Pedido`, `Producto`
- `Valoracion` → `Usuario`, `Restaurante`
- `Cupon` → puede asociarse a pedidos; propiedades: `Codigo`, `Titulo`, `Descuento`, `EsPorcentaje`, `FechaExpiracion`, `Activo`, `Stock`
- `CuponApartado` → cupón reservado por email (UsuarioEmail + Codigo + Descuento...)
- `MetodoPago` → 1:N `Pedido`
- `UbicacionUsuario` → direcciones por usuario

(Propiedades y anotaciones de validación están en los archivos `Models/*.cs`.)

---

## 5) Controladores y propósito general
Cada controlador sigue un patrón uniforme con endpoints:
- `List` (GET) — devuelve todos
- `Create` (PUT) — crea entidad
- `Update` (POST) — actualiza entidad
- `Search` (GET) — búsqueda por campo típico
- `Delete` (DELETE) — elimina por id

Controladores principales:
- `RolesController`, `UsuariosController`, `RestaurantesController`, `CategoriasController`,
  `ProductosController`, `PedidosController`, `DetallePedidosController`,
  `ValoracionesController`, `CuponesController`, `CuponesApartadosController`,
  `MetodosPagoController`, `UbicacionUsuariosController`.

---

## 6) Endpoints (base de producción)
Base: `https://www.APIRest-Comidas.somee.com` (ajustar a entorno local cuando corresponda)

Ejemplos (todos siguen el mismo patrón [List, Create, Update, Search, Delete]):

- Roles: `/roles/List`, `/roles/Create`, `/roles/Update`, `/roles/Search?nombreRol=...`, `/roles/Delete?id=...`
- Usuarios: `/usuarios/List`, `/usuarios/Create`, ... `/usuarios/Delete?id=...`
- Restaurantes: `/restaurantes/...`
- Categorias: `/categorias/...`
- Productos: `/productos/...`
- Pedidos: `/pedidos/...`
- DetallePedidos: `/detallepedidos/...`
- Valoraciones: `/valoraciones/...`
- Cupones: `/cupones/...`
- CuponesApartados: `/cuponesapartados/...`
- MetodosPago: `/metodospago/...`
- UbicacionUsuarios: `/ubicacionusuarios/...`

(Ver `Controllers/` para rutas exactas y cuerpos JSON requeridos.)

---

## 7) Flujo de datos típico (ejemplo: crear pedido)
1. Cliente envía PUT `/pedidos/Create` con JSON del `Pedido`.
2. `PedidosController.Create` recibe DTO/entidad y hace `_context.Pedidos.Add(temp)`.
3. `SaveChanges()` de EF Core genera SQL INSERT y lo ejecuta en la BD.
4. BD aplica constraints FKs, índices y persiste el registro.
5. Controlador devuelve mensaje de éxito o error genérico.

---

## 8) Configuraciones relevantes
- `appsettings.json` contiene `ConnectionStrings: DefaultConnection` apuntando a `APIRest-ComidasDB.mssql.somee.com`.
- `AppDbContext` configura precisión decimal para montos (`18,2`) y precisión GPS (`18,10`) en `OnModelCreating`.
- `Program.cs` registra `AddDbContext<AppDbContext>(options => options.UseSqlServer(...))` y `AddControllers()`.

---

## 9) Observaciones y recomendaciones (priorizadas)
1. Implementar autenticación y autorización (JWT + Identity).
2. Extraer lógica de negocio a capas `Service` y persistencia a `Repository`.
3. Validación de entrada robusta (FluentValidation).
4. Middleware global de manejo de excepciones y logging (Serilog).
5. Documentar API con Swagger/OpenAPI.
6. Añadir tests unitarios e integración (xUnit, Moq).
7. Revisar nullable reference types y advertencias de compilación (usar tipos anulables donde corresponda).

---

## 10) Información del workspace (local)
- Ruta raíz del proyecto: `C:\Proyectos-React\React\App-Comidas\APIRest-App-Comidas\`
- Archivos abiertos en el IDE:
  - `Program.cs`
  - `appsettings.json`
  - `Data\AppDbContext.cs`
  - `Models\Usuario.cs`
  - `Models\Categoria.cs`
  - `Models\Restaurante.cs`
  - `Models\README.md` (actual)
- Repositorio Git: `C:\Proyectos-React\React` (remote: `https://github.com/AlexJRQ32/React`, branch: `main`)
- Terminal preferido: `powershell.exe`

---

## Uso rápido / primeros pasos
1. Ajustar `appsettings.json` con credenciales seguras.
2. Ejecutar migraciones / aplicar script SQL en la base de datos SOMEE.
3. `dotnet run` para levantar localmente.
4. Probar endpoints con Postman usando la URL base adecuada.

---

### Nota final
Este README resume el contexto técnico y arquitectónico completo del proyecto tal como se discutió en la sesión. Si quieres, puedo:
- Generar un `README.md` ya escrito en el archivo del proyecto (lo pego por ti).
- Crear una colección Postman con los endpoints.
- Sugerir una estructura de `Service/Repository` y mostrar cómo refactorizar un controlador ejemplo.

