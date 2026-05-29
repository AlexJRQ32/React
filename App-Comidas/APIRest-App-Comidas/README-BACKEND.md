# Contexto extendido del backend de App-Comidas

## 1. Ubicación y stack

Este backend está en la carpeta:

- `c:\Proyectos-React\React\App-Comidas\APIRest-App-Comidas\APIRest-App-Comidas`

Es una API ASP.NET Core con:

- `Program.cs` que configura servicios de controladores.
- Entity Framework Core con SQL Server.
- `AppDbContext` que define los `DbSet` de los modelos.
- Controladores MVC en la carpeta `Controllers`.

## 2. Configuración principal

- `Program.cs` registra el `AppDbContext` con la cadena de conexión `DefaultConnection`.
- Se habilita OpenAPI/Swagger solo en `Development`.
- La aplicación usa `UseHttpsRedirection()` y `UseAuthorization()`.
- Los controladores se mapean con `app.MapControllers()`.

## 3. Controladores disponibles actualmente

Las rutas implementadas en el backend son:

- `AuthController`
  - `POST /api/auth/login`
  - `POST /api/auth/register`

- `UsersController`
  - `GET /api/users`
  - `GET /api/users/{id}`
  - `POST /api/users`
  - `PUT /api/users/{id}`
  - `DELETE /api/users/{id}`
  - `GET /api/users/{userId}/addresses`

- `RestaurantsController`
  - `GET /api/restaurants`
  - `GET /api/restaurants/{id}`
  - `POST /api/restaurants`
  - `PUT /api/restaurants/{id}`
  - `DELETE /api/restaurants/{id}`
  - `GET /api/restaurants/{id}/menu`
  - `GET /api/restaurants/user/{userId}`

- `DishesController`
  - `GET /api/dishes`
  - `GET /api/dishes/{id}`
  - `POST /api/dishes`
  - `PUT /api/dishes/{id}`
  - `DELETE /api/dishes/{id}`

- `CouponsController`
  - `GET /api/coupons`
  - `GET /api/coupons/{id}`
  - `POST /api/coupons`
  - `PUT /api/coupons/{id}`
  - `DELETE /api/coupons/{id}`
  - `GET /api/coupons/user/{userId}`
  - `POST /api/coupons/{couponId}/apartar/{userId}`

- `GeneralDataController`
  - `GET /api/generaldata/categories`
  - `GET /api/generaldata/payment-methods`

## 4. Modelos y tablas ya existentes

Los modelos presentes en `Models` y sus tablas correspondientes en la base de datos son:

- `User`
- `Role`
- `Restaurant`
- `Dish`
- `Coupon`
- `Category`
- `PaymentMethod`
- `Order`
- `OrderItem`
- `Address`

Además en `AppDbContext` existen los `DbSet`:

- `Categories`
- `PaymentMethods`
- `Roles`
- `Addresses`
- `Users`
- `Restaurants`
- `Dishes`
- `Orders`
- `Coupons`
- `OrderItems`

## 5. Qué funciona hoy

El backend ya tiene implementado:

- Registro y login de usuarios.
- CRUD completo de usuarios.
- CRUD completo de restaurantes.
- CRUD completo de platos.
- CRUD básico de cupones.
- Lectura de categorías.
- Lectura de métodos de pago.
- Lectura de direcciones por usuario.
- Consulta de menú por restaurante.
- Consulta de restaurantes por dueño.
- Apartar cupones y reducir stock.

## 6. Qué falta implementar

### 6.1. Pedidos / checkout / historial

A pesar de que existen modelos `Order` y `OrderItem`, no hay ningún `OrdersController` ni endpoints expuestos para:

- crear pedidos desde el carrito,
- obtener el historial de pedidos de un usuario,
- obtener órdenes entrantes por restaurante,
- consultar un pedido individual,
- actualizar el estado de un pedido.

### 6.2. Gestión de cupones reservados en tabla separada

Actualmente el backend usa el campo `UserId` de `Coupon` para marcar cuándo un cupón está reservado.

Sin embargo, la solicitud actual pide una tabla aparte para guardar los cupones apartados por cada usuario.

Eso significa:

- mantener `Coupons` como catálogo de cupones disponibles,
- crear una tabla adicional como `ReservedCoupons` o `UserCoupons` para almacenar la relación de cupón con el usuario,
- exponer endpoints claros de cupones disponibles y cupones reservados.

### 6.3. Endpoints adicionales recomendados

Los endpoints que aún faltan para completar el flujo actual son:

- `POST /api/orders`
- `GET /api/orders`
- `GET /api/orders/{id}`
- `GET /api/orders/user/{userId}`
- `GET /api/orders/restaurant/{restaurantId}`
- `PUT /api/orders/{id}`
- `GET /api/coupons/available`
- `GET /api/coupons/reserved/{userId}`

### 6.4. Ajustes de modelo

Para facilitar los filtros de órdenes entrantes, es recomendable que `Order` incluya una relación explícita con `Restaurant` mediante `RestaurantId`.

Para la separación de cupones reservados, es recomendable crear un nuevo modelo y tabla específica como `ReservedCoupon` (o `UserCoupon`).

## 7. Recomendaciones de arquitectura

1. Añadir un `OrdersController` separado que use los modelos `Order` y `OrderItem`.
2. Mantener el modelo `Coupon` para cupones generales.
3. Crear un nuevo modelo/table `ReservedCoupon` para la reserva de cupones por usuario.
4. Mantener `GET /api/users/{userId}/addresses` como único endpoint de direcciones si ese es el requisito.
5. No agregar endpoints de rating ni estadísticas de dashboard, ya que no son necesarios.

## 8. Observaciones finales

El backend ya tiene la base de datos y los modelos necesarios para soportar el flujo principal, pero requiere controladores adicionales y un ajuste de diseño para separar reservas de cupones correctamente.
