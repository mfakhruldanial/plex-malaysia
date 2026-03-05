export function CreateUser() {
    return (
        <div className="container fluid mt-5">
            <div className="col-md-6 mx-auto bg-light p-5 rounded-4">
                <h1 className="h3 mb-2">Create</h1>
                <form>
                    <div className="mb-3">
                        <label htmlFor="txtName" className="form-label">Name</label>
                        <input type="text" className="form-control" id="txtName" />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="txtEmail" className="form-label">Email</label>
                        <input type="email" className="form-control" id="txtEmail" />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="txtPassword" className="form-label">Password</label>
                        <input type="password" className="form-control" id="txtPassword" />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="txtConfirmPassword" className="form-label">Confirm Password</label>
                        <input type="password" className="form-control" id="txtConfirmPassword" />
                    </div>
                    <button type="submit" className="btn btn-outline-dark">Save</button>
                </form>
            </div>
        </div>
    );
}