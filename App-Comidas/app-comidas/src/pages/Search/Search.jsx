import './Search.css'
import { SearchBar } from '../../components/SearchBar/SearchBar'
import { LargeCard } from '../../components/Cards/Cards'
import Restaurants from '../../mocks/restaurants.json'

export function Search() {
  const restaurantsLength = Restaurants.length
  const restaurants = Restaurants

  return (
    <div className="page" style={{ backgroundColor: '#1e1c22' }}>
      <section className="search-section">
        <div className="header-search">
          <h1>Your next craving</h1>
          <p>Search by name, food, area or category</p>
          <SearchBar
            icon={'search'}
            style={{
              border: '2px solid #d3ab80',
              borderRadius: '15px'
            }}
          />
        </div>
      </section>
      <section className="show-section">
        <div className="header-show">
          <strong>Restaurants</strong>
          <span>{restaurantsLength} locals</span>
        </div>
        <div className="show-restaurants">
          {restaurants.map((restaurant) => (
            <LargeCard
              name={restaurant.name}
              img={restaurant.img}
              schedule={restaurant.schedule}
              location={restaurant.location}
              isOpen={restaurant.isOpen}
              deliveryFee={restaurant.deliveryFee}
              deliveryTime={restaurant.deliveryTime}
            />
          ))}
        </div>
      </section>
    </div>
  )
}
