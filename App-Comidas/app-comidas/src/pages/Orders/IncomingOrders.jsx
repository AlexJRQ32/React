import './IncomingOrders.css'
import Restaurants from '../../mocks/restaurants.json'
import OrdersData from '../../mocks/orders.json'
import { OrderCard } from '../../components/Cards/Cards'

export function IncomingOrders() {
  const restaurantNames = Restaurants.map((restaurant) => restaurant.name)
  const selectedRestaurant = restaurantNames.find(
    (name) => name === 'Taco Bell'
  )
  const orders = OrdersData
  const haveOrders = true

  return (
    <div className="page">
      <div className="order-section">
        <div className="order-header">
          <span>
            <i className="fa-solid fa-shopping-bag"></i>
            <h1>Incoming Orders</h1>
          </span>
          <p>{selectedRestaurant || 'Restaurant not found'}</p>
        </div>
        {
          haveOrders ?
          <div className="order-body">
            {orders.map((order) => (
              <OrderCard
                key={order.id}
                id={order.id}
                date={order.date}
                customer={order.customer}
                paymentMethod={order.paymentMethod}
                status={order.status}
                total={order.total}
                items={order.items}
                time={order.time}
              />
            ))}
          </div>
        : <p className='empty'>Your quaue is empty</p>
        }
      </div>
    </div>
  )
}
