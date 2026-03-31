import { useEffect, useState } from 'react'
import { getRandomFact } from '../services/Facts'

export const useCatFact = () => {
  // Se inicializa el estado dentro del Custom Hook
  const [fact, setFact] = useState('')

  // Se refresca el Fact que obtenemos de services para
  // luego utilizarlo cuando llamemos Custom hook
  const refreshFact = () => {
    getRandomFact().then((newFact) => setFact(newFact))
  }

  // Este seria el custom hook que va actualizar el Fact
  useEffect(refreshFact, [])

  // Aqui se retorna el estado y el Refresh por si es
  // necesario utilizarlos en otros componentes
  return { fact, refreshFact }
}
