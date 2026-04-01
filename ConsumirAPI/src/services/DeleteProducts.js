const DELETE_PRODUCT_ENDPOINT = 'http://www.APIRest-Practice.somee.com/api/Products/Delete'

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