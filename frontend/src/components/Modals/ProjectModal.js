import React, { Fragment } from 'react';
import PropTypes from 'prop-types'
import {
  Button,
  Form,
  FormGroup,
  Label,
  Input
} from 'reactstrap';

import Modal from '../UI/ReactstrapModal'

function ProjectModal(props) {

    return (
        <Fragment>
            <Button
                color='success'
                onClick={props.toggleModal}
                >New Project
            </Button>
            <Modal isOpen={props.isOpen} toggle={props.toggleModal} header='Create Project'>
                <Form onSubmit={props.submit}>
                    <FormGroup>
                        <Label for="project">Name</Label>
                        <Input
                            type="text"
                            name="project"
                            id="project"
                            onChange={props.onFormInputChange}
                        />
                    </FormGroup>
                    <FormGroup>
                        <Label for="customer">Customer</Label>
                        <Input
                            type="text"
                            name="customer"
                            id="customer"
                            onChange={props.onFormInputChange}
                        />
                    </FormGroup>
                    <FormGroup>
                        <Label for="start">Project Start</Label>
                        <Input
                            type="date"
                            name="start"
                            id="start"
                            onChange={props.onFormInputChange}
                        />
                    </FormGroup>
                    <FormGroup>
                        <Label for='baseRate'>Hourly Base Rate</Label>
                        <Input
                            type='decimal'
                            name='baseRate'
                            id='baseRate'
                            onChange={props.onFormInputChange}
                        />
                    </FormGroup>
                    <FormGroup>
                        <Label for='vatRate'>Vat Rate</Label>
                        <Input
                            type='decimal'
                            name='vatRate'
                            id='vatRate'
                            onChange={props.onFormInputChange}
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
            </Modal>
        </Fragment>
    );
}

ProjectModal.propTypes = {
    toggleModal: PropTypes.func.isRequired,
    onFormInputChange: PropTypes.func.isRequired,
    submit: PropTypes.func.isRequired
}

export default ProjectModal;