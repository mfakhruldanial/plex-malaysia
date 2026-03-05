import { ReactNode } from "react";
import { Star, StarFill, StarHalf } from "../Icons/Icons";

interface RatingStarComponentProps {
    rating: number | undefined;
}

export default function RatingStartComponent(props: RatingStarComponentProps) {
    const redColor = "#dc3545"
    const yellowColor = "#ffc107"
    const greenColor = "#198754"

    const ratingStar = (): ReactNode => {
        if (props.rating != null) {
            if (props.rating >= 1 && props.rating < 4) {
                return (<Star color={redColor} />)
            }

            if (props.rating >= 4 && props.rating < 6) {
                return (<StarHalf color={yellowColor} />)
            }

            if (props.rating >= 6) {
                return (<StarFill color={greenColor}/>)
            }
        }
        return <Star color={redColor} />
    }

    return (
        <div className="d-flex justify-content-center align-conten-center">
            <span className="text-center fw-bold h4 me-1">{ratingStar()}</span>
            <span className="text-center fw-bold h4 mt-1">{props.rating}</span>
        </div>
    )
}