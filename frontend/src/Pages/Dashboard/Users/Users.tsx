import Swal from "sweetalert2";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { useAuth } from "../../../Auth/AuthProvider";
import { UserService } from "../../../Services/UserService";
import { UserModel } from "../../../Models/UserModel";
import { GeneralResponse } from "../../../Models/Response/GeneralResponse";

export function Users() {
    const userService = new UserService();
    const [users, setUsers] = useState<UserModel[]>();
    const { setLoading } = useAuth();

    const fetchData = async () => {
        setLoading(true);
        await userService.GetAll().then((response: GeneralResponse<UserModel[]>) => {
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
    };

    useEffect(() => {
        fetchData();
    }, []);

    return (
        <>
            <div className="container p-2">
                <h1>User</h1 >
                <div className="d-flex justify-content-end my-2">
                    <Link to={"new-user"} className="btn btn-dark">
                        <i className="bi bi-plus-lg me-1"></i>
                        New
                    </Link>
                </div>
                <div className="table-responsive bg-light p-4 rounded">
                    <table className="table table-striped shadow">
                        <thead>
                            <tr className="text-center">
                                <th className="fw-bold">Name</th>
                                <th className="fw-bold">Last name</th>
                                <th className="fw-bold">Rol</th>
                                <th className="fw-bold">Email</th>
                                <th className="fw-bold">Opciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            {users?.map((user: UserModel) => {
                                return (
                                    <tr className="text-center" key={user.Id}>
                                        <td>{user.FirstName}</td>
                                        <td>{user.LastName}</td>
                                        <td>{user.Rol}</td>
                                        <td>{user.Email}</td>
                                        <td className="d-flex justify-content-evenly">
                                            <Link to={`edit-user/${user.Id}`}>
                                                <i className="bi bi-pencil-square text-success"></i>
                                            </Link>
                                            <Link to={`/${user.Id}`}>
                                                <i className="bi bi-trash3 text-danger"></i>
                                            </Link>
                                        </td>
                                    </tr>
                                )
                            })}
                        </tbody>
                    </table>
                </div>
            </div>
        </>
    )
}