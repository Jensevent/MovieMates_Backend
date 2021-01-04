import React, { Component } from 'react';
import { Card, Button, Form } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import './loginCardStyle.css';
import '../../SharedStyle/main.css';

import axios from 'axios';
import Cookies from 'js-cookie';

class loginComponent extends Component {
    state = {
        username: "",
        password: "",
        errorMessage: ""
    }


    validateForm = () => {
        if (this.state.username.length > 0 && this.state.password.length > 0) {
            return true;
        }
        return false;
    }


    componentDidMount = () => {
        if (Cookies.get('Username') !== undefined) {
            window.location.replace("/home");
        }
    }


    LoginCall = (e: any) => {
        e.preventDefault();
        const formData = new FormData();
        formData.append('username', this.state.username.toString());
        formData.append('password', this.state.password.toString());

        axios({
            method: 'post',
            url: "api/user/login",
            data: formData
        })
            .then(() => {
                console.log("gelukt!");
                Cookies.set('Username', this.state.username.toString());
                window.location.replace('/home');
            })
            .catch(error => {
                if(error.response === undefined){
                    this.setState({ errorMessage: "Something went wrong... please contact us." });
                    console.log(error);
                }
                else{
                    this.setState({ errorMessage: error.response.data });
                }
               
            });
    }


    render() {
        return (
            <>   
                <Card className="login-card vertical-center" border="dark" >
                    <Card.Img variant="top" src="/Images/popcornlogotext.png" />
                    <Card.Body>
                        <Card.Title className="error"> {this.state.errorMessage.toString()}</Card.Title> 
                        <Card.Title>Login</Card.Title>
                        <Form onSubmit={(e) => this.LoginCall(e)} >
                            <Form.Group>
                                <Form.Label>Username</Form.Label>
                                <Form.Control
                                    id="username"
                                    autoFocus
                                    type="text"
                                    placeholder="Username..."
                                    autoComplete="on"
                                    value={this.state.username.toString()}
                                    onChange={(e) => this.setState({ username: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group controlId="password">
                                <Form.Label>Password</Form.Label>
                                <Form.Control
                                    id="password"
                                    type="password"
                                    autoComplete="on"
                                    placeholder="Password..."
                                    value={this.state.password.toString()}
                                    onChange={(e) => this.setState({ password: e.target.value })}
                                />
                            </Form.Group>
                            <Button type="submit" id="login" disabled={!this.validateForm()} >Login </Button>
                        </Form>
                        <p>Become a member <button style={{outline:"none"}} className="href" onClick={() =>window.location.replace("/register")}>here. </button></p>
                    </Card.Body>
                </Card>
            </>
        );
    }
}

export default loginComponent;