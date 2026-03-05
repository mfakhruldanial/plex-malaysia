import { useAuth } from "../Auth/AuthProvider";
import { Navigate } from "react-router-dom";
import { LoadingComponent } from "./LoadingComponent";
import { DashboardLayout } from "../Pages/Dashboard/DashboardLayout";

export function ProtectedRoute() {
    const { isAuthenticated } = useAuth();

    return (
        <>
            {isAuthenticated ?
                <>
                    <LoadingComponent />
                    <DashboardLayout />
                </> 
            : <Navigate to="/login" />}
        </>
    )
}