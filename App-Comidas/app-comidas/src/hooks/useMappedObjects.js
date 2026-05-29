import Users from '../mocks/users.json'
import Categories from '../mocks/categories.json'
import Dishes from '../mocks/dishes.json'
import Coupons from '../mocks/coupons.json'
import Orders from '../mocks/orders.json'
import Payments from '../mocks/payment-methods.json'
import Restaurants from '../mocks/restaurants.json'
import StatCards from '../mocks/statcards.json'
import Ubications from '../mocks/ubications.json'
import Roles from '../mocks/roles.json'

export function useMappedObjects() {
  const mappedUsers = Users.map((user) => ({
    id: user.id,
    email: user.email,
    name: user.name,
    role: user.role,
    img: user.img,
    password: user.password,
    phone: user.phone
  }))

  const mappedCategories = Categories.map((element) => ({
    id: element.id,
    name: element.name,
    icon: element.icon,
    slug: element.slug,
  }))

  const mappedDishes = Dishes.map((dishe) => ({
    id: dishe.id,
    img: dishe.img,
    category: dishe.category,
    description: dishe.description,
    price: dishe.price,
    name: dishe.name,
  }))

  const mappedCoupons = Coupons.map((coupon) => ({
    id: coupon.Id,
    code: coupon.Code,
    title: coupon.Title,
    description: coupon.Description,
    discount: coupon.Discount,
    isPercentage: coupon.IsPercentage,
    expirationDate: coupon.ExpirationDate,
    active: coupon.Active,
    stock: coupon.Stock,
    categoryId: coupon.CategoryId,
  }))

  const mappedOrders = Orders.map((order) => ({
    id: order.id,
    restaurant: order.restaurant,
    status: order.status,
    date: order.date,
    time: order.time,
    customer: order.customer,
    paymentMethod: order.paymentMethod,
    items: order.items,
    total: order.total,
  }))

  const mappedPaymentMethods = Payments.map((method) => ({
    id: method.id,
    icon: method.icono,
    name: method.name,
    type: method.tipo,
    description: method.descripcion,
  }))

  const mappedRestaurants = Restaurants.map((restaurant) => ({
    id: restaurant.Id,
    tradeName: restaurant.TradeName,
    address: restaurant.Address,
    categoryId: restaurant.CategoryId,
    openingTime: restaurant.OpeningTime,
    closingTime: restaurant.ClosingTime,
    img: restaurant.Img,
    rating: restaurant.Rating,
    isOpen: restaurant.IsOpen,
    deliveryFee: restaurant.DeliveryFee,
    deliveryTime: restaurant.DeliveryTime,
  }))

  const mappedStatCards = StatCards.map((statCard) => ({
    id: statCard.id,
    icon: statCard.icon,
    site: statCard.site,
    title: statCard.title,
    value: statCard.value,
  }))

  const mappedUbications = Ubications.map((ubication) => ({
    id: ubication.id,
    name: ubication.name,
  }))

  const mappedRoles = Roles.map((role) => ({
    id: role.id,
    name: role.name,
    subtitle: role.subtitle,
    site: role.site,
    icon: role.icon
  }))

  return {
    users: mappedUsers,
    categories: mappedCategories,
    dishes: mappedDishes,
    coupons: mappedCoupons,
    orders: mappedOrders,
    paymentMethods: mappedPaymentMethods,
    restaurants: mappedRestaurants,
    statCards: mappedStatCards,
    ubications: mappedUbications,
    roles: mappedRoles,
  }
}
