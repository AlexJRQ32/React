const CREATE_PRODUCT_ENDPOINT = 'http://www.APIRest-Practice.somee.com/api/Products/Create'

export const UpdateProducts = async (nuevoProducto) => {
  try{
    const res = await fetch(CREATE_PRODUCT_ENDPOINT, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(nuevoProducto)
    })

    if(!res.ok){
      const errorType = await res.text()
      console.error("Error al crear", errorType)
      return null
    }

    const data = await res.json()
    return data

  } catch (error){
    console.error("Error de conexion con Somee al crear", error)
    return null
  }
}