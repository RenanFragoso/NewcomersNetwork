'use strict';

import * as types from './actionTypes';
import {apiCalls, apiConfig} from 'api';
import { reset } from 'redux-form';
import { extractFormException } from 'lib/forms/FormValidations';

// GETS JUSTSHARE GRID DATA
export function loadJustShareGridData(dataParams){
    return (dispatch) => {
        dispatch(justShareGridLoading());
        apiCalls.loadJustShareGridData(dataParams)
            .then(response => {
                dispatch(justShareGridDoneLoading());
                dispatch(receivedGridData(response.data.justShareGridData));
            })
            .catch(error => {
                dispatch(justShareGridDoneLoading()); 
                dispatch(receivedError(error));
            })
    };
}

export function receivedGridData(justShareGridData) {
    return { type: types.JSHARE_RECEIVED_GRID_DATA, justShareGridData};
}

export function receivedError(error) {
    return { type: types.JSHARE_ERROR_MESSAGE, error };
}

export function justShareGridLoading() {
    return { type: types.JUSTSHARE_GRID_LOADING };
}

export function justShareGridDoneLoading() {
    return { type: types.JUSTSHARE_GRID_DONELOADING };
}

// GETS JUSTSHARE DETAILS
export function receivedDetails(justShareDetails) {
    return { type: types.JSHARE_RECEIVED_DETAILS, justShareDetails};
}

export function loadJustShareDetails(justShareId){
    return (dispatch) => {
        apiCalls.loadJustShareDetails(justShareId)
            .then(response => {
                dispatch(receivedDetails(response.data.justShareWidget /*justShareDetails*/));
            })
            .catch(error => { 
                dispatch(receivedError(error));
            })
    };
}

export function goToLogin() {
    this.props.history.push("/login");
}

export function goToDetails(event) {
    this.props.history.push("/justshare/" + event.currentTarget.attributes["data-jsid"].nodeValue);
}

// GET VALUES LISTS RELATED TO JUSTSHARE
export function getValuesLists() {
    return (dispatch) => {
        apiCalls.getValuesListsByGroup('classified')
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

export function receivedValuesLists(valuesLists) {
    return { type: types.RECEIVED_VALUELISTS_SUCCESS, valuesLists};
}

export function receivedValuesListsError(error) {
    return { type: types.RECEIVED_VALUELISTS_ERROR, error};
}

// CREATES A NEW JUSTSHARE
export function justShareFormSubmitSucces() {
    return { type: types.JUSTSHARE_FORMSUBMIT_SUCCESS };
}

export function justShareFormSubmitError(error) {
    return { type: types.JUSTSHARE_FORMSUBMIT_ERROR, error};
}

export function justShareFormSubmitting() {
    return { type: types.FORM_JUSTSHAREFORM_SUBMITTING };
}

export function justShareFormSubmit(formValues) {
    const data = justShareFormToMultidata(formValues);
    return (dispatch) => {
        dispatch(justShareFormSubmitting());
        return apiCalls
            .justShareFormSubmit(data)
            .then(
                response => {
                    if(response.status === 200 || response.status === 201){
                        dispatch(reset('addJustShareForm'));
                        dispatch(justShareFormSubmitSucces());
                    }
                    dispatch(justShareFormSubmitError('Create JustShare Error'));
                    throw new SubmissionError({ _error: 'Create JustShare Error' });
                },
                error => {
                    dispatch(justShareFormSubmitError(error));
                    throw extractFormException(error);
                }
            );
    };
}

function justShareFormToMultidata(values) {
    var data = new FormData();
    data.append("Image", values.Image);
    data.append("Id", values.Id);
    data.append("Type", values.Type);
    data.append("Category", values.Category);
    data.append("Title", values.Title);
    data.append("Text", values.Text);
    //data.append("Captcha", values.Captcha);
    return data;
};

// SUBMIT JUSTSHARE IMAGE
export function justShareImageSubmit(imageData) {
    return (dispatch) => {
        apiCalls.updateJustShareImage(imageData)
            .then(response => {
                if(response.status === 200 || response.status === 201){
                    dispatch(justShareImageubmitSucces());
                }
                else {
                    dispatch(justShareImageSubmitError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(justShareImageSubmitError(error));
            })
    };
}

export function justShareImageubmitSucces() {
    return { type: types.JUSTSHARE_IMAGESUBMIT_SUCCESS };
}

export function justShareImageSubmitError(error) {
    return { type: types.JUSTSHARE_IMAGESUBMIT_ERROR, error};
}

// GETS NEW JUSTSHARE ID
export function getNewJustShareId() {
    return (dispatch) => {
        apiCalls.getNewJustShareId()
            .then(response => {
                if(response.status === 200){
                    dispatch(newJustShareIdSucces(response.data.newId));
                }
                else {
                    dispatch(newJustShareIdError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(newJustShareIdError(error));
            })
    };
}

export function newJustShareIdSucces(newId) {
    return { type: types.JUSTSHARE_NEWID_SUCCESS, newId };
}

export function newJustShareIdError(error) {
    return { type: types.JUSTSHARE_NEWID_ERROR, error };
}

// ADD JUST SHARE MESSAGE
export function jsMsgFormSubmit(formData) {
    return (dispatch) => {
        return apiCalls.addJustShareMessage(formData)
                .then(response => {
                    if(response.status === 200 || response.status === 201){
                        dispatch(addJustShareMessageSucces(response.data.newId));
                    }
                    else {
                        dispatch(addJustShareMessageError('Create Message Error'));
                        throw new SubmissionError({ _error: 'Create Message Error' });
                    }
                })
                .catch(error => { 
                    dispatch(addJustShareMessageError(error));
                    throw extractFormException(error);
                })
    };
}

export function addJustShareMessageSucces() {
    return { type: types.JUSTSHARE_ADDMESSAGE_SUCCESS };
}

export function addJustShareMessageError(error) {
    return { type: types.JUSTSHARE_ADDMESSAGE_ERROR, error };
}

//User JustShare Widget
export function userJustShareSuccess(userJustShare) {
    return { type: types.JUSTSHARE_USERJUSTSHARE_SUCCESS, userJustShare };
}

export function userJustShareError(error) {
    return { type: types.JUSTSHARE_USERJUSTSHARE_FAIL, error};
}

export function getUserJustShare(page, offset) {
    return (dispatch) => {
        apiCalls.getUserJustShareWidget(page, offset)
            .then(response => {
                if(response.status === 200){
                    dispatch(userJustShareSuccess(response.data));
                }
                else {
                    dispatch(userJustShareError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(userJustShareError(error));
            })
    };
}

//Finish JustShare
export function finishJustShareSuccess() {
    return { type: types.JUSTSHARE_FINISH_SUCCESS };
}

export function finishJustShareError(error) {
    return { type: types.JUSTSHARE_FINISH_FAIL, error};
}

export function finishJustShare(justShareId) {
    return (dispatch) => {
        apiCalls.finishJustShare(justShareId)
            .then(response => {
                if(response.status === 200){
                    dispatch(finishJustShareSuccess());
                }
                else {
                    dispatch(finishJustShareError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(finishJustShareError(error));
            })
    };
}

// Select Just Share grid type
export function selectedJsType(jsType) {
    return { type: types.JUSTSHARE_SELECTED_GRIDTYPE, jsType };
}

export function selectJsType(jsType) {
    return (dispatch) => {
        dispatch(selectedJsType(jsType));
    };
}

// Login Modal Show/Hide
function loginModalShowEvnt() {
    return { type: types.MODAL_LOGIN_SHOW };
}

function loginModalHideEvnt() {
    return { type: types.MODAL_LOGIN_HIDE };
}

export function showLoginModal() {
    return (dispatch) => {
        dispatch(loginModalShowEvnt());
    }
}

export function hideLoginModal() {
    return (dispatch) => {
        dispatch(loginModalHideEvnt());
    }
}