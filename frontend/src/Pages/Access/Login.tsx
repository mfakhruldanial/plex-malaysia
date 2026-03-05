import { Button, Form } from "react-bootstrap";
import NavbarComponent from "../../Components/NavbarMainComponent";
import { LoginModel } from "../../Models/Access/LoginModel";
import { LoginService } from "../../Services/Access/LoginService";
import { GeneralResponse } from "../../Models/Response/GeneralResponse";
import { Navigate } from "react-router-dom";
import { useAuth } from "../../Auth/AuthProvider";
import { LoadingComponent } from "../../Components/LoadingComponent";
import { useEffect } from "react";
import SwalAlert from "../../Helpers/SwalAlert";
import { useForm, SubmitHandler } from "react-hook-form";
import { TextAlert } from "../../Components/TextAlert";

export function Login() {
    const loginAuth = new LoginService();

    const { isAuthenticated, setLoading, setIsAuthenticated } = useAuth();
    const { register, handleSubmit, formState: { errors } } = useForm<LoginModel>();

    const onSubmit: SubmitHandler<LoginModel> = data => {
        setLoading(true)
        authData(data)
        setLoading(false)
    }

    const authData = async (login: LoginModel) => {
        await loginAuth.Auth(login).then((response: GeneralResponse<LoginModel>) => {
            SwalAlert({ statusCode: response.StatusCode, message: response.Message })
            if (response.StatusCode === 200) {
                setIsAuthenticated(true)
            }
        })
    }

    useEffect(() => {
        setLoading(false)
    }, [])

    return isAuthenticated ? <Navigate to="/dashboard/user" /> : (
        <>
            <LoadingComponent />
            <NavbarComponent />
            <div className="container fluid mt-5">
                <div className="col-md-5 mx-auto bg-light p-5 rounded-4">
                    <Form onSubmit={handleSubmit(onSubmit)}>
                        <h1 className="text-center mb-3 text-muted">Login</h1>
                        <Form.Group className="mb-3" controlId="formBasicEmail">
                            <Form.Label>Email address</Form.Label>
                            <Form.Control type="email" placeholder="Enter email" {...register("Email", { required: true, pattern: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/ })} />
                            {errors.Email && <TextAlert message="Email is required" variant="danger" />}
                        </Form.Group>

                        <Form.Group className="mb-3" controlId="formBasicPassword">
                            <Form.Label>Password</Form.Label>
                            <Form.Control type="password" placeholder="Password" {...register("Password", { required: true })} />
                            {errors.Password && <TextAlert message="Password is required" variant="danger" />}
                        </Form.Group>
                        <Form.Group className="mb-3" controlId="formBasicCheckbox">
                            <Form.Check type="checkbox" label="Remember me" name="rememberMe" />
                        </Form.Group>
                        <Button className="w-100" variant="primary" type="submit">
                            Login
                        </Button>
                    </Form>
                </div>
            </div>
        </>
    )
}