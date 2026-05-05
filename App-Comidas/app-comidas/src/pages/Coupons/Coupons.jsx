import { CouponTicket } from '../../components/Coupons/Coupons'
import Coupon from '../../mocks/coupons.json'
import './Coupons.css'

export function Coupons() {
  const coupons = Coupon

  return (
    <div className="page">
      <div className="coupons-section">
        <div className="coupons-header">
          <h1>Benefits Summary</h1>
        </div>
        <div className="grid-body">
          {coupons.map((coupon) => (
            <CouponTicket
              discount={coupon.discount}
              short_description={coupon.short_description}
              description={coupon.description}
              quantity={coupon.quantity}
            />
          ))}
        </div>
      </div>
    </div>
  )
}
