import { SignupModel } from '../../Models/Access/SignupModel';
import { GeneralResponse } from '../../Models/Response/GeneralResponse';

const baseUrl = import.meta.env.BASE_URL;

export class SignupService {

    public async Register(user: SignupModel): Promise<GeneralResponse<SignupModel>> {
        return await fetch(`${baseUrl}/Access/Signup`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include',
            body: JSON.stringify(user)
        })
        .then((response) => {
            return response.json();
        })
    }
}