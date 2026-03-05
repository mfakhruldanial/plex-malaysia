import { Link } from "react-router-dom";

export function Unauthorized() {
    return (
        <div className="min-vh-100 d-flex flex-column bg-light">
            <Link to={"/"} className="btn btn-danger me-auto ms-2 mt-2">
                <i className="bi bi-arrow-left fw-bold me-2"></i>
                Back
            </Link>
            <p className="mx-auto mt-auto fw-bold h1 display-1">
                <i className="bi bi-dash-circle-fill text-danger"></i>
            </p>
            <h1 className="mx-auto fw-bold h1 display-1">401</h1>
            <p className="mx-auto mb-auto fw-bold h1 display-1">Unauthorized</p>
        </div>
    )
}