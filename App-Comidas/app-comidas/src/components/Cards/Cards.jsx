import './Cards.css'
import { ButtonAction, ButtonRedirect } from '../Button/Button'
import { NavLink } from 'react-router-dom'

export function SimpleCard({ icon, name }) {
  return (
    <NavLink to="/search" className="simple-card">
      <i className={`fa-solid fa-${icon}`}></i>
      <h3>{name}</h3>
    </NavLink>
  )
}

export function ElaborateCard({ schedule, name, img, rating }) {
  return (
    <NavLink to="/search" className="elaborate-card">
      <img src={img} alt={name} />
      <h3>{name}</h3>
      <div className="other-info">
        <span>
          <i className="fa-solid fa-clock" />
          {schedule}
        </span>
        <span>
          <i className="fa-solid fa-star" />
          {rating}
        </span>
      </div>
    </NavLink>
  )
}

export function LargeCard({
  name,
  img,
  schedule,
  location,
  isOpen,
  deliveryFee,
  deliveryTime,
}) {
  return (
    <div className="large-card">
      <img src={img} alt={name} />
      <div className="card-info">
        <div className="content">
          <div id="top">
            <h3>{name}</h3>
            <span>
              <i className="fa-solid fa-location-dot" />
              {location}
            </span>
          </div>
          {isOpen ? (
            <span className="status is-open">OPEN</span>
          ) : (
            <span className="status is-closed">CLOSED</span>
          )}
        </div>
        <div className="content">
          <div id="bot">
            <span>
              <i className="fa-regular fa-clock"></i>
              {schedule}
            </span>
            <p>
              {deliveryFee} delivery • {deliveryTime}
            </p>
          </div>
          <ButtonRedirect title={'View menu'} />
        </div>
      </div>
    </div>
  )
}

export function OrderCard({
  id,
  date,
  customer,
  paymentMethod,
  status,
  total,
  items,
  time,
}) {
  const statusClass = ({ status }) => {
    if (status === 'DELIVERED') {
      return 'completed'
    } else if (status === 'PENDING') {
      return 'pending'
    } else {
      return 'cancelled'
    }
  }

  return (
    <div className="order-card">
      <div className="card-header">
        <div className="left-side">
          <h2>Order #{id}</h2>
          <p>
            <i className="fa-solid fa-calendar-days"></i>
            {date}
            <span>
              <i className="fa-regular fa-clock"></i>
              {time}
            </span>
          </p>
          <p>
            <i className="fa-solid fa-user"></i>
            {customer}
          </p>
          <p>
            <i className="fa-solid fa-credit-card"></i>
            {paymentMethod}
          </p>
        </div>
        <div className="right-side">
          <span className={statusClass({ status })}>{status}</span>
          <h3>${total}</h3>
        </div>
      </div>
      <div className="card-items">
        <i className="fa-solid fa-circle-dot"></i>
        {items.map((item) => (
          <span className="order-item">
            {item.quantity}x {item.name} (${item.price})
          </span>
        ))}
      </div>
    </div>
  )
}

export function OrderSimpleCard({ id, date, time, status, total }) {
  const statusClass = ({ status }) => {
    if (status === 'DELIVERED') {
      return 'completed'
    } else if (status === 'PENDING') {
      return 'pending'
    } else {
      return 'cancelled'
    }
  }

  return (
    <div className="order-card">
      <div className="card-header">
        <div className="left-side">
          <h2>Order #{id}</h2>
          <p>
            <i className="fa-solid fa-calendar-days"></i> {date}
            <i className="fa-regular fa-clock"></i> {time}
          </p>
        </div>
        <div className="right-side">
          <span className={statusClass({ status })}>{status}</span>
          <h3>${total}</h3>
          <ButtonRedirect
            icon={'star'}
            style={{ borderRadius: '12px' }}
            title={'Rate'}
          />
        </div>
      </div>
    </div>
  )
}

export function StatCard({ icon, title, value, site }) {
  return(
    <NavLink to={site} className={"stat-card"}>
      <div>
        <span>
          <i className={`fa-solid fa-${icon}`}></i>
        </span>
        <p>{title} {value}</p>
      </div>
    </NavLink>
    
  )
}

export function CrudCard({ img, name, attribute, classN, icon }) {
  return(
    <div className={`crud-card ${classN}`}>
      <img src={img} alt={name} />
      <span>
        <p>{name}</p>
        <strong><i className={`fa-solid fa-${icon}`}></i> {attribute}</strong>
      </span>
      <div>
        <ButtonAction icon={'pencil'} classN={'edit'} />
        <ButtonAction icon={'trash'} classN={'delete'}/>
      </div>
    </div>
  )
}