import './Footer.css'
import { IS_DEV } from '../../config'
// import { useFilters } from '../../hooks/useFilters'
import { useCart } from '../../hooks/useCart'

export function Footer() {
  const { cart } = useCart()
  // const { filters } = useFilters()
  return (
    <footer className="footer">
      <h4>Prueba Tecnica React - <span> @midudev</span></h4>
      <h5>Shopping Cart con useContext & useReducer</h5>
      {/* {IS_DEV && JSON.stringify(filters, null, 2)} */}
      {IS_DEV && JSON.stringify(cart, null, 2)}
    </footer>
  )
}
