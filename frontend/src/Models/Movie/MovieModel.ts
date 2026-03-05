import { ENTITY_STATUS } from "../ENUM/ENTITY_STATUS";

export class MovieModel {
    public Id: number = 0;
    public PrimaryImage?: string = '';
    public Name?: string = '';
    public Description?: string = '';
    public Trailer?: string = '';
    public Quality?: string = '';
    public Director?: string = '';
    public Rating?: number = 0;
    public Premiere?: string = '';
    public Duration?: string = '';
    public CreatedAt?: string = '';
    public Status?: ENTITY_STATUS = ENTITY_STATUS.ACTIVE;
    public MovieCasts?: MovieCast[] = new Array<MovieCast>();
    public MovieCategories?: MovieCategory[] = new Array<MovieCategory>();
    public MovieGenres?: MovieGenre[] = new Array<MovieGenre>();
    public MovieLikes?: MovieLike[] = new Array<MovieLike>();
    public Reviews?: Review[] = new Array<Review>();
    public Watchlists?: Watchlist[] = new Array<Watchlist>();
}

export interface MovieCast {
    // Define las propiedades de MovieCast
}

export interface MovieCategory {
    // Define las propiedades de MovieCategory
}

export interface MovieGenre {
    // Define las propiedades de MovieGenre
}

export interface MovieLike {
    // Define las propiedades de MovieLike
}

export interface Review {
    // Define las propiedades de Review
}

export interface Watchlist {
    // Define las propiedades de Watchlist
}
