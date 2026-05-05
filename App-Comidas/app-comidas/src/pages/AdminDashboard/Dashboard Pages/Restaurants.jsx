import { ButtonAction } from "../../../components/Button/Button"
import { CrudCard } from "../../../components/Cards/Cards"
import Restaurants from '../../../mocks/restaurants.json'

export function RestaurantsDashboard(){
  return(
    <div className="content-section">
          <div className="content-header">
            <h1>Our Locations</h1>
            <ButtonAction icon={'plus'} classN={'add'} />
          </div>
          <div className="content-main">
            {Restaurants.map((restaurant) => (
              <CrudCard
                img={restaurant.img}
                attribute={restaurant.location}
                icon={'location-dot'}
                name={restaurant.name}
              />
            ))}
          </div>
        </div>
  )
}