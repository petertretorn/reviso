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
        <CardTitle><h5>{props.project.projectName}</h5></CardTitle>
        <CardText>
            Started on <Moment format="D MMM YYYY">{props.project.start}</Moment> for customer {props.project.customer}
        </CardText>
        <Button 
            color='secondary'
            onClick={props.buttonFn}>
            {props.buttonText}
        </Button>
    </Card>
  )
}

ProjectCard.propTypes = {
    project: PropTypes.object.isRequired,
    buttonFn: PropTypes.func.isRequired,
    buttonText: PropTypes.string.isRequired
}

export default ProjectCard

