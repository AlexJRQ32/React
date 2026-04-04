import './Modal.css'
import Buttons from '../Buttons/Buttons'

function Modal({ isOpen, onClose, onSubmit, children }) {
  if (!isOpen) return null

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <Buttons onClick={onClose} type={'close-modal'} icon={'xmark'}/>
        <form onSubmit={onSubmit}>{children}</form>
      </div>
    </div>
  )
}

export default Modal
