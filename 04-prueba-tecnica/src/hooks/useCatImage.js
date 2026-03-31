import { useState, useEffect } from 'react'

export function useCatImage ({ fact }) {
  // Se inicializa el estado del Custom Hook
  const [imageUrl, setImageUrl] = useState()

  // Este es el cuerpo del Custom Hook
  useEffect(() => {
    // Si el estado no esta entonces return Nada
    if (!fact) return

    // Se obtienen las Tres primeras palabras en un json
    // se separan y se ingresan con .Join(' ')
    // a una cadena de texto unica
    const threeFirstWords = fact.split(' ', 3).join(' ')
    console.log(threeFirstWords)

    // Se declara el Nndpoint para traer las imagenes
    // de los gatos segun las 3 primeras palabras
    const CAT_ENDPOINT_GET_IMG = `https://cataas.com/cat/says/${threeFirstWords}?fontSize=50&fontColor=red&json=true`

    // Se hace el Fetch para extraer los datos del Endpoint
    fetch(CAT_ENDPOINT_GET_IMG)
      .then((res) => res.json())
      .then((response) => {
        const { url } = response
        setImageUrl(url)
      })
  }, [fact])

  return { imageUrl }
}
