import { Button, Form } from "react-bootstrap";
import { MovieModel } from "../../../Models/Movie/MovieModel";
import { MovieService } from "../../../Services/MovieService";
import { useAuth } from "../../../Auth/AuthProvider";
import { GeneralResponse } from "../../../Models/Response/GeneralResponse";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import SwalAlert from "../../../Helpers/SwalAlert";
import { SubmitHandler, useForm } from "react-hook-form";
import { TextAlert } from "../../../Components/TextAlert";

export function CreateMovie() {
    const movieService = new MovieService()

    const { setLoading } = useAuth();
    const { id } = useParams<{ id: string }>()
    const navigate = useNavigate();
    const { register, handleSubmit, formState: { errors }, setValue, reset } = useForm<MovieModel>();
    const [movie, setMovie] = useState<MovieModel>()
    const [response, setResponse] = useState<GeneralResponse<MovieModel>>()
    const movieId = parseInt(id ?? "0")
    const [isEditMode] = useState(movieId !== 0)    

    const onSubmit: SubmitHandler<MovieModel> = data => {
        setLoading(true)
        if (isEditMode) {
            movieService.Update(data, movieId).then((response: GeneralResponse<MovieModel>) => {
                SwalAlert({ statusCode: response.StatusCode, message: response.Message })
            })
        }
        else {
            movieService.Add(data).then((response: GeneralResponse<MovieModel>) => {
                SwalAlert({ statusCode: response.StatusCode, message: response.Message })
            })
        }
        navigate("/dashboard/movie")
        setLoading(false)
    }

    const fetchData = async () => {
        if (isEditMode) {
            await movieService.GetById(movieId).then((response: GeneralResponse<MovieModel>) => {
                setResponse(response)
                if (response.StatusCode == 200) {
                    setMovie(response.Object);
                }
                SwalAlert({ statusCode: response.StatusCode, message: response.Message })
            })
        }
        else {
            reset();
        }
    }

    const setData = () => {
        const date = movie?.Premiere ? new Date(movie.Premiere).toISOString().slice(0, 10) : "";
        console.log(date)
        if (response?.StatusCode === 200) {
            if (movie != undefined) {
                setValue("Id", movie.Id)
                setValue("Name", movie.Name)
                setValue("Description", movie.Description)
                setValue("PrimaryImage", movie.PrimaryImage)
                setValue("Trailer", movie.Trailer)
                setValue("Director", movie.Director)
                setValue("Rating", movie.Rating)
                setValue("Quality", movie.Quality)
                setValue("Premiere", date)
                setValue("Duration", movie.Duration)
            }
        }
    }

    useEffect(() => {
        setLoading(true)
        fetchData();
        setData();
        setLoading(false)
    }, [setData()])

    return (
        <div className="container fluid">
            <div className="col-md-6 mx-auto bg-light px-5 py-3 rounded-4">
                <Form onSubmit={handleSubmit(onSubmit)}>
                    <h1 className="text-center mb-1 text-muted h2">{isEditMode ? "Edit" : "Create"}</h1>
                    <Form.Group className="mb-3" controlId="formTitle" hidden>
                        <Form.Label>Id</Form.Label>
                        <Form.Control type="number" name="id" value={movie?.Id} />
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formTitle">
                        <Form.Label>Title</Form.Label>
                        <Form.Control type="text" placeholder="Enter movie title" {...register("Name", { required: "Title is required" })} />
                        {errors.Name && <TextAlert message={errors.Name.message ?? "Error"} variant="danger" />}
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formDescription">
                        <Form.Label>Description</Form.Label>
                        <Form.Control as="textarea" rows={3} placeholder="Enter description" {...register("Description", { required: "Description is required" })} />
                        {errors.Description && <TextAlert message={errors.Description.message ?? "Error"} variant="danger" />}
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formImage">
                        <Form.Label>Image</Form.Label>
                        <Form.Control type="text" placeholder="Enter image link" {...register("PrimaryImage", { required: "PrimaryImage is required" })} />
                        {errors.PrimaryImage && <TextAlert message={errors.PrimaryImage.message ?? "Error"} variant="danger" />}
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formTrailer">
                        <Form.Label>Trailer</Form.Label>
                        <Form.Control type="text" placeholder="Enter youtube id" {...register("Trailer", { required: "Trailer is required" })} />
                        {errors.Trailer && <TextAlert message={errors.Trailer.message ?? "Error"} variant="danger" />}
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formDirector">
                        <Form.Label>Director</Form.Label>
                        <Form.Control type="text" placeholder="Enter director's full name" {...register("Director", { required: "Director is required" })} />
                        {errors.Director && <TextAlert message={errors.Director.message ?? "Error"} variant="danger" />}
                    </Form.Group>

                    <div className="row cols-row-2">
                        <div className="col">
                            <Form.Group className="mb-3" controlId="formQuality">
                                <Form.Label>Quality</Form.Label>
                                <Form.Control type="text" placeholder="HD, FHD, 2K, 4K..." {...register("Quality", { required: "Quality is required" })} />
                                {errors.Quality && <TextAlert message={errors.Quality.message ?? "Error"} variant="danger" />}
                            </Form.Group>
                        </div>
                        <div className="col">
                            <Form.Group className="mb-3" controlId="formDuration">
                                <Form.Label>Duration</Form.Label>
                                <Form.Control type="text" placeholder="Enter duration hours and minutes" {...register("Duration", { required: "Duration is required" })} />
                                {errors.Duration && <TextAlert message={errors.Duration.message ?? "Error"} variant="danger" />}
                            </Form.Group>
                        </div>
                    </div>

                    <div className="row cols-row-2">
                        <div className="col">
                            <Form.Group className="mb-3" controlId="formRating">
                                <Form.Label>Rating</Form.Label>
                                <Form.Control type="number" placeholder="Enter rating" {...register("Rating", { required: "Rating is required" })} />
                                {errors.Rating && <TextAlert message={errors.Rating.message ?? "Error"} variant="danger" />}
                            </Form.Group>
                        </div>
                        <div className="col">
                            <Form.Group className="mb-3" controlId="formPremiere">
                                <Form.Label>Premiere</Form.Label>
                                <Form.Control type="date" placeholder="Enter date of premiere" {...register("Premiere", { required: "Premiere is required" })} />
                                {errors.Premiere && <TextAlert message={errors.Premiere.message ?? "Error"} variant="danger" />}
                            </Form.Group>
                        </div>
                    </div>
                    <Button className="w-100" variant="success" type="submit">
                        Save
                    </Button>
                </Form>
            </div>
        </div>
    )
}