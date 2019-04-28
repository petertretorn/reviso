import React, { Component, Fragment} from 'react';
import Moment from 'react-moment';
import axios from 'axios';
import { 
    Container, 
    Dropdown, 
    DropdownMenu, 
    DropdownToggle, 
    DropdownItem, 
    ListGroup, 
    ListGroupItem,
    Modal,
    ModalHeader,
    ModalBody,
    Form,
    FormGroup,
    Label,
    Input,
    Button } from 'reactstrap';

class Registrations extends Component {
    state = {
        projects: [],
        chosenProject: null,
        dropdownOpen: false,
        modalOpen: false
    }
    
    componentDidMount() {
        this.loadProjects();
    }

    async loadProjects() {
        return axios.get('api/projects').then(response => {
            this.setState({
                projects: response.data
              });
        });
    }

    toggleDropdown = () => {
        this.setState(state => ({
          dropdownOpen: !state.dropdownOpen
        }));
    }

    toggleModal = () => {
        this.setState({
            modalOpen: !this.state.modalOpen
        });
    }
    
    onChange = e => {
        this.setState({ [e.target.name]: e.target.value });
    };
    
    onSubmit = async (e) => {
        e.preventDefault();
    
        const projectId = this.state.chosenProject.id;

        const newRegistration = {
          hours: this.state.hours,
          date: this.state.date,
          projectId
        };
    
        await axios.post(`api/projects/${projectId}/registrations/`, newRegistration);
        await this.loadProjects();
        
        this.chooseProject(projectId);
        this.toggleModal();
    };
    
    chooseProject(id) {
        const chosenProject = this.state.projects.find(p => p.id === id);
        chosenProject.registrations = chosenProject.registrations.sort( (r1, r2) => new Date(r1.date) - new Date(r2.date) );

        this.setState(state => ({
            chosenProject,
        }))
    }

    deleteRegistration = async (registrationId) => {
        const projectId = this.state.chosenProject.id;
        const url  = `api/projects/${projectId}/registrations/${registrationId}`;
        
        await axios.delete(url);
        
        const filterFn = r => r.id  !== registrationId;

        const chosenProject = this.state.chosenProject;
        chosenProject.registrations = chosenProject.registrations.filter(filterFn);

        this.setState(state => ({
            projects: [...state.projects.filter(p => p.id !== projectId), chosenProject],
            chosenProject
        }));
    }

    render() {
        const { projects, chosenProject } = this.state;

        return (
            <Fragment>
                <Container className="mt-3">
                    <Dropdown className="mb-3" isOpen={this.state.dropdownOpen} toggle={this.toggleDropdown}>
                        <DropdownToggle caret>
                        Choose Project
                        </DropdownToggle>
                        <DropdownMenu>
                            {projects.map( ({name, id}) => (
                                <DropdownItem key={id} onClick={this.chooseProject.bind(this, id)}>
                                    {name}
                                </DropdownItem>        
                            ))}
                        
                        </DropdownMenu>
                    </Dropdown>
                    
                    {chosenProject && (
                        <Fragment>
                            <h3>Time Registrations for {chosenProject.name}</h3>
                            <ListGroup>
                                <Button
                                    color='dark'
                                    style={{ marginBottom: '2rem' }}
                                    onClick={this.toggleModal}
                                    >
                                    Add New Registration
                                </Button>
                                {chosenProject.registrations.map( ({ date, hours, id }, index) => (
                                    <ListGroupItem key={index}>
                                        <Moment format="YYYY/MM/DD">{date}</Moment>: {hours} hours
                                        <Button
                                            className='remove-btn float-right'
                                            color='danger'
                                            size='sm'
                                            onClick={() => this.deleteRegistration(id)}
                                            >
                                            &times;
                                        </Button>
                                    </ListGroupItem>
                                ))}
                            </ListGroup>
                        </Fragment>
                    )}
                </Container>

                <Modal isOpen={this.state.modalOpen} toggle={this.toggleModal}>
                    <ModalHeader toggle={this.toggleModal}>Add Time Registration</ModalHeader>
                    <ModalBody>
                        <Form onSubmit={this.onSubmit}>
                            <FormGroup>
                                <Label for="date">Date</Label>
                                <Input
                                    type="date"
                                    name="date"
                                    id="date"
                                    placeholder="choose date"
                                    onChange={this.onChange}
                                />
                            </FormGroup>
                            <FormGroup>
                                <Label for='hours'>Hours</Label>
                                <Input
                                    type='number'
                                    name='hours'
                                    id='hours'
                                    placeholder='hours to be invoiced'
                                    onChange={this.onChange}
                                />
                                <Button color='dark' style={{ marginTop: '2rem' }} block>
                                    Add Registration
                                </Button>
                            </FormGroup>
                        </Form>
                    </ModalBody>
                </Modal>
            </Fragment>
        )
    }
}

export default Registrations;
