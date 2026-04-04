import { useState } from 'react'
import { CreateProducts, UpdateProducts } from '../services/APIProducts'

export function useModal(refetchProducts, setFormData) {
  const [isModalOpen, setIsModalOpen] = useState(false)

  const clearModal = () => {
    setFormData({ nombre: '', precio: '', stock: '', activo: true })
  }

  const isOpen = () => {
    setIsModalOpen(true)
  }

  const onClose = () => {
    setIsModalOpen(false)
    clearModal()
  }

  const onSubmit = async (e, formData, editingId) => {
    e.preventDefault()

    if (editingId) {
      await UpdateProducts({ id: editingId, ...formData })
    } else {
      await CreateProducts({ ...formData, activo: true })
    }

    await refetchProducts()
    onClose()
  }

  return { isModalOpen, isOpen, onClose, onSubmit, clearModal }
}
