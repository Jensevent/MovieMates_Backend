import React, { Component } from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import Navbar from '../../Components/Navbar/navbarComponent';
import { Button, Col, Container, Form, Row, Card } from 'react-bootstrap';
import Axios from 'axios';
import Cookies from 'js-cookie';
import { iMovie } from '../../Interfaces/iMovie';
import GroupCard from '../../Components/GroupCard/groupCard';
import './groupPageStyle.css';
import { iGroup } from '../../Interfaces/iGroup';




class groupPage extends Component {
    state = {
        movies: Array<iMovie>(),
        userGroups: Array<iGroup>(),
        groupID: 0,
        joinGroupID: 0,
        message: ""
    }

    componentDidMount() {
        this.GetUserGroups();
    }

    GetUserGroups(){
        Axios.get(`api/user/${Cookies.get("UserID")}/groups`).then(response => {
            this.setState({ userGroups: response.data });
        })
    }

    returnCard(mymovies: Array<iMovie>) {
        if (mymovies.length !== 0) {
            return (<GroupCard movies={this.state.movies} />);
        }
        else {
            return (<Card className="groupCard"> </Card>)
        }


    }

    SetGroup(value: string) {
        this.setState({ groupID: value }, () => {

            if (this.state.groupID !== 0) {
                Axios.get(`api/group/${this.state.groupID}/movies`).then(response => {
                    this.setState({ movies: response.data });
                });
            };
        });
    }

    SetJoinID(value: string) {
        if (value.length <= 6) {
            this.setState({ joinGroupID: value })
        }
        console.log(this.state.joinGroupID);
    }

    JoinGroup(e: any) {
        e.preventDefault();

        Axios.patch(`group/${this.state.joinGroupID}/add/${Cookies.get("UserID")}`).then(response => {
            this.setState({ message: response.data }, () => this.GetUserGroups()   );

        }).catch(error => {
            this.setState({ message: error.response.data });
        })
    }


    render() {
        let groups;
        groups = this.state.userGroups.map(g => {
            return (
                <option value={g.id}>{g.name} ({g.joinID})</option>
            )
        });


        return (
            <>
                <Navbar />
                <Container className="container">
                    <Row>
                        <Col>
                            <Form onSubmit={(e) => this.JoinGroup(e)}>
                                <Form.Group>
                                    <Form.Label className="grouptitle">Join Group:</Form.Label>
                                    <Form.Label className="message"> {this.state.message} </Form.Label>
                                    <Form.Control type="number" value={this.state.joinGroupID} onChange={(e) => this.SetJoinID(e.target.value)} />
                                </Form.Group>
                                <Button type="submit"> Join</Button>
                            </Form>
                            <hr />
                            <Form>
                                <Form.Group>
                                    <Form.Label className="grouptitle">Choose group</Form.Label>
                                    <Form.Control as="select" onChange={(e) => this.SetGroup(e.target.value)} >
                                        <option> Choose a group... </option>
                                        {groups}
                                    </Form.Control>
                                </Form.Group>
                            </Form>
                        </Col>
                        <Col>
                            {this.returnCard(this.state.movies)}
                        </Col>
                    </Row>
                </Container>
            </>
        );
    }

}

export default groupPage;