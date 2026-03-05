import { BrowserRouter, Route, Routes } from 'react-router-dom'
// Login
import { Login } from './Pages/Access/Login'
import { Signup } from './Pages/Access/Signup'
// Movie
import { HomeMovie } from './Pages/HomeMovie'
import { MovieDetail } from './Pages/MovieDetail'
import { CreateMovie } from './Pages/Dashboard/Movies/CreateMovie'
// Shared
import { ProtectedRoute } from './Components/ProtectedRoute'
import { NotFound } from './Pages/HttpCodes/NotFound'
import { Forbidden } from './Pages/HttpCodes/Forbidden'
import { Unauthorized } from './Pages/HttpCodes/Unauthorized'
import { LoadingComponent } from './Components/LoadingComponent'
// User
import { Users } from './Pages/Dashboard/Users/Users'
import { CreateUser } from './Pages/Dashboard/Users/CreateUser'
import { EditUser } from './Pages/Dashboard/Users/EditUser'
import { MoviesDashboard } from './Pages/Dashboard/Movies/MoviesDashboard'
import { MovieDetailDashboard } from './Pages/Dashboard/Movies/MovieDetailDashboard'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<HomeMovie />}/>
        <Route path="/movie/:id" element={<MovieDetail />} />
        <Route path="/login" element={<Login />} />
        <Route path="/signup" element={<Signup />} />
        <Route path="/dashboard" element={<ProtectedRoute />}>
            <Route path="/dashboard/user" element={<Users />}/>,
            <Route path="/dashboard/user/new-user" element={<CreateUser />} />,
            <Route path="/dashboard/user/edit-user/:id" element={<EditUser />} />, 
            <Route path="/dashboard/l" element={<LoadingComponent />} />

            <Route path="/dashboard/movie/create-movie/:id" element={<CreateMovie />} />
            <Route path="/dashboard/movie" element={<MoviesDashboard />} />
            <Route path="/dashboard/movie/:id" element={<MovieDetailDashboard />} />
        </Route>

      // Http codes
        <Route path="/404" element={<NotFound />} />
        <Route path="/403" element={<Forbidden />} />
        <Route path="/401" element={<Unauthorized />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App