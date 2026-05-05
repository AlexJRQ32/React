import { ButtonAction } from '../../../components/Button/Button'
import { CouponTicket } from '../../../components/Coupons/Coupons'
import Coupons from '../../../mocks/coupons.json'

export function CouponsDashboard() {
  return (
    <div className="content-section">
      <div className="content-header">
        <h1>Coupons</h1>
        <ButtonAction icon={'plus'} classN={'add'} />
      </div>
      <div className="content-main" style={{flexDirection: 'row', flexWrap: 'wrap', justifyContent: 'center'}}>
        {Coupons.map((coupon) => (
          <CouponTicket
            classN={'coupon-buttons'}
            discount={coupon.discount}
            short_description={coupon.short_description}
            description={coupon.description}
            quantity={coupon.quantity}
          />
        ))}
      </div>
    </div>
  )
}
