import { ButtonRedirect } from '../Button/Button'
import './SearchBar.css'

export function SearchBar({ icon, style }) {
  return (
    <>
      <form className="search-bar" style={style}>
        <span>
          <i className={`fa-solid fa-${icon}`}></i>
          <input type="text" placeholder="Search what you want to eat" />
        </span>
        <ButtonRedirect title={'Search'} site={'search'} style={{borderRadius: '12px'}} />
      </form>
    </>
  )
}
