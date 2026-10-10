import {Link, NavLink} from "react-router-dom";
// Navegar para pagina selecionada7
function NavBar() {
    return(
        <nav aria-label="Main navigation">
            <NavLink to="/loginPage" className="nav-link">Login</NavLink>   
            <NavLink to="/RegisterPage" className="nav-link">Register</NavLink>
        </nav>
    );
}
export default NavBar;