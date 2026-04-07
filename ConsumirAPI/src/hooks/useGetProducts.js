import { useEffect, useState } from 'react'
import { GetProducts } from '../services/apiProducts'

export const useGetProducts = () => {
  const [products, setProducts] = useState([])

  const fetchProducts = () => {
    GetProducts().then((data) => setProducts(data))
  }

  useEffect(() => {
    fetchProducts()
  }, [])

  return { products, refetch: fetchProducts }
}
