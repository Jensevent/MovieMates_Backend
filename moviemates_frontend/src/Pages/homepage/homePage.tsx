import React, { Component } from 'react';
import Navbar from '../../Components/Navbar/navbarComponent';
import 'bootstrap/dist/css/bootstrap.css';
import '../../SharedStyle/card.css';
import '../../SharedStyle/main.css';
import './homeStyle.css';
import MovieCard from '../../Components/MovieCard/movieCard';
import Cookies from 'js-cookie';
import { Form, Spinner, Container, Row, Col, Card, Image } from 'react-bootstrap';
import Axios from 'axios';
import { iMovie } from '../../Interfaces/iMovie';
import { iGenre } from '../../Interfaces/iGenre';


class homePage extends Component {
    state = {
        genres: Array<iGenre>(),
        genreID: Number,
        movies: Array<iMovie>(),
        error: ""
    }

    componentDidMount = () => {
        if (Cookies.get('Username') === undefined) {
            window.location.replace("/");
        }
        else if (Cookies.get("Username") !== undefined && Cookies.get("UserID") === undefined) {
            Axios.get(`api/user/find/${Cookies.get("Username")}`).then(response => {
                Cookies.set("UserID", response.data.id);
            })
        }


        Axios.get(`api/genre/all`).then(response => {
            this.setState({ genres: response.data, genreID: response.data[0].id }, () => this.getMovies(response.data[0].id));
        });


    }

    getMovies(genreID: Number) {
        Axios.get(`api/movie/filter/${genreID}`).then(response => {
            this.setState({ movies: response.data, error: "" });
        }).catch((error) => {
            this.setState({ error: error.response.data });
        });
    }

    myMovieCard(movies: any) {
        if (this.state.error !== "") {
            return (
                <Card className="custom-card">
                    <Card.Title className="title"> {this.state.error} </Card.Title>
                    <Card.Body className="body">
                        <Image src="/Images/error.png" className="img" alt="error 404" />
                    </Card.Body>
                </Card>
            )
        }
        else if (movies[0] === undefined) {
            return (<Spinner animation="border" />);
        }
        else {
            return (<MovieCard movies={this.state.movies} />);
        }
    }


    render() {
        let Genres;
        Genres = this.state.genres.map(g => {
            return (
                <option value={g.id.toString()}> {g.genreName} </option>
            )
        });


        return (
            <>
                <Navbar />
                <Container className="container">
                    <Row>
                        <Col md={4}>
                            <Form>
                                <Form.Label> Genre: </Form.Label>
                                <Form.Control as="select" onChange={(e) => this.getMovies(Number(e.target.value))} >
                                    {Genres}
                                </Form.Control>
                            </Form>
                        </Col>
                        <Col md={8}>
                            {this.myMovieCard(this.state.movies)}
                        </Col>
                    </Row>

                </Container>
            </>
        );
    }

}

export default homePage;