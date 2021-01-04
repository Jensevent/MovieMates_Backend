import React, { Component } from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import { Button, Spinner, Table, ToggleButton } from 'react-bootstrap';
import { iMovie } from '../../Interfaces/iMovie';
import Axios from 'axios';
import Cookies from 'js-cookie';
import './myMoviesStyle.css';

interface iUserMovie{
    id : number,
    watched? : boolean,
    userRating? : number,
    movie : iMovie,
}



class myMovieComponent extends Component {
    state = {
        myMovies: Array<iUserMovie>(),
        message : "",
    }


    componentDidMount(){
       this.GetMovies();
    }

    GetMovies(){
         Axios.get(`api/user/movies/${Cookies.get("UserID")}`).then(response => { 
            this.setState({ myMovies: response.data })
            
            if(response.data.length === 0){
                this.setState({message : "You haven't saved any movies!" })
            }
            ;
        });
    }

    LoadButton(movieID : Number, watched? : boolean  ){
        if(watched === true){
            return(
                <Button disabled variant="success">Watched</Button>
            )
        }
        else{
            return(
                <ToggleButton type="radio" value={movieID.toString()} onChange={(e) => this.WatchMovie(Number(e.target.value))}>Watched?</ToggleButton>
            )
        }
    }

    WatchMovie(movieID : Number){
        Axios.patch(`api/user/watched/${Cookies.get("UserID")}/${movieID}/${true}`).then(() =>
        this.GetMovies());
    }
   
    LoadTable(movies: Array<iUserMovie>) {
        if(this.state.message !== ""){
            return (<h1>You have not liked any movies!</h1>);
        }
        else if (movies.length === 0) {
            return (<Spinner animation="border" />);
        }
        else {
            let myMovies;
            myMovies = this.state.myMovies.map(um =>{                
                return(
                    <tr>
                        <td> {um.movie.title} </td>
                        <td> {um.movie.description} </td>
                        <td> {this.LoadButton(um.movie.id,um.watched)} </td>
                    </tr>
                )
            });

            
            return (
                <Table>
                    <thead>
                        <tr>
                            <th>Title</th>
                            <th>Description</th>
                            <th>Watched</th>
                        </tr>
                    </thead>
                    <tbody>
                        {myMovies}
                    </tbody>
                </Table>
            )
        }
    }

    render() {
        return (
            this.LoadTable(this.state.myMovies)
        );
    }

}

export default myMovieComponent;