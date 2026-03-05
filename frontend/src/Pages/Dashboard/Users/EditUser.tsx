import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useAuth } from "../../../Auth/AuthProvider";
import { UserService } from "../../../Services/UserService";
import { GeneralResponse } from "../../../Models/Response/GeneralResponse";
import { UserModel } from "../../../Models/UserModel";
import Swal from "sweetalert2";

export function EditUser() {
    const userService = new UserService();
    const { id } = useParams();
    const [userData, setUsers] = useState<UserModel>();
    const { setLoading } = useAuth();

    const fetchData = async () => {
        setLoading(true);
        if (id != undefined) {
            await userService.GetById(parseInt(id)).then((response: GeneralResponse<UserModel>) => {
                if (response.StatusCode == 200) {
                    setUsers(response.Object);
                }
                else {
                    if (response.StatusCode <= 500) {
                        Swal.fire({
                            title: 'Error',
                            text: response.Message,
                            icon: 'error',
                            confirmButtonText: 'Ok'
                        })
                    }
                    else {
                        Swal.fire({
                            title: 'Warning',
                            text: response.Message,
                            icon: 'warning',
                            confirmButtonText: 'Ok'
                        })
                    }
                }
            }).catch(() => {
                Swal.fire({
                    title: 'Error',
                    text: 'An error has occurred',
                    icon: 'error',
                    confirmButtonText: 'Ok'
                })
            });
            setLoading(false);
        }
    }

    useEffect(() => {
        fetchData();
    }, [id]);

    return (
        <div className="container fluid">
            <div className="col-md-6 mx-auto bg-light px-5 py-2 rounded-4">
                <h1 className="h3 mb-2">Edit</h1>
                <form>
                    <div className="mb-3">
                        <label htmlFor="txtName" className="form-label">Name</label>
                        <input type="text" className="form-control" id="txtName" value={userData?.FirstName }/>
                    </div>
                    <div className="mb-3">
                        <label htmlFor="txtName" className="form-label">Last name</label>
                        <input type="text" className="form-control" id="txtName" value={userData?.LastName }/>
                    </div>
                    <div className="mb-3">
                        <label htmlFor="txtEmail" className="form-label">Email</label>
                        <input type="email" className="form-control" id="txtEmail" value={userData?.Email}/>
                    </div>
                    <div className="mb-3">
                        <label htmlFor="slctRol" className="form-label">Rol</label>
                        <option>Select</option>
                        <option>Select</option>
                        <option>Select</option>
                        {/* <input type="select" className="form-control" id="slctRol" /> */}
                    </div>
                    <button type="submit" className="btn btn-outline-dark">Save</button>
                </form>
            </div>
        </div>
    );
}