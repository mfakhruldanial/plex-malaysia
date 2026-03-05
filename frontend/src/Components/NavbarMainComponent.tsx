import { Link } from 'react-router-dom';
import { LoginAndLogoutButton } from './LoginAndLogoutButton';

export default function NavbarMainComponent() {
  return (
    <header className="p-3 bg-dark text-white mb-3">
      <div className="container">
        <div className="d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start">

          <ul className="nav col-12 col-lg-auto me-lg-auto mb-2 justify-content-center mb-md-0">
            <li><Link to={"/"} className="nav-link px-2 text-secondary">Home</Link></li>    
          </ul>

          <LoginAndLogoutButton />
        </div>
      </div>
    </header>
  )
}