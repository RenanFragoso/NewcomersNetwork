'use strict';

import { SubmissionError } from 'redux-form';

const required = value => (value || typeof value === 'number' ? undefined : 'Required');
const email = value => (value && !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(value) ? 'Invalid email address' : undefined);
const alphaNumeric = value => (value && /[^a-zA-Z0-9 ]/i.test(value) ? 'Only alphanumeric characters' : undefined);
const number = value => (value && isNaN(Number(value)) ? 'Must be a number' : undefined);
const passwordsMatch = (vals) => ( vals.password !== vals.confirmPassword ? 'Passwords doesn not match' : undefined );
const minLength = min => value => (value && value.length < min ? `Must be ${min} characters or more` : undefined);
const minValue = min => value => (value && value < min ? `Must be at least ${min}` : undefined);
const maxLength = max => value => (value && value.length > max ? `Must be ${max} characters or less` : undefined);
const maxValue = max => value => (value && value < min ? `Must be at least ${min}` : undefined);
const maxLength15 = maxLength(15);
const phoneNumber = value => ( value && !/^(0|[1-9][0-9]{9})$/i.test(value) ? 'Invalid phone number, must be 10 digits' : undefined );
const checked = value => ( value ? 'You need to select' : undefined );
const formValid = {
    password: (value, values, form, fieldName) => {
        if(values.Password && values.ConfirmPassword) {
            if(!(values.Password === values.ConfirmPassword)) {
                return 'Passwords does not match.';
            }
        }
        return undefined;
    },
    termsSelected: (value) => {
        if(!value) {
            return 'You must read and accept the Newcomers Network Terms of Use.'
        }
        return undefined;
    }
};

const extractFormException = (respError) => {

    var submmissError = new SubmissionError();
    if(respError.response.data && respError.response.data.ModelState) {
        submmissError.errors = Object.assign( {},
                                    respError.response.data.ModelState,
                                    { '_error': respError.response.data.Message} );
    }
    return submmissError;
}


export default {
    required,
    email,
    alphaNumeric,
    number,
    passwordsMatch,
    maxLength15,
    phoneNumber,
    checked,
    formValid,
    extractFormException
}