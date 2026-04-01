const UPDATE_PRODUCTS_ENDPOINT = 'http://www.APIRest-Practice.somee.com/api/Products/Update'

export const UpdateProducts = async (productoEditado) => {
  try{
    const res = await fetch(UPDATE_PRODUCTS_ENDPOINT, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(productoEditado)
    })

    if(!res.ok){
      const errorType = await res.text()
      console.error("Error al actualizar", errorType)
      return false
    }

    return true

  } catch (error){
    console.error("Error de conexion con Somee al actualizar", error)
    return false
  }
}