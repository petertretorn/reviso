import React, { Component } from 'react';
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
    Row, Col,
    Button } from 'reactstrap';

import ProjectCard from '../View/ProjectCard';
import ProjectModal from '../Modals/ProjectModal';
import RegistrationModal from '../Modals/RegistrationModal';
import InvoiceModal from '../Modals/InvoiceModal';

class Dashboard extends Component {
    state = {
        projects: [],
        chosenProject: null,
        invoice: null,
        dropdownOpen: false,
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

    toggleInvoice = () => {
        console.log('toggleInvoice')
        this.setState(state => ({
            viewInvoice: !state.viewInvoice
        }));
    }

    submitRegistration = async (newRegistration) => {
        const projectId = this.state.chosenProject.id;

        await axios.post(`api/projects/${projectId}/registrations/`, newRegistration);
        await this.loadProjects();
        
        this.chooseProject(projectId);
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
                    <ListGroup>
                        <RegistrationModal 
                            invoiceble={invoiceble} 
                            buttonText={registrationButtonText}
                            chosenProject={chosenProject}
                            submitRegistration={this.submitRegistration}>
                        </RegistrationModal>
                        
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
                )}

                {!!this.state.invoice && (
                <InvoiceModal 
                    invoice={this.state.invoice} 
                    viewInvoice={this.state.viewInvoice}
                    toggleInvoice={this.toggleInvoice}>
                </InvoiceModal>
            )}
            </Container>
        )
    }
}

export default Dashboard;
