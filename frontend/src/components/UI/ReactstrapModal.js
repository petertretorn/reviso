import React from 'react'
import PropTypes from 'prop-types'
import { 
    Modal,
    ModalHeader,
    ModalBody } from 'reactstrap';

function ReactstrapModal(props) {
  return (
    <Modal isOpen={props.isOpen} toggle={props.toggle}>
        <ModalHeader toggle={props.toggle}>{props.header}</ModalHeader>
        <ModalBody>
            {props.children}
        </ModalBody>
    </Modal>
  )
}

ReactstrapModal.propTypes = {

}

export default ReactstrapModal

