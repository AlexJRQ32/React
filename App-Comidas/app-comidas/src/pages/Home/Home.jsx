import { SearchBar } from '../../components/SearchBar/SearchBar'
import { SimpleCard } from '../../components/Cards/Cards'
import { ElaborateCard } from '../../components/Cards/Cards'
import Categories from '../../mocks/categories.json'
import Restaurants from '../../mocks/restaurants.json'
import './Home.css'

export function Home() {
  const restaurants = Restaurants
  const categories = Categories

  return (
    <>
      <section className="hero-section">
        <div className="hero-body">
          <h1>Food delivery right to your doorstep</h1>
          <SearchBar icon={'search'} style={{borderRadius: '15px'}}/>
        </div>
      </section>
      <section className="section">
        <div className="body">
          <h2>Browse by category</h2>
          <div className="categories">
            {categories.map((category) => (
              <SimpleCard
                key={category.id}
                name={category.name}
                icon={category.icon}
              />
            ))}
          </div>
        </div>
      </section>
      <section className="section">
        <div className="body">
          <div className="header-restaurants">
            <h2>Populars restaurants</h2>
            <a href="/search">View all</a>
          </div>
          <div className="restaurants">
            {restaurants.map((restaurant) => (
              <ElaborateCard
                name={restaurant.name}
                img={restaurant.img}
                rating={restaurant.rating}
                schedule={restaurant.schedule}
              />
            ))}
          </div>
        </div>
      </section>
      <section className='section'>
        <div className='body'>
          <div className='pasos'>
            <div className="paso">
              <i className='fa-solid fa-location-dot'></i>
              <h3>Enter adress</h3>
              <p>Tell us where you are so we can show you restaurants with coverage in your area.</p>
            </div>
            <div className="paso">
              <i className='fa-solid fa-utensils'></i>
              <h3>Choose your food</h3>
              <p>Browse through the menus of your favorite businesses and put together your ideal order.</p>
            </div>
            <div className="paso">
              <i className='fa-solid fa-motorcycle'></i>
              <h3>Receive it fast</h3>
              <p>And that's it! Our couriers fly to deliver your hot cravings.</p>
            </div>
          </div>
        </div>
      </section>
    </>
  )
}
