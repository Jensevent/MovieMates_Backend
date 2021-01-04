import React, { Component } from 'react';
import { Card, Button, Form } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import './registerCardStyle.css';
import axios from 'axios';
import Cookies from 'js-cookie';

class registerComponent extends Component {
    state = {
        username: "",
        email: "",
        passwordSalt: "",
        passwordHash: "",
        errorMessage: ""
    }


    validateForm = () => {
        if (this.state.username.length > 0 && this.state.passwordSalt.length > 0 && this.state.passwordHash.length > 0 && this.state.email.length > 0) {
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
        if (this.state.email !== "") {
            formData.append('email', this.state.email.toString());
        }

        formData.append('passwordHash', this.state.passwordHash.toString());
        formData.append('passwordSalt', this.state.passwordHash.toString());

        axios({
            method: 'post',
            url: "api/user/register",
            data: formData
        })
            .then(() => {
                console.log("gelukt!");
                Cookies.set('Username', this.state.username.toString());
                window.location.replace('/home');
            })
            .catch(error => {
                if(error.response === undefined){
                    this.setState({ errorMessage: "Something went wrong... please contact us" });
                }
                else{
                    this.setState({ errorMessage: error.response.data });
                };
            });
    }


    render() {
        return (
            <>
                <Card className="register-card vertical-center" border="dark" >
                    <Card.Img variant="top" src="/Images/popcornlogotext.png" />
                    <Card.Body>
                        <Card.Title className="error"> {this.state.errorMessage.toString()}</Card.Title>
                        <Card.Title>Login<span className="info">* is required</span>   </Card.Title>
                        
                        <Form onSubmit={(e) => this.LoginCall(e)} >
                            <Form.Group>
                                <Form.Label>Username*</Form.Label>
                                <Form.Control
                                    autoFocus
                                    type="text"
                                    autoComplete="on"
                                    placeholder="Username..."
                                    value={this.state.username.toString()}
                                    onChange={(e) => this.setState({ username: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group>
                                <Form.Label>Email*</Form.Label>
                                <Form.Control
                                    type="email"
                                    autoComplete="on"
                                    placeholder="Email..."
                                    value={this.state.email.toString()}
                                    onChange={(e) => this.setState({ email: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group controlId="password">
                                <Form.Label>Password*</Form.Label>
                                <Form.Control
                                    type="password"
                                    placeholder="Password..."
                                    value={this.state.passwordSalt.toString()}
                                    onChange={(e) => this.setState({ passwordSalt: e.target.value })}
                                />

                            </Form.Group>
                            <Form.Group>
                                <Form.Label>Repeat password*</Form.Label>
                                <Form.Control
                                    type="password"
                                    placeholder="Password..."
                                    value={this.state.passwordHash.toString()}
                                    onChange={(e) => this.setState({ passwordHash: e.target.value })}
                                />
                            </Form.Group>
                            <Button type="submit" disabled={!this.validateForm()} >
                                Register
                        </Button>
                        </Form>
                        <p>Already a member? <button className="href" onClick={() => window.location.replace("/")}>log in here.</button></p>
                    </Card.Body>
                </Card>
            </>
        );
    }
}

export default registerComponent;