import React from 'react'
import PropTypes from 'prop-types'
import Moment from 'react-moment';
import {
    Button,
    Card,
    CardTitle,
    CardText,
} from 'reactstrap';

function ProjectCard(props) {
  return (
    <Card body outline color="primary" className="mb-3">
        <CardTitle><h5>{props.chosenProject.projectName}</h5></CardTitle>
        <CardText>
            Started on <Moment format="D MMM YYYY">{props.chosenProject.start}</Moment> for customer {props.chosenProject.customer}
        </CardText>
        <Button 
            color='secondary'
            onClick={props.invoiceButtonFn}>
            {props.invoiceButtonText}
        </Button>
    </Card>
  )
}

ProjectCard.propTypes = {
    chosenProject: PropTypes.object.isRequired,
    invoiceButtonFn: PropTypes.func.isRequired,
    invoiceButtonText: PropTypes.string.isRequired
}

export default ProjectCard

