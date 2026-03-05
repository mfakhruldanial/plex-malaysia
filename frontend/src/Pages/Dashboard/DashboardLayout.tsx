import { Outlet} from "react-router-dom";
import { DashboardNavbar } from "../../Components/DashboardNavbar";
import SideBarDashboard from "../../Components/SideBarDashboard";

export function DashboardLayout() {
    

    return (
        <>
            <DashboardNavbar />

            <div className="container-fluid">
                <div className="row">
                    <div className="col-md-2 m-0 p-0">
                        <SideBarDashboard />
                    </div>
                    <main className="col p-2 mt-2">
                        <Outlet />
                    </main>
                </div>
            </div>
        </>
    )
}