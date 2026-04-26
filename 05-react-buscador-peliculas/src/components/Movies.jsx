function ListOfMovies({ movies }) {
  return(
    <ul className="movies">
      {
        movies.map(movie => (
          <li key={movie.id} className="movie">
            <h3>{movie.year}</h3>
            <p>{movie.title}</p>
            <img src={movie.poster} alt={movie.title} />
          </li>
        ))
      }
    </ul>
  )
}

function NoMoviesResults() {
  return <p>No se encontraron peliculas para esta busqueda</p>
}


export function Movies({ movies }) {
  const hasMovies = movies?.length > 0

  return(
    hasMovies
      ? <ListOfMovies movies={movies}/>
      : <NoMoviesResults />
  )
}