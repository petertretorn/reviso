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

function RegistrationModal(props) {
    const header = `Log Time for ${props.chosenProject.projectName}`;

    return (
        <Fragment>
            <Button
                color='dark'
                onClick={props.toggleModal}
                disabled={!props.isInvoiceable}
                >
                {props.buttonText}
            </Button>

            <Modal isOpen={props.isOpen} toggle={props.toggleModal} header={header}>
                <Form onSubmit={props.submit}>
                    <FormGroup>
                        <Label for="date">Date</Label>
                        <Input
                            type="date"
                            name="date"
                            id="date"
                            placeholder="choose date"
                            onChange={props.onFormInputChange}
                        />
                    </FormGroup>
                    <FormGroup>
                        <Label for='hours'>Hours</Label>
                        <Input
                            type='number'
                            name='hours'
                            id='hours'
                            placeholder='hours to be invoiced'
                            onChange={props.onFormInputChange}
                        />
                        <Button 
                            disabled={!props.isInvoiceable}
                            color='dark'
                            style={{ marginTop: '2rem' }} block>
                            {props.buttonText}
                        </Button>
                    </FormGroup>
                </Form>
            </Modal>
        </Fragment>
    )
}

RegistrationModal.propTypes = {
    toggleModal: PropTypes.func.isRequired,
    onFormInputChange: PropTypes.func.isRequired,
    submit: PropTypes.func.isRequired
}

export default RegistrationModal;