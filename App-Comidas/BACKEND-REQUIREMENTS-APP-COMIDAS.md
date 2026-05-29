# Requisitos Backend para App-Comidas

## Resumen

Este documento describe los endpoints, tablas y modelos necesarios para respaldar el frontend actual de `app-comidas` con los cambios solicitados:

- Eliminamos rating.
- Eliminamos estadísticas del dashboard.
- Dejamos el endpoint de direcciones que ya existe.
- Dejamos el endpoint de categorías que ya existe.
- Añadimos separación entre cupones reservados y cupones normales.
- Añadimos soporte de pedidos/checkout/historial de órdenes.

---

## Endpoints actuales que se mantienen

- `POST /api/auth/login`
- `POST /api/auth/register`
- `GET /api/users`
- `GET /api/users/{id}`
- `POST /api/users`
- `PUT /api/users/{id}`
- `DELETE /api/users/{id}`
- `GET /api/users/{userId}/addresses`
- `GET /api/restaurants`
- `GET /api/restaurants/{id}`
- `POST /api/restaurants`
- `PUT /api/restaurants/{id}`
- `DELETE /api/restaurants/{id}`
- `GET /api/restaurants/{id}/menu`
- `GET /api/restaurants/user/{userId}`
- `GET /api/dishes`
- `GET /api/dishes/{id}`
- `POST /api/dishes`
- `PUT /api/dishes/{id}`
- `DELETE /api/dishes/{id}`
- `GET /api/coupons`
- `GET /api/coupons/{id}`
- `POST /api/coupons`
- `PUT /api/coupons/{id}`
- `DELETE /api/coupons/{id}`
- `GET /api/coupons/user/{userId}`
- `POST /api/coupons/{couponId}/apartar/{userId}`
- `GET /api/generaldata/categories`
- `GET /api/generaldata/payment-methods`

> Nota: el endpoint de categorías ya existe y no necesita cambios.
> Nota: el endpoint de direcciones que ya existe es `GET /api/users/{userId}/addresses`.

---

## Endpoints que faltan o deben implementarse

### 1. Pedidos / checkout / historial

- `POST /api/orders`
  - Crear un pedido desde el carrito.
- `GET /api/orders`
  - Listar todos los pedidos (administración o supervisión general).
- `GET /api/orders/{id}`
  - Obtener detalle de un pedido.
- `GET /api/orders/user/{userId}`
  - Historial de pedidos de un cliente.
- `GET /api/orders/restaurant/{restaurantId}`
  - Órdenes entrantes de un restaurante específico.
- `PUT /api/orders/{id}`
  - Actualizar estado del pedido (`PENDING`, `DELIVERED`, `CANCELLED`, etc.).

### 2. Separación de cupones reservados y cupones normales

- `GET /api/coupons/available`
  - Cupones normales disponibles para mostrar en el wallet.
  - Debe filtrar: `Active == true`, `Stock > 0`, `UserId == null`.
- `GET /api/coupons/reserved/{userId}`
  - Cupones reservados por un usuario.
  - Debe filtrar: `UserId == userId`.

> `GET /api/coupons/user/{userId}` puede continuar existiendo como alternativa, pero conviene dejar una ruta explícita `/reserved/{userId}` para separar el concepto.
>
> Nota: para este caso se recomienda usar una tabla aparte que guarde los cupones apartados de cada usuario en lugar de depender solo de `UserId` dentro de la tabla `Coupons`.

---

## Tablas de base de datos necesarias

### Tablas ya necesarias y presentes en el backend

- `Users`
- `Roles`
- `Restaurants`
- `Dishes`
- `Categories`
- `PaymentMethods`
- `Addresses`
- `Coupons`
- `Orders`
- `OrderItems`

### Tablas que se usan para este flujo

- `Orders`
- `OrderItems`
- `Coupons`
- `Addresses`
- `PaymentMethods`
- `Categories`

> Nota: no es necesario crear nuevas tablas adicionales para los cambios solicitados; los modelos y tablas ya existen en el backend.

---

## Modelos necesarios

### Modelos que ya existen y se deben usar/ajustar

- `Order`
- `OrderItem`
- `Coupon`
- `Address`
- `Category`
- `PaymentMethod`
- `User`
- `Restaurant`
- `Dish`

### Ajustes recomendados al modelo `Order`

El modelo actual de `Order` tiene estos campos relevantes:

- `Id`
- `Restaurant` (string)
- `Status` (string)
- `Date` (string)
- `Time` (string)
- `CategoryId` (int)
- `CustomerId` (int)
- `PaymentMethodId` (string)
- `AddressId` (string)
- `Total` (int)
- `Items` (`List<OrderItem>`)

Para apoyar mejor el filtro de órdenes entrantes por restaurante, se recomienda agregar:

- `RestaurantId` (int)
- `virtual Restaurant RestaurantRef` o `Restaurant` como relación directa con `Restaurant`

Esto facilita `GET /api/orders/restaurant/{restaurantId}` sin depender solo de un nombre de restaurante.

### Modelo `Coupon`

El modelo actual ya incluye un campo clave para separar cupones:

- `UserId` (int?)

Con esto, se puede distinguir:

- `UserId == null` → cupón normal disponible
- `UserId != null` → cupón reservado por un usuario

---

## Controladores recomendados a añadir

- `OrdersController`
  - CRUD de pedidos y filtros por cliente/restaurant.
- `CouponsController` (extendido)
  - Endpoint adicional para `/available`.
  - Endpoint adicional para `/reserved/{userId}`.

---

## Resumen rápido

### Ya cubierto

- Autenticación y registro.
- Usuarios CRUD.
- Restaurantes CRUD.
- Platillos CRUD.
- Cupones CRUD básico.
- Categorías.
- Direcciones de usuario.
- Métodos de pago.

### Faltante específico

- Persistencia de pedidos/checkout/historial.
- Separación explícita de cupones reservados vs cupones normales.

---

## Ruta del archivo

Este archivo está en la raíz del proyecto: `c:\Proyectos-React\BACKEND-REQUIREMENTS-APP-COMIDAS.md`
