const CREATE_PRODUCT_ENDPOINT = 'http://www.APIRest-Practice.somee.com/api/Products/Create'
const DELETE_PRODUCT_ENDPOINT = 'http://www.APIRest-Practice.somee.com/api/Products/Delete'
const GET_PRODUCTS_ENDPOINT = 'http://www.APIRest-Practice.somee.com/api/Products/List'
const GET_PRODUCT_BY_NAME_ENDPOINT = "http://www.APIRest-Practice.somee.com/api/Products/SearchName"
const UPDATE_PRODUCTS_ENDPOINT = 'http://www.APIRest-Practice.somee.com/api/Products/Update'

//#region HttpGet
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
//#endregion

//#region HttpPost
export const CreateProducts = async (nuevoProducto) => {
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
//#endregion

//#region HttpPut
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
//#endregion

//#region HttpDelete
export const DeleteProduct = async (id) => {
  try{
    const res = await fetch(`${DELETE_PRODUCT_ENDPOINT}/${id}`, {
      method: 'DELETE',
    })

    if(!res.ok){
      console.error("No se pudo eliminar el producto con ID", id)
      return false
    }

    return true
  } catch (error) {
    console.error("Error de conexion con Somee al Eliminar", error)
  }
}
//#endregion
