import React from 'react';
import './App.css';
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import LoginPage from './Pages/loginPage/loginPage';
import HomePage from './Pages/homepage/homePage';
import RegisterPage from './Pages/registerPage/registerPage';
import MoviePage from './Pages/moviesPage/moviePage';
import groupPage from './Pages/groupPage/groupPage';
import axios from "axios";

axios.defaults.baseURL="http://localhost:5000/";
//axios.defaults.baseURL="https://localhost:44356/";

function App() {
  return (
    <Router >
      <Switch>
        <Route exact path="/" component={LoginPage} />
        <Route exact path="/register" component={RegisterPage} />
        <Route exact path="/home" component={HomePage} />
        <Route exact path="/movies" component={MoviePage} />
        <Route exact path="/groups" component={groupPage} />
      </Switch>
    </Router>
  );
}

export default App;
