import { ButtonAction } from '../../../components/Button/Button'
import { CrudCard } from '../../../components/Cards/Cards'
import Dishes from '../../../mocks/dishes.json'

export function MyMenuDashboard() {
  return (
    <div className="content-section">
      <div className="content-header">
        <h1>My Dishes</h1>
        <ButtonAction icon={'plus'} classN={'add'} />
      </div>
      <div className="content-main">
        {Dishes.map((dishe) => (
          <CrudCard
            img={dishe.img}
            attribute={dishe.price}
            name={dishe.value}
          />
        ))}
      </div>
    </div>
  )
}
