import React, { Component } from 'react';
import { Navbar, Nav, Button } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import './navbarStyle.css';
import Cookies from 'js-cookie';


class navbarComponent extends Component {
    
    logout = () =>{
        Cookies.remove('Username');
        Cookies.remove('UserID');
        window.location.replace('/');
    }
    
    
    render() {      
        return (
            <>
                <Navbar collapseOnSelect className="navbar" expand="sm" variant="dark" >
                    <Navbar.Brand onClick={()=>window.location.replace('/home')}>
                        <img src="/Images/popcornlogo.png" height="50px" alt="logo" ></img>
                    </Navbar.Brand>
                    <Navbar.Brand onClick={()=>window.location.replace('/home')}>MovieMates</Navbar.Brand>

                    <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="mr-auto">
                            <Nav.Link onClick={() => window.location.replace('/movies')}>My Movies</Nav.Link>
                            <Nav.Link onClick={() => window.location.replace('/groups')}>Groups</Nav.Link> 
                        </Nav>
                        <Nav>
                            <Nav.Link disabled className="welcome"> Welcome, {Cookies.get("Username")}   </Nav.Link>
                            <Button onClick={() => this.logout()} variant="outline-light">Logout</Button>
                        </Nav>
                    </Navbar.Collapse>
                </Navbar>
            </>
        );
    }
}

export default navbarComponent;