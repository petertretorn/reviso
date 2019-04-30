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
    Row, Col,
    Button } from 'reactstrap';
import ProjectModal from './ProjectModal';
import ProjectCard from './ProjectCard'

class Registrations extends Component {
    state = {
        projects: [],
        chosenProject: null,
        invoice: null,
        dropdownOpen: false,
        modalOpen: false,
        viewInvoice: false,
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

    toggleRegistrationModal = () => {
        this.setState({
            modalOpen: !this.state.modalOpen
        });
    } 

    toggleInvoice = () => {
        this.setState(state => ({
            viewInvoice: !state.viewInvoice
        }));
    }

    onFormInputChange = e => {
        this.setState({ [e.target.name]: e.target.value });
    };
    
    submitRegistration = async (e) => {
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
        this.toggleRegistrationModal();
    };

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

    submitProject = (newProject) => {
        axios.post('api/projects', newProject).then(response => {
            
            this.setState(state => ({
                projects: [...state.projects, response.data]
            }));
            
            this.chooseProject(response.data.id);
        });
    };
    
    invoiceProject() {
        const projectId = this.state.chosenProject.id;
        const project = this.state.chosenProject;

        axios.post(`api/projects/${projectId}/invoice`).then(response => {
            project.invoice = response.data;
            project.isActive = false;

            this.setState(state => ({
                invoice: response.data,
                chosenProject: project
            }));

            this.toggleInvoice();
        });
    }

    viewInvoice() {
        this.setState(state => ({
            invoice: this.state.chosenProject.invoice
        }), this.toggleInvoice() );
    }

    chooseProject(id) {
        const chosenProject = this.state.projects.find(p => p.id === id);
        
        chosenProject.registrations = chosenProject.registrations || [];
        chosenProject.registrations = chosenProject.registrations.sort( (r1, r2) => new Date(r1.date) - new Date(r2.date) );

        this.setState(state => ({
            chosenProject,
        }));
    }

    render() {
        const { projects, chosenProject } = this.state;
        const invoiceble = (!!chosenProject && chosenProject.isActive)
        const invoiceButtonText = (invoiceble)
            ? 'Close and Invoice Project' 
            : 'View Created Invoice';

        const registrationButtonText = (invoiceble)
            ? 'Add New Registration' 
            : 'Project closed for registrations';

        const invoiceButtonFn = (invoiceble)
            ? () => this.invoiceProject()
            : () => this.viewInvoice()

        return (
            <Fragment>
                <Container className="mt-3">
                    <Row>
                        <Col md="6" sm="12">
                            <Dropdown className="mb-3 mr-3 float-left" isOpen={this.state.dropdownOpen} toggle={this.toggleDropdown}>
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
                            <ProjectModal submitProject={this.submitProject}/>
                        </Col>

                        <Col sm="12" md="6">
                            {chosenProject && (
                                <ProjectCard 
                                    chosenProject={chosenProject}
                                    invoiceButtonFn={invoiceButtonFn}
                                    invoiceButtonText={invoiceButtonText}
                                /> 
                            )}
                        </Col>
                    </Row>

                    {chosenProject && (
                        <Fragment>
                            <ListGroup>
                                <Button
                                    color='dark'
                                    onClick={this.toggleRegistrationModal}
                                    disabled={!invoiceble}
                                    >
                                    {registrationButtonText}
                                </Button>
                                {chosenProject.registrations.map( ({ date, hours, id }, index) => (
                                    <ListGroupItem key={index}>
                                        <Moment format="D MMM YYYY">{date}</Moment>: {hours} hours
                                        <Button
                                            disabled={!invoiceble}
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

                {!!this.state.chosenProject && (
                    <Modal isOpen={this.state.modalOpen} toggle={this.toggleRegistrationModal}>
                    <ModalHeader toggle={this.toggleRegistrationModal}>Log Time for {this.state.chosenProject.projectName}</ModalHeader>
                    <ModalBody>
                        <Form onSubmit={this.submitRegistration}>
                            <FormGroup>
                                <Label for="date">Date</Label>
                                <Input
                                    type="date"
                                    name="date"
                                    id="date"
                                    placeholder="choose date"
                                    onChange={this.onFormInputChange}
                                />
                            </FormGroup>
                            <FormGroup>
                                <Label for='hours'>Hours</Label>
                                <Input
                                    type='number'
                                    name='hours'
                                    id='hours'
                                    placeholder='hours to be invoiced'
                                    onChange={this.onFormInputChange}
                                />
                                <Button 
                                    disabled={!invoiceble}
                                    color='dark'
                                    style={{ marginTop: '2rem' }} block>
                                    {registrationButtonText}
                                </Button>
                            </FormGroup>
                        </Form>
                    </ModalBody>
                    </Modal>
                )}
                
                {!!this.state.invoice && (
                    <Modal isOpen={this.state.viewInvoice} toggle={this.toggleInvoice}>
                        <ModalHeader toggle={this.toggleInvoice}>Invoice for {this.state.invoice.customer}</ModalHeader>
                        <ModalBody>
                            <h5>Project: {this.state.invoice.project}</h5>
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
