import { MovieModel } from "../Models/Movie/MovieModel";
import { GeneralResponse } from "../Models/Response/GeneralResponse";

const apiUrl = import.meta.env.VITE_API_URL;

export class MovieService{
    public async GetAll(): Promise<GeneralResponse<MovieModel[]>> {
        return await fetch(`${apiUrl}/Movie`, {
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

    public async GetById(id: number): Promise<GeneralResponse<MovieModel>> {
        return await fetch(`${apiUrl}/Movie/${id}`, {
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

    public async Add(movie: MovieModel): Promise<GeneralResponse<MovieModel>> {
        return await fetch(`${apiUrl}/Movie`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify(movie)
        })
        .then((response: Response) => {
            return response.json();
        })
    }

    public async Update(movie: MovieModel, id: number): Promise<GeneralResponse<MovieModel>> {
        return await fetch(`${apiUrl}/Movie/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify(movie)
        })
        .then((response: Response) => {
            return response.json();
        })
    }
}