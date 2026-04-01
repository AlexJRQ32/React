export function List({ productos }) {
  return(
    <>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Producto</th>
            <th>Precio</th>
            <th>Stock</th>
            <th>Estado</th>
          </tr>
        </thead>
        <tbody>
          {productos.map((p) => (
            <tr key={p.id}>
              <td>{p.id}</td>
              <td>{p.nombre}</td>
              <td>{p.precio}</td>
              <td>{p.stock}</td>
              <td>{p.activo ? "Activo" : "Inactivo"}</td>
            </tr>
          ))}
        </tbody>
        
      </table>
    </>
  )
}