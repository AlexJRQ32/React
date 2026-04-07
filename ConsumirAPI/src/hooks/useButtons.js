import { useState } from 'react'
import { DeleteProduct } from '../services/apiProducts'

export const useButtons = (isOpen, setFormData, refetch, clearModal, object = []) => {
  const [editingId, setEditingId] = useState(null)
  const [query, setQuery] = useState('')

  const onAdd = () => {
    isOpen()
    clearModal()
  }

  const onEdit = (item) => {
    setEditingId(item.id)
    setFormData({
      nombre: item.nombre,
      precio: item.precio,
      stock: item.stock,
      activo: item.activo,
    })
    isOpen()
  }

  const onDelete = async (id) => {
    await DeleteProduct(id)
    await refetch()
  }

  const filtered = object.filter((p) =>
    p.nombre.toLowerCase().includes(query.toLowerCase())
  )

  return { editingId, onEdit, onDelete, onAdd, query, setQuery, filtered }
}
