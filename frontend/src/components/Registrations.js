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
    Card,
    CardTitle,
    CardText,
    Row, Col,
    Button } from 'reactstrap';

class Registrations extends Component {
    state = {
        projects: [],
        chosenProject: null,
        invoice: null,
        dropdownOpen: false,
        modalOpen: false,
        viewInvoice: false
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

    toggleInvoice = () => {
        this.setState(state => ({
            viewInvoice: !state.viewInvoice
        }));
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
        }));
    }

    invoiceProject() {
        const projectId = this.state.chosenProject.id;

        axios.post(`api/projects/${projectId}/invoice`).then(response => {
            this.setState(state => ({
                invoice: response.data
            }));

            this.toggleInvoice();
        });
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
                            {projects.map( ({projectName, id}) => (
                                <DropdownItem key={id} onClick={this.chooseProject.bind(this, id)}>
                                    {projectName}
                                </DropdownItem>        
                            ))}
                        
                        </DropdownMenu>
                    </Dropdown>
                    
                    {chosenProject && (
                        <Fragment>
                            <Row>
                                <Col sm="6">
                                    <Card body outline color="primary" className="mb-3 sm-6">
                                        <CardTitle>{chosenProject.projectName}</CardTitle>
                                        <CardText>
                                            Started on <Moment format="D MMM YYYY">{chosenProject.start}</Moment> for customer {chosenProject.customer}
                                        </CardText>
                                        <Button 
                                            color='success'
                                            onClick={() => this.invoiceProject()}
                                            >Close and Invoice Project</Button>
                                    </Card>
                                </Col>
                            </Row>
                            
                            <ListGroup>
                                <Button
                                    color='dark'
                                    onClick={this.toggleModal}
                                    >
                                    Add New Registration
                                </Button>
                                {chosenProject.registrations.map( ({ date, hours, id }, index) => (
                                    <ListGroupItem key={index}>
                                        <Moment format="D MMM YYYY">{date}</Moment>: {hours} hours
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

                {this.state.invoice && (
                    <Modal isOpen={this.state.viewInvoice} toggle={this.toggleInvoice}>
                        <ModalHeader toggle={this.toggleInvoice}>Invoice for {this.state.invoice.customer}</ModalHeader>
                        <ModalBody>
                            <h4>Project: {this.state.invoice.project}</h4>
                            <p>Net Ammount: {this.state.invoice.net}</p>
                            <p>Vat: {this.state.invoice.vat}</p>
                            <p>Total: {parseFloat(this.state.invoice.vat) + parseFloat(this.state.invoice.net)}</p>
                            <p>Invoiced on <Moment format="D MMM YYYY">{this.state.invoiceDate}</Moment></p>
                        </ModalBody>
                    </Modal>
                )}
                
            </Fragment>
        )
    }
}

export default Registrations;
