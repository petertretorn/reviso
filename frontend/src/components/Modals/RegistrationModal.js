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

class RegistrationModal extends Component {
    state = {
        modalOpen: false
    }

    toggleRegistrationModal = () => {
        this.setState({
            modalOpen: !this.state.modalOpen
        });
    } 

    onFormInputChange = e => {
        this.setState({ [e.target.name]: e.target.value });
    };

    submit = async (e) => {
        e.preventDefault();
    
        const projectId = this.props.chosenProject.id;

        const newRegistration = {
          hours: this.state.hours,
          date: this.state.date,
          projectId
        };
    
        this.props.submitRegistration(newRegistration);
        this.toggleRegistrationModal();
    };

    render() {
        return (
            <Fragment>
                <Button
                    color='dark'
                    onClick={this.toggleRegistrationModal}
                    disabled={!this.props.invoiceble}
                    >
                    {this.props.buttonText}
                </Button>
                <Modal isOpen={this.state.modalOpen} toggle={this.toggleRegistrationModal}>
                    <ModalHeader toggle={this.toggleRegistrationModal}>Log Time for {this.props.chosenProject.projectName}</ModalHeader>
                    <ModalBody>
                        <Form onSubmit={this.submit}>
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
                                    disabled={!this.props.invoiceble}
                                    color='dark'
                                    style={{ marginTop: '2rem' }} block>
                                    {this.props.buttonText}
                                </Button>
                            </FormGroup>
                        </Form>
                    </ModalBody>
                </Modal>
            </Fragment>
        );
    }
}

export default RegistrationModal;