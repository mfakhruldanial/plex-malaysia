import Swal from "sweetalert2";

interface SwalComponentProps {
  statusCode: number;
  message: string;
}

export default function SwalAlert({ statusCode, message }: SwalComponentProps) {
  if (200 === statusCode || 201 === statusCode || 204 === statusCode) {
    Swal.fire({
      title: 'Success',
      text: message,
      icon: 'success',
      confirmButtonText: 'Ok',
    });
  } else if (statusCode === 500 || statusCode === 400) {
    Swal.fire({
      title: 'Error',
      text: message,
      icon: 'error',
      confirmButtonText: 'Ok',
    });
  } else {
    Swal.fire({
      title: 'Warning',
      text: message,
      icon: 'warning',
      confirmButtonText: 'Ok',
    });
  }
}