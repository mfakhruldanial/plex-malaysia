import { useEffect, useState } from "react";
import { MovieHome } from "../Components/MovieHome";
import { MovieModel } from "../Models/Movie/MovieModel";
import { MovieService } from "../Services/MovieService";
import { GeneralResponse } from "../Models/Response/GeneralResponse";
import Swal from "sweetalert2";
import { useAuth } from "../Auth/AuthProvider";
import NavbarMainComponent from "../Components/NavbarMainComponent";

export function HomeMovie() {
    const movieService = new MovieService()
    const [movies, setMovies] = useState<MovieModel[]>()
    const { setLoading } = useAuth();

    const fetchData = async () => {
        setLoading(true);
        await movieService.GetAll().then((response: GeneralResponse<MovieModel[]>) => {
            if (response.StatusCode == 200) {
                setMovies(response.Object);
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
        })
        setLoading(false)
    }

    useEffect(() => {
        fetchData()
    }, [])

    return (
        <>
            <NavbarMainComponent />
            <main className="container">
                <div className="row">
                    {movies?.map((movie) => (
                        <MovieHome key={movie.Id}
                            id={movie.Id}
                            title={movie.Name}
                            year={movie.Premiere ?? ''}
                            rating={movie.Rating}
                            image={movie.PrimaryImage}
                            quality={movie.Quality} />
                    ))}                  
                </div>
            </main>
        </>
    )
}