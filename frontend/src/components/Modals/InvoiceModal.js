import React from 'react';
import Moment from 'react-moment';

import Modal from './ReactstrapModal'

function InvoiceModal(props) {

    const headerText = `Invoice for ${props.invoice.customer}`;

    return (
        <Modal isOpen={props.viewInvoice} toggle={props.toggleInvoice} header={headerText}>
            <h5>Project: {props.invoice.project}</h5>
            <p>Net Ammount: {props.invoice.net}</p>
            <p>Vat: {props.invoice.vat}</p>
            <p>Total: {parseFloat(props.invoice.vat) + parseFloat(props.invoice.net)}</p>
            <p>Invoiced on <Moment format="D MMM YYYY">{props.invoice.invoiceDate}</Moment></p>
        </Modal>
    )
}

export default InvoiceModal

