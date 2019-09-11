import React from 'react';
import { FormGroup, ControlLabel, FormControl, HelpBlock } from 'react-bootstrap';

class InputText extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { id, label, type, input, placeholder} = this.props;
        const { touched, error, warning, dirty } = this.props.meta;
        const validStation = ( touched && error ? "error" : null );
        /*
        <div>
            <label>{label}</label>
            <div>
                <input className={inputClass} placeholder={placeholder || label} type={type} {...input}/>
                {touched && ((error && <span>{error}</span>) || (warning && <span>{warning}</span>))}
            </div>
        </div>
        */
        const fieldContent =    <FormGroup controlId={id} validationState={validStation}>
                                    <ControlLabel>{label}</ControlLabel>
                                    <FormControl id={id} type={type} placeholder={placeholder} {...input}/>
                                    <FormControl.Feedback />
                                    <HelpBlock>{(touched && error)}</HelpBlock>
                                </FormGroup>;        
        return( fieldContent );
    }
}

export default InputText;