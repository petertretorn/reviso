import React, { Component, Fragment } from 'react';
import {
  Button,
  Form,
  FormGroup,
  Label,
  Input
} from 'reactstrap';

import Modal from '../UI/ReactstrapModal'

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

        const header = `Log Time for ${this.props.chosenProject.projectName}`;

        return (
            <Fragment>
                <Button
                    color='dark'
                    onClick={this.toggleRegistrationModal}
                    disabled={!this.props.isInvoiceable}
                    >
                    {this.props.buttonText}
                </Button>

                <Modal isOpen={this.state.modalOpen} toggle={this.toggleRegistrationModal} header={header}>
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
                                disabled={!this.props.isInvoiceable}
                                color='dark'
                                style={{ marginTop: '2rem' }} block>
                                {this.props.buttonText}
                            </Button>
                        </FormGroup>
                    </Form>
                </Modal>
            </Fragment>
        )
    }
}

export default RegistrationModal;