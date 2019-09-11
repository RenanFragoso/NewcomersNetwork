import React from 'react';
import { FormGroup, ControlLabel, FormControl, HelpBlock } from 'react-bootstrap';

class InputTextArea extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { id, label, type, input, placeholder, inline} = this.props;
        const { touched, error, warning, dirty } = this.props.meta;
        const validStation = ( touched && error ? "error" : null );
        
        const fieldContent =    <FormGroup controlId={id} validationState={validStation}>
                                    <ControlLabel>{label}</ControlLabel>
                                    <FormControl id={id} componentClass="textarea" placeholder={placeholder} {...input}/>
                                    <FormControl.Feedback />
                                    <HelpBlock>{(touched && error)}</HelpBlock>
                                </FormGroup>;
        return fieldContent;
    }
}

export default InputTextArea;