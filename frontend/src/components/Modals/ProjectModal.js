import React, { Component, Fragment } from 'react';
import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  Form,
  FormGroup,
  Label,
  Input
} from 'reactstrap';

class ProjectModal extends Component {
    state = {
        isAddingNewProject: false
    }

    toggleAddProject = () => {
        this.setState(state => ({
            isAddingNewProject: !state.isAddingNewProject
        }));
    }

    onFormInputChange = e => {
        this.setState({ [e.target.name]: e.target.value });
    };

    submit = (e) => {
        e.preventDefault();
    
        const newProject = {
            project: this.state.project,
            customer: this.state.customer,
            start: this.state.start,
            baseRate: this.state.baseRate,
            vatRate: this.state.vatRate
        };

        this.props.submitRegistration(newProject);

        this.toggleAddProject();
    };

    render() {
        return (
            <Fragment>
                <Button
                    color='success'
                    onClick={this.toggleAddProject}
                    >New Project
                </Button>
                <Modal isOpen={this.state.isAddingNewProject} toggle={this.toggleAddProject}>
                    <ModalHeader toggle={this.toggleAddProject}>Create Project</ModalHeader>
                    <ModalBody>
                        <Form onSubmit={this.submit}>
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
        );
    }
}

export default ProjectModal;