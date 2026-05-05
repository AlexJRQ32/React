import './Button.css'
import { useNavigate } from 'react-router-dom'

export function ButtonRedirect({ classN, icon, title, type, site, style }) {
  const navigate = useNavigate()

  const handleClick = () => navigate(`/${site}`)
  return (
    <button
      className={`btn-redirect btn-${classN}`}
      onClick={handleClick}
      type={type}
      style={style}
    >
      <i className={`fa-solid fa-${icon}`}></i>
      {title}
    </button>
  )
}

export function ButtonAction({ classN, icon, type, onclick, style }) {
  return (
    <button
      className={`btn-action btn-${classN}`}
      onClick={onclick}
      type={type}
      style={style}
    >
      <i className={`fa-solid fa-${icon}`}></i>
    </button>
  )
}
