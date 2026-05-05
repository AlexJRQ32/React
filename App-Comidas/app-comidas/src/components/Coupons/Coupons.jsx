import { ButtonAction } from '../Button/Button'
import './Coupons.css'

export function CouponCard({ name, discount }) {
  return (
    <div className="coupon-card">
      <span>
        <i className="fa-solid fa-tag"></i>
        {name}
      </span>
      {/\d/.test(discount) ? <p>{discount}% OFF</p> : <p>{discount}</p>}
    </div>
  )
}

export function CouponTicket({
  discount,
  short_description,
  description,
  quantity,
  classN
}) {
  const classNameAcepted = "coupon-buttons"
  return (
    <div className="coupon-ticket">
      <div class="pin-seguro"></div>
      <div className="coupon-header">
        <p>RappiDoz Ticket</p>
        {/\d/.test(discount) ? <h1>{discount}%</h1> : <h1>{discount}</h1>}
        <span>OFF</span>
      </div>
      <div className="coupon-body">
        <span>
          <strong>{short_description}</strong>
          <p>{description}</p>
        </span>
        {classN === classNameAcepted ?
          <div className={classN}>
            <ButtonAction icon={'pencil'} classN={'edit'} />
            <ButtonAction icon={'trash'} classN={'delete'} />
          </div>
          :
          <div className={classN}></div>
        }

        <p>{quantity} remaining</p>
      </div>
    </div>
  )
}
