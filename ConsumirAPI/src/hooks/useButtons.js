import { useState } from "react"
import { DeleteProduct } from "../services/APIProducts"

export const useButtons = (isOpen, setFormData, refetch, clearModal) => {
  const [editingId, setEditingId] = useState(null)

  const onAdd = () => {
    isOpen()
    clearModal()
  }

  const onEdit = (item) => {
    setEditingId(item.id)
    setFormData({ nombre: item.nombre, precio: item.precio, stock: item.stock, activo: item.activo })
    isOpen()
  }

  const onDelete = async (id) => {
    await DeleteProduct(id)
    await refetch()
  }

  return { editingId, onEdit, onDelete, onAdd }
}