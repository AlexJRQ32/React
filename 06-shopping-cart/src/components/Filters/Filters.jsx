import './Filters.css'
import { useId } from 'react'
import { useFilters } from '../../hooks/useFilters'

export function Filters() {
  const { filters, setFilters } = useFilters()

  const minPriceFilterId = useId()
  const categoryFilterId = useId()

  const handleChangeMinPrice = (event) => {
    setFilters((prevState) => ({ ...prevState, minPrice: event.target.value }))
  }

  const handleChangeCategory = (event) => {
    setFilters((prevState) => ({ ...prevState, category: event.target.value }))
  }

  return (
    <section className="filters">
      <div className="filter">
        <label htmlFor="price">Precio a partir de: </label>
        <input
          type="range"
          id={minPriceFilterId}
          min="0"
          max="1000"
          onChange={handleChangeMinPrice}
          value={filters.minPrice}
        />
        <span>${filters.minPrice}</span>
      </div>

      <div className="filter">
        <label htmlFor={categoryFilterId}>Categoria: </label>
        <select id={categoryFilterId} onChange={handleChangeCategory}>
          <option value="all">Todos</option>
          <option value="fragrances">Fragancias</option>
          <option value="beauty">Belleza</option>
          <option value="groceries">Comestibles</option>
          <option value="furniture">Mobiliario</option>
        </select>
      </div>
    </section>
  )
}
