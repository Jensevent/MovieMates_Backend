import React, { Component } from 'react';
import { Card, Button, Image, Spinner } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import '../../SharedStyle/card.css';
import './groupCardStyle.css';
import Axios from 'axios';
import { iMovie } from '../../Interfaces/iMovie';


interface props {
    movies: Array<iMovie>
}


class movieCard extends Component<props> {
    state = {
        currentMovie: 0,
        url: "",
    }

    componentDidMount = () => {
        this.setState({ currentMovie: 0 });
    }

    componentDidUpdate(prevProps: props) {
        if (prevProps.movies.length !== this.props.movies.length) {
            this.setState({ currentMovie: 0 }, () => this.getPoster(this.props.movies[this.state.currentMovie].title));
        }
        else {
            this.getPoster(this.props.movies[this.state.currentMovie].title)
        };

    }

    IncreaseCount() {
        var y = Number(this.state.currentMovie) + Number(1);

        if (y < this.props.movies.length) {
            this.setState({ currentMovie: y, url: "" });
        }
        else {
            this.setState({ currentMovie: 0 });
        }
    }

    InfoClick() {
        document.querySelector(".transform")?.classList.toggle("transform-active");
    }


    getPoster = (movieTitle: String) => {
        var apiKey = "2a6089c778a42948fa1bf13817636858";
        let url;

        Axios.get("https://api.themoviedb.org/3/search/movie?api_key=" + apiKey + "&query=" + movieTitle.toString())
            .then(response => {
                url = "http://image.tmdb.org/t/p/w500/" + response.data.results[0].poster_path;

                if (this.state.url !== url && this.state.url !== "") {
                    this.setState({ url: url });
                }
                else if (this.state.url === "") {
                    this.setState({ url: url });
                }
                else {
                    return;
                }

            }).catch(() => {
                url = "/Images/error.png";
                this.setState({ url: url });
            });

        return url;
    }

    loadEverything() {
        if (this.state.currentMovie >= this.props.movies.length) {
            this.setState({ currentMovie: 0 });
            return (<Spinner animation="border" />)
        }
        else {

            let genres;
            genres = this.props.movies[this.state.currentMovie].movieGenres.map(m => {
                return (<span className="genre">{m.genreName}</span>);
            });


            return (
                <>
                    <Card className="custom-card" border="dark" >
                        <Card.Body className="body" >
                            <Card.Title className="title">
                                <div className="users"> {this.props.movies[this.state.currentMovie].amount} users like this movie </div>
                                {this.props.movies[this.state.currentMovie].title}
                                <span className="runtime"> {this.props.movies[this.state.currentMovie].runTime} </span>
                                <div> {genres}</div>

                            </Card.Title>
                            <Image className="poster" src={this.state.url} alt={"Poster for the movie " +this.props.movies[this.state.currentMovie].title } />
                            <div className="move transform">
                                <div className="Scroll">
                                    <div className="rating"> {this.props.movies[this.state.currentMovie].imDb} </div>
                                    <strong>Description:</strong> <br />
                                    <div className="description">{this.props.movies[this.state.currentMovie].description} </div>
                                </div>
                                <br />
                                <strong>Release date:</strong> <br />  { new Date(this.props.movies[this.state.currentMovie].releaseYear).toLocaleDateString()}
                            </div>



                        </Card.Body>
                        <div className="row">
                            <Button className="MoreInfoButton"  onClick={() => this.InfoClick()}>ⓘ</Button>
                            <Button className="NextButton" variant="success" onClick={() => this.IncreaseCount()}> ✖</Button>
                        </div>
                    </Card>
                </>
            );
        }
    }

    render() {
        return (
            this.loadEverything()
        )
    }
}

export default movieCard;