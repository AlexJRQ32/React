import { products as initialProducts } from './mocks/products'
import { Products } from './components/Products/Products'
import { Header } from './components/Header/Header'
import { Footer } from './components/Footer/Footer'
import { useFilters } from './hooks/useFilters'
import { Cart } from './components/Cart/Cart'
import { CartProvider } from './context/cart'

function App() {
  const { filterProducts } = useFilters()

  const filteredProducts = filterProducts(initialProducts)

  return (
    <CartProvider>
      <Header />
      <Cart />
      <Products products={filteredProducts} />
      <Footer />
    </CartProvider>
  )
}

export default App
