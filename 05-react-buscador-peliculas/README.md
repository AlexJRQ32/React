## Ejercicio: Buscador de Películas

Crea una aplicación para buscar películas utilizando la API de OMDb.

API a usar:

- URL: https://www.omdbapi.com/  https://www.omdbapi.com/?apikey=4287ad07&s=Avengers
- API_KEY: 4287ad07

Requerimientos:

💹 Necesita mostrar un input para buscar la película y un botón para buscar.
💹 Lista las películas encontradas y muestra el título, año y poster.
💹 Que el formulario funcione.
💹 Haz que las películas se muestren en un grid responsivo.
💹 Hacer el fetching de datos de la API

Primera iteración:

💹 Evitar duplicados: Evitar que se haga la misma búsqueda dos veces seguidas.
💹 Búsqueda en tiempo real: Haz que la búsqueda se haga automáticamente al escribir.
💹 Optimización (Debounce): Evita que se haga la búsqueda continuamente al escribir para ahorrar peticiones.