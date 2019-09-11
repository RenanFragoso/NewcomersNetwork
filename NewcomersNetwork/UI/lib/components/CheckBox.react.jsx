import React from 'react';

class CheckBox extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const {label, type, input, name, id, touched, error, warning} = this.props;
        return( <div className="form-check">
                    <input type="checkbox" className="form-check-input" placeholder={label} type={type} {...input} />
                    <label className="form-check-label" htmlFor={id}>{label}</label>
                    {touched && ((error && <span>{error}</span>) || (warning && <span>{warning}</span>))}
                </div>);
    }
}

export default CheckBox;