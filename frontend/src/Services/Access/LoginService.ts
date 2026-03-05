import { LoginModel } from "../../Models/Access/LoginModel";
import { GeneralResponse } from "../../Models/Response/GeneralResponse";

const baseUrl = import.meta.env.VITE_API_URL;

export class LoginService {
    public async Auth(loginData: LoginModel): Promise<GeneralResponse<LoginModel>> {
        return await fetch(`${baseUrl}/Access/Login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include',
            body: JSON.stringify(loginData)
        })
        .then((response: Response) => {
            return response.json();
        })
    }
}