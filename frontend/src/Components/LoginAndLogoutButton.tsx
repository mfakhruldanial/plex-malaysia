import { useEffect } from "react";
import { useAuth } from "../Auth/AuthProvider";
import { Link } from "react-router-dom";

export function LoginAndLogoutButton() {
    const { isAuthenticated, logout } = useAuth();

    useEffect(() => { }, [isAuthenticated]);

    return isAuthenticated ? (
        <div className="text-end">
            <Link to={"/dashboard"} className="btn btn-outline-primary me-2">Dashboard</Link>
            <Link to={"/"} className="btn btn-outline-primary me-2">Home</Link>
            <Link to={"/"} onClick={logout} className="btn btn-outline-light me-2">Logout</Link>
        </div>
    ) : (
        <div className="text-end">
            <Link to={"/login"} className="btn btn-outline-light me-2">Login</Link>
            <Link to={"/signup"} className="btn btn-warning">Sign-up</Link>
        </div>
    )
}