import { GeneralResponse } from "../Models/Response/GeneralResponse";
import { UserModel } from "../Models/UserModel";

const apiUrl = import.meta.env.VITE_API_URL;

export class UserService{
    public async GetAll(): Promise<GeneralResponse<UserModel[]>> {
        return await fetch(`${apiUrl}/User`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        })
        .then((response: Response) => {
            return response.json();
        })
    }

    public async GetById(id: number): Promise<GeneralResponse<UserModel>> {
        return await fetch(`${apiUrl}/User/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        })
        .then((response: Response) => {
            return response.json();
        })
    }

    public async PostNewUser(user: UserModel): Promise<GeneralResponse<UserModel>> {
        return await fetch(`${apiUrl}/User`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify(user)
        })
        .then((response: Response) => {
            return response.json();
        })
    }

    public async PutEditUser(user: UserModel, id: number): Promise<GeneralResponse<UserModel>> {
        return await fetch(`${apiUrl}/User/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify(user)
        })
        .then((response: Response) => {
            return response.json();
        })
    }
}