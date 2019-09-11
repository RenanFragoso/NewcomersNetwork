import React from 'react';

class Select extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const {label, type, input, touched, error, warning, placeholder} = this.props;
        return(
            <div>
                <label>{label}</label>
                <div>
                    <select className="form-control" placeholder={placeholder || label} {...input} >
                        {
                            this.props.options.map((item, i) => { 
                                return <option value={item.id} key={i} selected={this.props.input.value === item.id}>{item.text}</option>
                            } )
                        }
                    </select>
                    {touched && ((error && <span>{error}</span>) || (warning && <span>{warning}</span>))}
                </div>
            </div>
        );
    }
}

export default Select;