import './App.css'
import { useState, useMemo } from 'react'
import { useSearch } from './hooks/useSearch'
import { Movies } from './components/Movies'
import { useMovies } from './hooks/useMovies'
import debounce from 'just-debounce-it'

function App() {
  const { search, updateSearch, error } = useSearch()
  const [sort, setSort] = useState(false)
  const { movies, getMovies, loading } = useMovies({ search, sort })

  const debouncedGetMovies = useMemo(
    () => debounce((search) => {
      console.log('search', search)
      getMovies({ search })
    }, 500),
    [getMovies]
  )

  const handleSubmit = (event) => {
    event.preventDefault()
    getMovies({ search })
  }

  const handleSort = () => {
    setSort(!sort)
  }

  const handleChange = (event) => {
    const newSearch = event.target.value
    updateSearch(newSearch)
    debouncedGetMovies(newSearch)
  }

  return (
    <div className='page'>
      <header>
        <h1>Buscador de Peliculas</h1>
        <form className='form' onSubmit={handleSubmit}>
          <input
            style={{
              border: '1px solid transparent', 
              borderColor: error ? 'red' : 'transparent'
            }}
            onChange={handleChange}
            value={search}
            name='query'
            type="text"
            placeholder='Avengers, Star Wars, The Matrix'
          />
          <input type="checkbox" name='sortByName' onChange={handleSort} checked={sort} />
          <label htmlFor="sortByName">Ordenar por nombre</label>
          <button type='submit'>Buscar</button>
        </form>
      </header>
      <main>
        {error && <p style={{ color: 'red' }}>{error}</p>}
        {loading? <p>Buscando peliculas...</p> : <Movies movies={movies} />}
      </main>
    </div>
  )
}

export default App
