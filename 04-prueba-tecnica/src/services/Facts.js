const CAT_ENDPOIN_RANDOM_FACT = 'https://catfact.ninja/fact'

export const getRandomFact = async () => {
  const res = await fetch(CAT_ENDPOIN_RANDOM_FACT)
  const data = await res.json()
  const { fact } = data
  return fact
}
