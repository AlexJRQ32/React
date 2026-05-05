import './Header.css'
import logo from '../../assets/logo-final.png'
import { NavLink } from 'react-router-dom'

export function Header() {
  const isRestaurant = true

  const userData = {
    name: 'John Doe',
    rol: 'Customer'
  }

  return (
    <header className="header">
      <NavLink className="logo" to="/home">
        <img src={logo} alt="logo de la app" />
      </NavLink>
      <nav className="navbar">
        <ul>
          <li>
            <NavLink to="/home">
              <i className="fa-solid fa-house"></i>Home
            </NavLink>
          </li>
          <li>
            <NavLink to="/search">
              <i className="fa-solid fa-search"></i>Search
            </NavLink>
          </li>
          <li>
            <NavLink to="">
              <i className="fa-solid fa-map-marker-alt"></i>Ubication
            </NavLink>
          </li>
          <li>
            <NavLink to="/cart">
              <i className="fa-solid fa-shopping-cart"></i>Cart
            </NavLink>
          </li>
          <div className="dropdown">
            <li>
              <div className='more'>
                <i className="fa-solid fa-ellipsis-vertical"></i>More
              </div>
            </li>
            <div className="dropdown-content">
              {isRestaurant && (
                <li>
                  <NavLink to='incoming-orders'>
                    <i className="fa-solid fa-bag-shopping"></i>Incoming Orders
                  </NavLink>
                </li>
              )}
              <li>
                <NavLink to="/order-history">
                  <i className="fa-solid fa-clock-rotate-left"></i>Order History
                </NavLink>
              </li>
              <li>
                <NavLink to="/coupons">
                  <i className="fa-solid fa-ticket"></i>Coupons
                </NavLink>
              </li>
              {isRestaurant && <div className="spacer"></div>}
              {isRestaurant && (
                <li>
                  <NavLink to="/dashboard">
                    <i className="fa-solid fa-gauge-high"></i>Admin Dashboard
                  </NavLink>
                </li>
              )}
            </div>
          </div>
          <div className='sesion'>
            <li className='profile-link'>
              <NavLink to="/profile" >
                <i className="fa-solid fa-user-circle"></i>
                <div className='user-info'>
                  <span>{userData.name}</span>
                  <span className="user-role">{userData.rol}</span>
                </div>
              </NavLink>
            </li>
            <div className="dropdown-content">
              <li>
                <NavLink to="/settings">
                  <i className="fa-solid fa-cog"></i>Settings
                </NavLink>
              </li>
              <div className="spacer"></div>
              <li>
                <NavLink to="/logout" className="logout-link">
                  <i className="fa-solid fa-sign-out-alt"></i>Logout
                </NavLink>
              </li>
            </div>
          </div>
        </ul>
      </nav>
    </header>
  )
}
