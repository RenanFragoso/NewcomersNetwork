'use strict';

import * as types from './actionTypes';
import {apiCalls, apiConfig} from '../api';
import { extractFormException } from 'lib/forms/FormValidations';
import { SubmissionError, reset } from 'redux-form';

// SIGN-UP
export function signUpFormSuccess() {
    return { type: types.USRADM_SIGNUP_SUCCESS };
}

export function receivedSignUpFormError(error) {
    return { type: types.USRADM_SIGNUP_ERROR, error };
}

export function sendSignUpForm(values){
    return (dispatch) => {
        apiCalls.login(values)
            .then(response => {
                if(response.status === 200){
                    const userInfo = {
                        userName: response.data.userName,
                        token: response.data.access_token,
                        name: '',
                        logged: true
                    }
                    dispatch(receivedLoginSuccess(userInfo));
                }
                else {
                    dispatch(receivedLoginError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(receivedLoginError(error));
            })
    };
}

// User Information form values
export function userDetailsFormSubmitSuccess() {
    return { type: types.USRADM_USERDETAILSFORMSUBMIT_SUCCESS };
}

export function userDetailsFormSubmitError(error) {
    return { type: types.USRADM_USERDETAILSFORMSUBMIT_FAIL, error };
}

export function receivedUserDetailsForm(userDetailsForm) {
    return { type: types.USRADM_USERDETAILSFORM_SUCCESS, userDetailsForm };
}

export function receivedUserDetailsFormError() {
    return { type: types.USRADM_USERDETAILSFORM_FAIL };
}

export function getUserDetailsForm(){
    return (dispatch) => {
        apiCalls.getUserDetailsForm()
            .then(response => {
                if(response.status === 200){
                    const userDetailsForm = response.data.userDetailsForm;
                    dispatch(receivedUserDetailsForm(userDetailsForm));
                }
                else {
                    dispatch(receivedUserDetailsFormError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(receivedUserDetailsFormError(error));
            })
    };
}

export function userDetailsFormSubmit(formData) {
    return (dispatch) => {
        return apiCalls
            .updateUserDetails(formData)
            .then(response => {
                if(response.status === 200){
                    dispatch(userDetailsFormSubmitSuccess());
                }
                else {
                    dispatch(userDetailsFormSubmitError(response.data.statusText));
                    throw new SubmissionError({ _error: 'Personal Information update fail' });
                }
            })
            .catch(error => { 
                dispatch(userDetailsFormSubmitError(error));
                throw extractFormException(error);
            })
    };
}

export function userAvatarSubmitSuccess() {
    return { type: types.USRADM_USERAVATARSUBMIT_SUCCESS };
}

export function userAvatarSubmitError(error) {
    return { type: types.USRADM_USERAVATARSUBMIT_FAIL, error };
}

export function userAvatarUpdate(imageData) {
    const data = avatarFormToMultidata(imageData);
    return (dispatch) => {
        apiCalls.updateUserAvatar(data)
            .then(response => {
                if(response.status === 200){
                    dispatch(reset('updateAvatarForm'));
                    dispatch(userAvatarSubmitSuccess());
                }
                else {
                    dispatch(userAvatarSubmitError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(userAvatarSubmitError(error));
            })
    };
}

function avatarFormToMultidata(values) {
    var data = new FormData();
    data.append("AvatarFile", values.AvatarFile);
    return data;
};

// User Information
export function receivedUserDetails(userDetails) {
    return { type: types.USRADM_USERDETAILS_SUCCESS, userDetails };
}

export function receivedUserDetailsError() {
    return { type: types.USRADM_USERDETAILS_FAIL };
}

export function getUserDetails(){
    return (dispatch) => {
        apiCalls.getUserDetails()
            .then(response => {
                if(response.status === 200){
                    const userDetails = response.data.userDetails;
                    dispatch(receivedUserDetails(userDetails));
                }
                else {
                    dispatch(receivedUserDetailsError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(receivedUserDetailsError(error));
            })
    };
}

// User Information select options
export function receivedValuesLists(valuesLists) {
    return { type: types.USRADM_USERDETAILVALUESLISTS_SUCCESS, valuesLists };
}

export function receivedValuesListsError(error) {
    return { type: types.USRADM_USERDETAILVALUESLISTS_ERROR, error };
}

export function getValuesLists() {
    return (dispatch) => {
        apiCalls.getValuesListsByGroup('user')
            .then(response => {
                if(response.status === 200){
                    const valuesLists = response.data;
                    dispatch(receivedValuesLists(valuesLists));
                }
                else {
                    dispatch(receivedValuesListsError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(receivedValuesListsError(error));
            })
    };
}

// FORGOT PASSWORD
export function forgotPasswordSuccess() {
    return { type: types.USRADM_FORGOTPASSWORD_SUCCESS };
}

export function forgotPasswordError() {
    return { type: types.USRADM_FORGOTPASSWORD_FAIL };
}

export function forgotPasswordSubmit(formData) {
    return (dispatch) => {
        return apiCalls
            .forgotPassword(formData)
            .then(response => {
                if(response.status === 200){
                    dispatch(forgotPasswordSuccess());
                }
                else {
                    dispatch(forgotPasswordError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(forgotPasswordError(error));
            })
    };
}

// RESET PASSWORD
export function resetPasswordSuccess() {
    return { type: types.USRADM_RESETPASSWORD_SUCCESS };
}

export function resetPasswordError() {
    return { type: types.USRADM_RESETPASSWORD_FAIL };
}

export function resetPasswordSubmit(formData) {
    return (dispatch) => {
        apiCalls.resetPassword(formData)
            .then(response => {
                if(response.status === 200){
                    dispatch(resetPasswordSuccess());
                }
                else {
                    dispatch(resetPasswordError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(resetPasswordError(error));
            })
    };
}

// REGISTER
const registerUserSuccess = (confirmMail) => {
    return { type: types.FORM_SIGNUP_SUCCESS, confirmMail };
}

const registerUserError = (errors) => {
    return { type: types.FORM_SIGNUP_FAIL, errors };
}

export function signupFormSubmit(formData) {
    return (dispatch) => {
        return apiCalls.registerUser(formData)
            .then(
                response => {
                    if(response.status === 200 || response.status === 201){
                        dispatch(reset('signUpForm'));
                        dispatch(registerUserSuccess(response.data.confirmMail))
                    }
                    throw new SubmissionError({ _error: 'SignUp Fail' });
                },
                error => {
                    dispatch(registerUserError(error));
                    throw extractFormException(error);
                })
    };
}

// CONFIRMATION MESSAGE
export function confirmMsg() {
    return { type: types.USRADM_CONFIRM_MSG };
}

export function resendConfirmMsg(mailTo) {
    return (dispatch) => {
        apiCalls.resendConfirmMsg(mailTo)
            .then(response => {
                if(response.status === 200){
                    dispatch(confirmMsg());
                }
                else {
                    dispatch(confirmMsg());
                }
            })
            .catch(error => { 
                dispatch(registerUserError(error));
            })
    };
}

// CONFIRM ACCOUNT
export function confirmAccountSuccess() {
    return { type: types.USRADM_ACCOUNTCONFIRM_SUCCESS };
}

export function confirmAccountError(error) {
    return { type: types.USRADM_ACCOUNTCONFIRM_FAIL, error};
}

export function tryConfirmAccount(accountConfirmation) {
    return (dispatch) => {
        apiCalls.confirmAccount(accountConfirmation)
            .then(response => {
                if(response.status === 200){
                    dispatch(confirmAccountSuccess());
                }
                else {
                    dispatch(confirmAccountError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(confirmAccountError(error));
            })
    };
}

// CHANGE PASSWORD
export function passwdChangeSuccess() {
    return { type: types.FORM_PASSWDCHANGE_SUCCESS };
}

export function passwdChangeError(errors) {
    return { type: types.FORM_PASSWDCHANGE_FAIL, errors };
}

export function passwdChangeFormStartSubmit () {
    return { type: types.FORM_PASSWDCHANGE_SUBMITTING };
}

export function passwdChangeSubmit(formData) {
    return (dispatch) => {
        return apiCalls
            .passwdChange(formData)
            .then(
                response => {
                    if(response.status === 200 || response.status === 201){
                        dispatch(reset('changePasswordForm'));
                        dispatch(passwdChangeSuccess(response.data.confirmMail))
                    }
                    else {
                        throw new SubmissionError({ _error: 'Change Password Fail' });
                    }
                },
                error => {
                    dispatch(passwdChangeError(error));
                    throw extractFormException(error);
                })
    };
}