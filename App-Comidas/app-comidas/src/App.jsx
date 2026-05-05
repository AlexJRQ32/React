import './App.css'
import { Routes, Route, Navigate } from 'react-router-dom'
import { Home } from './pages/Home/Home'
import { Search } from './pages/Search/Search'
import { Cart } from './pages/Cart/Cart'
import { IncomingOrders } from './pages/Orders/IncomingOrders'
import { OrderHistory } from './pages/Orders/OrderHistory'
import { Coupons } from './pages/Coupons/Coupons'
import { AdminDashboard } from './pages/AdminDashboard/AdminDashboard'
import { UserLayout } from './UserLayout'
import { OverviewDashboard } from './pages/AdminDashboard/Dashboard Pages/Overview'
import { MyMenuDashboard } from './pages/AdminDashboard/Dashboard Pages/MyMenu'
import { RestaurantsDashboard } from './pages/AdminDashboard/Dashboard Pages/Restaurants'
import { UsersDashboard } from './pages/AdminDashboard/Dashboard Pages/Users'
import { CouponsDashboard } from './pages/AdminDashboard/Dashboard Pages/CouponsDashboard'

function App() {
  return (
    <div className="app">
      <Routes>
        <Route path="/" element={<Navigate to="/home" replace />} />
        <Route element={<UserLayout />}>
          <Route path="/home" element={<Home />} />
          <Route path="/search" element={<Search />} />
          <Route path="/cart" element={<Cart />} />
          <Route path="/incoming-orders" element={<IncomingOrders />} />
          <Route path="/order-history" element={<OrderHistory />} />
          <Route path="/coupons" element={<Coupons />} />
        </Route>

        <Route path="/dashboard" element={<AdminDashboard />}>
          <Route index element={<Navigate to="overview" replace />} />
          <Route path="overview" element={<OverviewDashboard />} />
          <Route path="my-menu" element={<MyMenuDashboard />} />
          <Route path="restaurants" element={<RestaurantsDashboard />} />
          <Route path="users" element={<UsersDashboard />} />
          <Route path="coupons-dashboard" element={<CouponsDashboard />} />
        </Route>
      </Routes>
    </div>
  )
}

export default App
