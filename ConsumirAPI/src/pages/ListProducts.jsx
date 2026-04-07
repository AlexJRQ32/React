import { useState } from 'react'
import List from '../components/List/List'
import { useGetProducts } from '../hooks/useGetProducts'
import { useModal } from '../hooks/useModal'
import Modal from '../components/Modal/Modal'
import { useButtons } from '../hooks/useButtons'

function ListProducts () {
  const { products, refetch } = useGetProducts()
  const [formData, setFormData] = useState({ nombre: '', precio: '', stock: '' })
  const { isModalOpen, isOpen, onClose, onSubmit, clearModal } = useModal(refetch, setFormData)
  const { onEdit, editingId, onDelete, onAdd, filtered, query, setQuery } = useButtons(
    isOpen,
    setFormData,
    refetch,
    clearModal,
    products
  )

  const cols = [
    { value: 'ID' },
    { value: 'Product' },
    { value: 'Price' },
    { value: 'Stock' },
    { value: 'Status' },
    { value: 'Actions' },
  ]

  const fields = [
    { label: 'Name', name: 'nombre', type: 'text' },
    { label: 'Price', name: 'precio', type: 'number' },
    { label: 'Stock', name: 'stock', type: 'number' },
  ]

  return(
    <div className="body">
      <List
        filtered={filtered}
        title={'Products'}
        cols={cols}
        query={query}
        setQuery={setQuery}
        onAdd={onAdd}
        onEdit={onEdit}
        onDelete={onDelete}
      />
      <Modal
        isOpen={isModalOpen}
        onClose={onClose}
        onSubmit={(e) => onSubmit(e, formData, editingId)}
      >
        {fields.map((f) => (
          <div key={f.name}>
            <label>{f.label}</label>
            <input
              type={f.type}
              name={f.name}
              value={formData[f.name] ?? ''}
              onChange={(e) => setFormData({ ...formData, [f.name]: e.target.value })}
            />
          </div>
        ))}
        {editingId && (
          <div className="status">
            <label>Status</label>
            <input
              type="checkbox"
              id="checkboxInput"
              checked={formData.activo ?? true}
              onChange={(e) => setFormData({ ...formData, activo: e.target.checked })}
            />
            <label htmlFor="checkboxInput" className="toggleSwitch"></label>
          </div>
        )}
        <button type="submit">Guardar</button>
      </Modal>
    </div>
  )
}

export default ListProducts