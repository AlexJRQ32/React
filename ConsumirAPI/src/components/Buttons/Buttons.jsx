import './Buttons.css'

function Buttons({ onClick, icon, type }) {
  return (
    <button className={`btn-changes ${type}`} onClick={onClick}>
      <i className={`fa-solid fa-${icon}`}></i>
    </button>
  )
}

export default Buttons
