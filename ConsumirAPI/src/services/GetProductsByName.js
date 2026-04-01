const GET_PRODUCT_BY_NAME_ENDPOINT = "http://www.APIRest-Practice.somee.com/api/Products/SearchName"

export const GetProductByName = async (name) => {
  try{
    const res = await fetch(`${GET_PRODUCT_BY_NAME_ENDPOINT}/${name}`)

    if(!res.ok){
      console.error("No se pudo encontrar el producto con Nombre:", name)
      return []
    }

    const data = await res.json()
    return data

  } catch (error){
    console.error("Error de conexion con Somee al buscar por nombre", error)
    return []
  }
}