import React, { Component } from 'react';
import { Card, Button, Image, Spinner } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import './movieCardStyle.css';
import '../../SharedStyle/card.css';
import '../../SharedStyle/main.css';
import Axios from 'axios';
import { iMovie } from '../../Interfaces/iMovie';
import Cookies from 'js-cookie';


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

    LikeClick(){
        let movieID = this.props.movies[this.state.currentMovie].id;

        Axios.patch(`api/user/likes/${Cookies.get("UserID")}/${movieID}`).then( () =>{
            alert("The movie has been added!");

        }).catch(error => {
            alert(error.response.data + "(" + this.props.movies[this.state.currentMovie].title + ")");

        }).finally(() => {
            this.IncreaseCount();
        })


        
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

            }).catch(error => {
                url = "https://www.antagonist.nl/blog/wp-content/uploads/2015/04/wordpress_errors.jpg";
                this.setState({ url: url });
            });

        return url;
    }

    loadEverything() {
        if (this.state.currentMovie >= this.props.movies.length) {
            this.setState({ currentMovie: 0 });
            return (<Spinner animation="border" />)
        }
        else if(this.props.movies.length === 0){
            return(<h1>There are no movies with this genre!</h1>)
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
                             <div className="movieTitle">
                                 {this.props.movies[this.state.currentMovie].title}<span className="runtime"> {this.props.movies[this.state.currentMovie].runTime} </span>
                                 </div>   
                                    
                                
                                <div> {genres}</div>
                            </Card.Title>
                            <Image className="poster" src={this.state.url} alt={"Poster for the movie " +this.props.movies[this.state.currentMovie].title } />
                            <div className="move transform">
                                <div className="scroll">
                                    <div className="rating"> {this.props.movies[this.state.currentMovie].imDb} </div>
                                    <strong>Description:</strong> <br />
                                    <div className="description">{this.props.movies[this.state.currentMovie].description} </div>
                                </div>
                                <br />
                                <strong>Release date:</strong> <br />  { new Date(this.props.movies[this.state.currentMovie].releaseYear).toLocaleDateString()}
                            </div>
                        </Card.Body>
                        <div className="row">
                            <Button className="LikeButton" variant="success" onClick={() => this.LikeClick()}> ✔</Button>
                            <Button className="InfoButton" onClick={() => this.InfoClick()}> ⓘ</Button>
                            <Button className="DislikeButton" variant="danger" onClick={() => this.IncreaseCount()}> ✖</Button>
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