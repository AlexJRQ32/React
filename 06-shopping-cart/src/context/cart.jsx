import { createContext, useState } from 'react'

export const CartContext = createContext()

export function CartProvider({ children }) {
  const [cart, setCart] = useState([])

  const addToCart = (product) => {
    const productInCart = cart.findIndex((item) => item.id === product.id)
    if (productInCart >= 0) {
      const newCart = structuredClone(cart)
      newCart[productInCart].quantity += 1
      return setCart(newCart)
    }

    setCart((prevState) => [...prevState, { ...product, quantity: 1 }])
  }

  const clearCart = () => {
    setCart([])
  }

  const removeToCart = (product) => {
    setCart((prevState) => prevState.filter((item) => item.id !== product.id))
  }

  const subtractItem = (product) => {
    const isItems = cart.findIndex((item) => item.id === product.id)
    const newCart = structuredClone(cart)
    newCart[isItems].quantity -= 1
    return setCart(newCart)
  }

  const plusItem = (product) => {
    const isItems = cart.findIndex((item) => item.id === product.id)
    const newCart = structuredClone(cart)
    newCart[isItems].quantity += 1
    return setCart(newCart)
  }

  return (
    <CartContext.Provider
      value={{ cart, addToCart, clearCart, removeToCart, subtractItem, plusItem }}
    >
      {children}
    </CartContext.Provider>
  )
}
