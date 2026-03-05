import { useEffect, useState } from "react"
import RatingStartComponent from "../../../Components/RatingStarComponent"
import { ytLink } from "../../../Helpers/YoutubeEmbed"
import Swal from "sweetalert2"
import { GeneralResponse } from "../../../Models/Response/GeneralResponse"
import { MovieModel } from "../../../Models/Movie/MovieModel"
import { useAuth } from "../../../Auth/AuthProvider"
import { Link, useParams } from "react-router-dom"
import { MovieService } from "../../../Services/MovieService"

export function MovieDetailDashboard() {
    const movieService = new MovieService()

    const { id } = useParams()
    const { setLoading } = useAuth()
    const [movie, setMovie] = useState<MovieModel>()

    const fetchData = async () => {
        await movieService.GetById(parseInt(id ?? "0")).then((response: GeneralResponse<MovieModel>) => {
            if (response.StatusCode == 200) {
                setMovie(response.Object);
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

    useEffect(() => {
        fetchData()
    }, [id])

    const premiere = movie?.Premiere ? new Date(movie.Premiere) : new Date();
    return (
        <>
            <main className="container">
                <div className="d-flex flex-column">
                    <div className="col">
                        <Link to={`/dashboard/movie/create-movie/${movie?.Id}`} className="btn btn-warning w-100 mb-2">
                            Editar
                        </Link>
                        <div className="d-flex justify-content-between">
                            <h1 className="h1">{movie?.Name}</h1>

                            <div className="d-flex flex-column">
                                <span className="text-center fw-bold h4">Rating</span>
                                <RatingStartComponent rating={movie?.Rating} />
                            </div>

                        </div>

                        <span>{premiere.getFullYear()} | {movie?.Duration}</span>


                        <div className="d-flex mt-1">
                            <img src={movie?.PrimaryImage} alt={movie?.Name} width={'200px'} height={'320'} />
                            <span className="ms-2"><iframe width="560" height="320" src={`${ytLink}/${movie?.Trailer}`} title="" frameBorder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowFullScreen></iframe></span>
                        </div>

                        <p>{movie?.Description}</p>
                        <hr />
                        <span><span className="fw-bold">Director</span> {movie?.Director}</span>
                        <hr />
                    </div>
                </div>
            </main>
        </>
    )
}