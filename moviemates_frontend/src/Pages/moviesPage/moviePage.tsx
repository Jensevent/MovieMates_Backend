import React, { Component } from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import Navbar from '../../Components/Navbar/navbarComponent';
import './movieStyle.css';
import MyMovieComponent from '../../Components/MyMovies/myMoviesComponent';
import { Col, Container, Row } from 'react-bootstrap';




class moviePage extends Component {
    test() {
        document.querySelector(".hid-box")?.classList.toggle("newbox");
    }


    render() {
        return (
            <>
                <Navbar />
                <Container>
                    <Row>
                        <Col>
                            <MyMovieComponent />
                        </Col>
                    </Row>
                </Container>
            </>
        );
    }

}

export default moviePage;