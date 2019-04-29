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
        viewInvoice: false,
        isAddingNewProject: false
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

    toggleAddProject = () => {
        this.setState(state => ({
            isAddingNewProject: !state.isAddingNewProject
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

    submitProject = async (e) => {
        e.preventDefault();
    
        const newProject = {
            project: this.state.project,
            customer: this.state.customer,
            start: this.state.start,
            baseRate: this.state.baseRate,
            vatRate: this.state.vatRate
        };

        const response = await axios.post('api/projects', newProject);
        
        this.setState(state => ({
            projects: [...state.projects, response.data]
        }));
        
        this.chooseProject(response.data.id);
        this.toggleAddProject();
    };
    
    invoiceProject() {
        const projectId = this.state.chosenProject.id;

        const project = this.state.chosenProject;
        project.isActive = false;

        axios.post(`api/projects/${projectId}/invoice`).then(response => {
            this.setState(state => ({
                invoice: response.data,
                chosenProject: project
            }));

            this.toggleInvoice();
        });
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
            : 'Project closed and invoiced';

        const registrationButtonText = (invoiceble)
            ? 'Add New Registration' 
            : 'Project closed for new registrations';

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
                            <Button
                                color='success'
                                onClick={this.toggleAddProject}
                                >New Project
                            </Button>
                        </Col>

                        <Col sm="12" md="6">
                        {chosenProject && (
                            <Card body outline color="primary" className="mb-3">
                                <CardTitle>{chosenProject.projectName}</CardTitle>
                                <CardText>
                                    Started on <Moment format="D MMM YYYY">{chosenProject.start}</Moment> for customer {chosenProject.customer}
                                </CardText>
                                <Button 
                                    color='secondary'
                                    disabled={!invoiceble}
                                    onClick={() => this.invoiceProject()}
                                    >{invoiceButtonText}</Button>
                            </Card>
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
                            <h4>Project: {this.state.invoice.project}</h4>
                            <p>Net Ammount: {this.state.invoice.net}</p>
                            <p>Vat: {this.state.invoice.vat}</p>
                            <p>Total: {parseFloat(this.state.invoice.vat) + parseFloat(this.state.invoice.net)}</p>
                            <p>Invoiced on <Moment format="D MMM YYYY">{this.state.invoiceDate}</Moment></p>
                        </ModalBody>
                    </Modal>
                )}

                <Modal isOpen={this.state.isAddingNewProject} toggle={this.toggleAddProject}>
                <ModalHeader toggle={this.toggleAddProject}>Create Project</ModalHeader>
                <ModalBody>
                    <Form onSubmit={this.submitProject}>
                        <FormGroup>
                            <Label for="project">Name</Label>
                            <Input
                                type="text"
                                name="project"
                                id="project"
                                onChange={this.onFormInputChange}
                            />
                        </FormGroup>
                        <FormGroup>
                            <Label for="customer">Customer</Label>
                            <Input
                                type="text"
                                name="customer"
                                id="customer"
                                onChange={this.onFormInputChange}
                            />
                        </FormGroup>
                        <FormGroup>
                            <Label for="start">Project Start</Label>
                            <Input
                                type="date"
                                name="start"
                                id="start"
                                onChange={this.onFormInputChange}
                            />
                        </FormGroup>
                        <FormGroup>
                            <Label for='baseRate'>Hourly Base Rate</Label>
                            <Input
                                type='decimal'
                                name='baseRate'
                                id='baseRate'
                                onChange={this.onFormInputChange}
                            />
                        </FormGroup>
                        <FormGroup>
                            <Label for='vatRate'>Vat Rate</Label>
                            <Input
                                type='decimal'
                                name='vatRate'
                                id='vatRate'
                                onChange={this.onFormInputChange}
                            />
                        </FormGroup>
                        <FormGroup>
                            <Button 
                                color='dark'
                                style={{ marginTop: '2rem' }} block>
                                Submit
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
