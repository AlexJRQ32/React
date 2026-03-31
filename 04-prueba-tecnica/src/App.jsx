import './App.css'
import { useCatImage } from './hooks/useCatImage'
import { useCatFact } from './hooks/useCatFact'

function App () {
  const { fact, refreshFact } = useCatFact()
  const { imageUrl } = useCatImage({ fact })

  const handdleClick = async () => {
    refreshFact()
  }

  return (
    <main>
      <h1>Cats App</h1>
      <button onClick={handdleClick}>Get new Fact</button>
      <section>
        {fact && <p>{fact}</p>}
        {imageUrl && (
          <img src={imageUrl} alt={`image extracted using the first three words for ${fact}`} />
        )}
      </section>
    </main>
  )
}

export default App
