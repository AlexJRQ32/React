# Contexto extendido de modelos y endpoints del backend

Este documento describe cada modelo del backend de `APIRest-App-Comidas` y explica el propósito de cada endpoint de los controladores existentes.

---

## Modelos y atributos

### User

- `Id` (int): identificador único del usuario.
- `Name` (string): nombre completo.
- `RoleId` (int): referencia al rol del usuario.
- `Role` (Role): relación con el rol.
- `Email` (string): correo electrónico para autenticación.
- `Img` (string): URL o ruta de la imagen de perfil.
- `Password` (string): contraseña del usuario.
- `Phone` (string): número de teléfono.
- `Addresses` (ICollection<Address>): direcciones asociadas al usuario.
- `Restaurants` (ICollection<Restaurant>): restaurantes que el usuario administra o posee.
- `Orders` (ICollection<Order>): pedidos realizados por el usuario.
- `Coupons` (ICollection<Coupon>): cupones asociados al usuario.

### Role

- `Id` (int): identificador del rol.
- `Name` (string): nombre del rol (por ejemplo, `Restaurante`, `Usuario`).
- `Subtitle` (string): descripción breve del rol.
- `Site` (string): ruta asociada al rol en el frontend.
- `Icon` (string): icono o clase de icono para la interfaz.
- `Users` (ICollection<User>): usuarios que tienen este rol.

### Restaurant

- `Id` (int): identificador del restaurante.
- `TradeName` (string): nombre comercial.
- `Address` (string): dirección física del restaurante.
- `CategoryId` (int): categoría del restaurante.
- `Category` (Category): relación con la categoría.
- `UserId` (int): propietario / usuario que administra el restaurante.
- `User` (User): relación con el usuario propietario.
- `OpeningTime` (string): hora de apertura.
- `ClosingTime` (string): hora de cierre.
- `Img` (string): imagen del restaurante.
- `Rating` (string): calificación del restaurante.
- `IsOpen` (bool): estado de apertura.
- `DeliveryFee` (string): costo de entrega.
- `DeliveryTime` (string): tiempo estimado de entrega.
- `Dishes` (ICollection<Dish>): platos disponibles en el restaurante.
- `Orders` (ICollection<Order>): pedidos asociados al restaurante.

### Dish

- `Id` (int): identificador del platillo.
- `Name` (string): nombre del platillo.
- `CategoryId` (int): categoría del platillo.
- `Category` (Category): relación con la categoría.
- `Price` (decimal): precio del platillo.
- `Img` (string): imagen del platillo.
- `Description` (string): descripción del platillo.
- `RestaurantId` (int): restaurante que ofrece el platillo.
- `Restaurant` (Restaurant): relación con el restaurante.

### Coupon

- `Id` (int): identificador del cupón.
- `Code` (string): código del cupón.
- `Title` (string): título o nombre corto.
- `Description` (string): descripción detallada.
- `Discount` (decimal): valor del descuento.
- `IsPercentage` (bool): indica si el descuento es porcentaje.
- `ExpirationDate` (string): fecha de expiración.
- `Active` (bool): estado activo/inactivo.
- `Stock` (int?): cantidad disponible.
- `CategoryId` (int?): categoría del cupón.
- `Category` (Category): relación con categoría.
- `OrderId` (int?): pedido asociado al cupón.
- `Order` (Order): relación con el pedido.
- `UserId` (int?): usuario que apartó el cupón.
- `User` (User): relación con el usuario.

### Category

- `Id` (int): identificador de categoría.
- `Name` (string): nombre de la categoría.
- `Icon` (string): icono representativo.
- `Slug` (string): texto amigable para URLs o filtros.
- `Restaurants` (ICollection<Restaurant>): restaurantes que pertenecen a esta categoría.
- `Dishes` (ICollection<Dish>): platos que pertenecen a esta categoría.
- `Coupons` (ICollection<Coupon>): cupones que pertenecen a esta categoría.

### PaymentMethod

- `Id` (string): identificador del método de pago.
- `Name` (string): nombre de la forma de pago.
- `Tipo` (string): tipo de pago.
- `Descripcion` (string): descripción del método.
- `Icono` (string): icono representativo.
- `Orders` (ICollection<Order>): pedidos que usaron este método.

### Order

- `Id` (int): identificador del pedido.
- `Restaurant` (string): nombre o referencia del restaurante entendido en el pedido.
- `Status` (string): estado actual del pedido (`PENDING`, `DELIVERED`, `CANCELLED`, etc.).
- `Date` (string): fecha del pedido.
- `Time` (string): hora del pedido.
- `CategoryId` (int): categoría asociada al pedido.
- `Category` (Category): relación con categoría.
- `CustomerId` (int): usuario que hizo el pedido.
- `Customer` (User): relación con el cliente.
- `PaymentMethodId` (string): método de pago elegido.
- `PaymentMethod` (PaymentMethod): relación con el método de pago.
- `AddressId` (string): dirección de entrega.
- `Address` (Address): relación con la dirección.
- `Total` (int): total del pedido.
- `Items` (List<OrderItem>): lista de líneas de pedido.

### OrderItem

- `Id` (int): identificador de la línea de pedido.
- `OrderId` (int): pedido al que pertenece la línea.
- `Order` (Order): relación con el pedido.
- `DishId` (int): platillo solicitado.
- `Dish` (Dish): relación con el platillo.
- `Quantity` (int): cantidad de ese platillo.
- `Name` (string): nombre del platillo en el pedido.
- `Price` (int): precio unitario del platillo en el pedido.

### Address

- `Id` (string): identificador de la dirección.
- `Name` (string): nombre de la ubicación/dirección.
- `UserId` (int): usuario propietario de la dirección.
- `User` (User): relación con el usuario.

---

## Controladores y propósito de cada endpoint

### AuthController

- `POST /api/auth/login`
  - Propósito: validar credenciales y devolver datos del usuario autenticado.
  - Funciona como punto de entrada para que el usuario inicie sesión.

- `POST /api/auth/register`
  - Propósito: registrar un nuevo usuario o restaurante.
  - Crea un `User` y, si el rol es restaurante, crea también un `Restaurant` asociado.

### UsersController

- `GET /api/users`
  - Devuelve todos los usuarios registrados.
  - Útil para administración y dashboards de usuarios.

- `GET /api/users/{id}`
  - Devuelve los datos de un solo usuario por su ID.
  - Usado para mostrar perfiles o editar un usuario específico.

- `POST /api/users`
  - Crea un usuario nuevo en la base de datos.
  - Permite registrar usuarios desde el backend o importar datos.

- `PUT /api/users/{id}`
  - Actualiza los datos de un usuario existente.
  - Sirve para editar perfil, contraseña, rol, imagen, etc.

- `DELETE /api/users/{id}`
  - Elimina un usuario.
  - Útil para administración o limpieza de cuentas.

- `GET /api/users/{userId}/addresses`
  - Devuelve las direcciones asociadas a un usuario.
  - Sirve a la funcionalidad de selección de entrega en el frontend.

### RestaurantsController

- `GET /api/restaurants`
  - Devuelve todos los restaurantes.
  - Usado para listar opciones en el home y búsqueda.

- `GET /api/restaurants/{id}`
  - Devuelve los datos de un restaurante específico.
  - Útil para páginas de detalle de restaurante.

- `POST /api/restaurants`
  - Crea un restaurante nuevo.
  - Requerido para el registro de negocios o administración.

- `PUT /api/restaurants/{id}`
  - Actualiza un restaurante.
  - Permite cambiar dirección, horario, estado, costo de entrega, etc.

- `DELETE /api/restaurants/{id}`
  - Elimina un restaurante.
  - Usado para administración de locales.

- `GET /api/restaurants/{id}/menu`
  - Devuelve los platillos del restaurante.
  - Soporta la vista de menú por restaurante.

- `GET /api/restaurants/user/{userId}`
  - Devuelve restaurantes asociados al usuario dueño.
  - Sirve para mostrar el dashboard del negocio.

### DishesController

- `GET /api/dishes`
  - Devuelve todos los platillos.
  - Útil para listados generales o administración.

- `GET /api/dishes/{id}`
  - Devuelve un platillo específico.
  - Usado para ver detalles o editar.

- `POST /api/dishes`
  - Crea un platillo nuevo.
  - Usado en el panel de menú del restaurante.

- `PUT /api/dishes/{id}`
  - Actualiza un platillo.
  - Permite cambiar nombre, precio, categoría, imagen y descripción.

- `DELETE /api/dishes/{id}`
  - Elimina un platillo.
  - Usado para gestión del menú.

### CouponsController

- `GET /api/coupons`
  - Devuelve todos los cupones.
  - Usado para administración y catálogos generales.

- `GET /api/coupons/{id}`
  - Devuelve un cupón específico.
  - Útil para ver detalles del cupón.

- `POST /api/coupons`
  - Crea un cupón nuevo.
  - Requerido para la creación y administración de promociones.

- `PUT /api/coupons/{id}`
  - Actualiza un cupón.
  - Permite modificar código, descuento, stock, estado, etc.

- `DELETE /api/coupons/{id}`
  - Elimina un cupón.
  - Usado para retirar promociones.

- `GET /api/coupons/user/{userId}`
  - Devuelve cupones relacionados a un usuario.
  - Actualmente sirve para mostrar cupones reservados o usados por el usuario.

- `POST /api/coupons/{couponId}/apartar/{userId}`
  - Aparta un cupón para un usuario.
  - Reduce el stock y marca el cupón como reservado por ese usuario.

### GeneralDataController

- `GET /api/generaldata/categories`
  - Devuelve todas las categorías.
  - Sirve para alimentar filtros y selección de categorías en el frontend.

- `GET /api/generaldata/payment-methods`
  - Devuelve los métodos de pago.
  - Sirve para poblar opciones de pago en el checkout.

---

## Notas de contexto

- El backend ya tiene el modelo `Order` y `OrderItem`, pero no hay controladores de pedidos expuestos.
- Las direcciones solo se consultan por usuario; no existe CRUD completo de direcciones.
- Los modelos `Category` y `PaymentMethod` están pensados para datos de catálogo.
- El modelo `Coupon` actualmente mezcla cupones disponibles y cupones apartados con `UserId`.
- Para separar cupones apartados correctamente, se recomienda crear una tabla aparte de reservas de cupones.

---

## Recomendación rápida

- Mantener los controladores existentes tal como están.
- Añadir un `OrdersController` para pedidos y flujo de checkout.
- Crear un nuevo modelo/tablas de reserva de cupones para almacenar `ReservedCoupon` o `UserCoupon`.
- No es necesario agregar endpoints de rating ni estadísticas.
