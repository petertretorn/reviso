import React from 'react';
import Moment from 'react-moment';
import { 
    Modal,
    ModalHeader,
    ModalBody } from 'reactstrap';

function InvoiceModal(props) {

    return (
        <Modal isOpen={props.viewInvoice} toggle={props.toggleInvoice}>
            <ModalHeader toggle={props.toggleInvoice}>Invoice for {props.invoice.customer}</ModalHeader>
            <ModalBody>
                <h5>Project: {props.invoice.project}</h5>
                <p>Net Ammount: {props.invoice.net}</p>
                <p>Vat: {props.invoice.vat}</p>
                <p>Total: {parseFloat(props.invoice.vat) + parseFloat(props.invoice.net)}</p>
                <p>Invoiced on <Moment format="D MMM YYYY">{props.invoice.invoiceDate}</Moment></p>
            </ModalBody>
        </Modal>
        )
}

export default InvoiceModal

