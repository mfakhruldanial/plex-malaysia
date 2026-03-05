import { Link } from "react-router-dom";
import { LoginAndLogoutButton } from "./LoginAndLogoutButton";

export function DashboardNavbar() {
  return (
    <header className="navbar navbar-dark bg-dark py-3">
      <div className="container-fluid">
        <Link className="navbar-brand" to={"/dashboard"}>Navbar</Link>
        <form className="d-flex" role="search">
          <div className="input-group">
          <input className="form-control" type="search" placeholder="Search" aria-label="Search" />
          <button className="btn btn-outline-secondary" type="submit">Search</button>
          </div>
        </form>
        <LoginAndLogoutButton />
      </div>
    </header>
  )
}