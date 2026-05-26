import { NavLink } from 'react-router-dom'
import { ButtonRedirect } from '../Button/Button'
import './AuthForms.css'
import { RoleCard } from '../Cards/Cards'
import { useMappedObjects } from '../../hooks/useMappedObjects'

export function SignInForm() {
  return (
    <>
      <form>
        <h1>Sign In</h1>
        <div className="auth-input">
          <label htmlFor="email">Email</label>
          <div className="input-wrap">
            <input type="email" name="email" placeholder="example@gmail.com" />
            <i className="fas fa-envelope" ></i>
          </div>
        </div>
        <div className="auth-input">
          <label htmlFor="password">Password</label>
          <div className="input-wrap">
            <input type="password" name="password" placeholder="••••••••" />
            <i className="fas fa-lock" ></i>
          </div>
        </div>
        <div className="auth-input">
          <NavLink to={"#"}><p>Forgot your password?</p></NavLink>
        </div>
        <ButtonRedirect className={'action-large'} site={'/'} title={'SIGN IN'}/>
          <NavLink to={'/auth/sign-up'}><p>Don't have an account? Sign up</p></NavLink>
      </form>
    </>
  )
}

export function SignUpForm() {
  return (
    <>
      <form>
        <h1>Sign Up</h1>
        <div className="auth-input">
          <label htmlFor="name">Name</label>
          <div className="input-wrap">
            <input type="text" name="name" placeholder="Enter your name" />
            <i className="fas fa-user" ></i>
          </div>
        </div>
        <div className="auth-input">
          <label htmlFor="email">Email</label>
          <div className="input-wrap">
            <input type="email" name="email" placeholder="example@gmail.com" />
            <i className="fas fa-envelope" ></i>
          </div>
        </div>
        <div className="auth-input">
          <label htmlFor="password">Password</label>
          <div className="input-wrap">
            <input type="password" name="password" placeholder="••••••••" />
            <i className="fas fa-key" ></i>
          </div>
        </div>
        <div className="auth-input">
          <label htmlFor="confirm-password">Confirm Password</label>
          <div className="input-wrap">
            <input type="password" name="confirm-password" placeholder="••••••••" />
            <i className="fas fa-lock" ></i>
          </div>
        </div>
        <div class="phone-container">
          <div className="auth-input">
            <label htmlFor="country-code">Country Code</label>
            <div className="input-wrap">
              <select id="country-code" name="country-code">
                <option value="+1">+1 (US)</option>
                <option value="+34">+34 (ES)</option>
                <option value="+52">+52 (MX)</option>
                <option value="+54">+54 (AR)</option>
                <option value="+506">+506 (CR)</option>
              </select>
              <i className='fas fa-globe'></i>
            </div>
          </div>
          <div className="auth-input">
            <label htmlFor="phone">Phone Number</label>
            <div className="input-wrap">
              <input
                type="tel" 
                id="phone" 
                name="phone" 
                placeholder="123-456-7890" 
                required
              />
              <i className='fas fa-phone'></i>
            </div>
          </div>
        </div>
        <ButtonRedirect className={'action-large'} site={'/auth/choose-role'} title={'SIGN UP'}/>
          <NavLink to={'/auth/sign-in'}><p>Already have an account? Sign in</p></NavLink>
      </form>
    </>
  )
}

export function ChooseRoleForm() {
  const { roles } = useMappedObjects()
  return(
    <div className="role-container">
      <span><h2>Rappi</h2><h2>'Doz</h2></span>
      <h1>Who are you?</h1>
      <RoleCard roles={roles} />
      <NavLink to={'/auth/sign-in'}>
        <i className='fas fa-arrow-left'></i>
        Go Back
      </NavLink>
    </div>
  )
}
