import { Alert } from "react-bootstrap";

interface AlertProps {
    message: string;
    variant: string;
}

export function TextAlert(props: AlertProps){
    return(
        <Alert className="my-2" variant={props.variant}>
            {props.message}	
        </Alert>
    )
}