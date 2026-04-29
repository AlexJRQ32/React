import './Cart.css'
import { ClearCartIcon, CartIcon } from '../Icons'
import { useCart } from '../../hooks/useCart'
import { useId } from 'react'

export function Cart() {
  const { cart, clearCart, subtractItem, plusItem } = useCart()
  const cartCheckboxID = useId()
  return (
    <>
      <label className="cart-button" htmlFor={cartCheckboxID}>
        <CartIcon />
      </label>
      <input type="checkbox" id={cartCheckboxID} hidden />

      <aside className="cart">
        <ul>
          {cart.map((product) => (
            <li key={product.id}>
              <img src={product.thumbnail} alt={product.title} />
              <div>
                <strong>{product.title}</strong> - ${product.price}
              </div>
              <footer>
                <button className='btn-action' onClick={() => subtractItem(product)}>-</button>
                <small>Qty: {product.quantity}</small>
                <button className='btn-action' onClick={() => plusItem(product)}>+</button>
              </footer>
            </li>
          ))}
        </ul>

        <button onClick={() => clearCart()}>
          <ClearCartIcon />
        </button>
      </aside>
    </>
  )
}
