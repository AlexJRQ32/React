import { useState, useRef, useMemo, useCallback } from 'react'
import { searchMovies } from '../services/movies'

export function useMovies({ search, sort }) {
  const [movies, setMovies] = useState([])
  const [error, setError] = useState(null)
  const [loading, setLoading] = useState(false)
  const previusSearch = useRef(search)

  const getMovies = useCallback( async ({ search }) => {
    if (previusSearch.current === search) return
    // si el valor de search es el mismo que el valor anterior, no hacemos nada

    try{
      setLoading(true)
      setError(null)
      previusSearch.current = search
      const newMovies = await searchMovies({ search })
      setMovies(newMovies)

    } catch (e) {
      setError(e.message)

    } finally {
      // se ejecuta siempre, tanto si hay error como si no lo hay
      setLoading(false)
    }
  }, [])

  const sortedMovies = useMemo(() => {
    return sort
      ? [...movies].sort((a, b) => a.title.localeCompare(b.title))
      : movies
  }, [sort, movies])

  return { movies: sortedMovies, getMovies, loading }
}