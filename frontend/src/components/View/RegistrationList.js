import React from 'react'
import PropTypes from 'prop-types'
import Moment from 'react-moment';
import { 
    ListGroup, 
    ListGroupItem,
    Button } from 'reactstrap';

function RegistrationList(props) {
  return (
    <ListGroup>
        {props.children}
        
        {props.project.registrations.map( ({ date, hours, id }, index) => (
            <ListGroupItem key={index}>
                <Moment format="D MMM YYYY">{date}</Moment>: {hours} hours
                <Button
                    disabled={!props.isInvoiceable}
                    className='remove-btn float-right'
                    color='danger'
                    size='sm'
                    onClick={() => props.deleteRegistration(id)}
                    >
                    &times;
                </Button>
            </ListGroupItem>
        ))}
    </ListGroup>
  )
}

RegistrationList.propTypes = {
    project: PropTypes.object.isRequired,
    isInvoiceable: PropTypes.bool.isRequired,
    deleteRegistration: PropTypes.func.isRequired,
}

export default RegistrationList

