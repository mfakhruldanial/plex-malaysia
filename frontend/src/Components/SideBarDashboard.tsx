import { useEffect } from 'react';
import Accordion from 'react-bootstrap/Accordion';
import { Link, useLocation } from 'react-router-dom';

function SideBarDashboard() {
    const location = useLocation();

    useEffect(() => {
        const links = document.querySelectorAll('.nav-link');
        links.forEach(link => {
            if (link.getAttribute('href') === location.pathname) {
                link.classList.add('active');
            } else {
                link.classList.remove('active');
            }
        })
    }, [location.pathname])

    return (
        <Accordion>
            <Accordion.Item eventKey="0">
                <Accordion.Header>Access</Accordion.Header>
                <Accordion.Body className="p-0 my-1">
                    <ul className="nav nav-pills flex-column my-1">
                        <li className="nav-item">
                            <Link to={"/dashboard/user"} className="nav-link">
                                <i className="bi bi-people me-2"></i>
                                User List
                            </Link>
                        </li>
                        <li>
                            <Link to={"/dashboard/user/new-user"} className="nav-link">
                                <i className="bi bi-person-add me-2"></i>
                                New user
                            </Link>
                        </li>
                    </ul>
                </Accordion.Body>
            </Accordion.Item>
            <Accordion.Item eventKey="1">
                <Accordion.Header>Movies</Accordion.Header>
                <Accordion.Body className="p-0 my-1">
                    <ul className="nav nav-pills flex-column my-1">
                        <li className="nav-item">
                            <Link to={"/dashboard/movie"} className="nav-link">
                                <i className="bi bi-film me-2"></i>
                                Movie List
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link to={"/dashboard/movie/create-movie/0"} className="nav-link">
                                <i className="bi bi-camera-reels me-2"></i>
                                New movie
                            </Link>
                        </li>
                    </ul>
                </Accordion.Body>
            </Accordion.Item>
        </Accordion>
    );
}

export default SideBarDashboard;