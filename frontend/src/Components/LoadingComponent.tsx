import { useAuth } from "../Auth/AuthProvider";


export function LoadingComponent() {
    const { loading } = useAuth();

    return (
        <div className={`modal-loading ${loading ? 'd-flex' : 'd-none'}`}>
            <div className="mx-auto my-auto">
                <div className="modal-content-loading">
                    <div className="spinner-grow" style={{ width: '3rem', height: '3rem' }} role="status">
                        <span className="visually-hidden">Loading...</span>
                    </div>
                </div>
            </div>
        </div>
    );
}