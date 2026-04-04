import Buttons from '../Buttons/Buttons'
import './List.css'

function List({ object, title, cols, onAdd, onEdit, onDelete }) {
  return (
    <>
      <div className="title">
        <h1>List of {title}</h1>
        <Buttons icon="plus" type="btn-add" onClick={onAdd} />
      </div>
      <table>
        <thead>
          <tr>
            {cols.map((c) => (
              <th key={c.value}>{c.value}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {object.map((p) => (
            <tr key={p.id}>
              <td>{p.id}</td>
              <td>{p.nombre}</td>
              <td>{p.precio}</td>
              <td>{p.stock}</td>
              <td>{p.activo ? 'Enable' : 'Disable'}</td>
              <td className="actions">
                <Buttons icon="pencil" type="btn-edit" onClick={() => onEdit(p)} />
                <Buttons icon="trash" type="btn-delete" onClick={() => onDelete(p.id)} />
              </td> 
            </tr>
          ))}
        </tbody>
      </table>
    </>
  )
}

export default List
