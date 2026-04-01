const GET_PRODUCTS_ENDPOINT = 'http://www.APIRest-Practice.somee.com/api/Products/List'

export const GetProducts = async () => {
  try {
    const res = await fetch(GET_PRODUCTS_ENDPOINT)
    
    if(!res.ok){
      console.log(res)
      return []
    }

    const data = await res.json()

    return data
  } catch (error){
    console.error("Error de conexion con Somee", error)
    return []
  }
}