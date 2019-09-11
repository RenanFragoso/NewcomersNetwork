'use strict';

import * as types from './actionTypes';
import {apiCalls, apiConfig} from 'api/AdminUi';

export function receivedUserInfo(userInfo) {
    return { type: types.AUTH_RECEIVED_USERINFO, userInfo} ;
}

export function receivedUserInfoError(error) {
    return { type: types.AUTH_USERINFO_ERROR, error };
}

export function receivedError(error) {
    return { type: types.ERROR_MESSAGE, error };
}

export function getUserInfo(){
    return (dispatch) => {
        apiCalls.userInfo()
            .then(response => {
                dispatch(receivedUserInfo(response.data.userInfo));
            })
            .catch(error => { 
                dispatch(receivedUserInfoError(error));
            })
    };
}

// LOGIN
export function loginFormSubmitting() {
    return { type: types.FORM_LOGINFORM_SUBMITTING };
}

export function loginFormDoneSubmitting() {
    return { type: types.FORM_LOGINFORM_DONESUBMITTING };
}

export function receivedLoginSuccess(userInfo) {
    return { type: types.AUTH_RECEIVED_LOGINSUCCESS, userInfo };
}

export function receivedLoginError(error) {
    return { type: types.AUTH_RECEIVED_LOGINERROR, error };
}

export function tryLogin({username, password}){
    return (dispatch) => {
        dispatch(loginFormSubmitting());
        apiCalls.login({username, password})
            .then(response => {
                if(response.status === 200){
                    const userInfo = {
                        userName: response.data.userName,
                        token: response.data.access_token,
                        name: '',
                        logged: true
                    }
                    dispatch(loginFormDoneSubmitting());
                    dispatch(receivedLoginSuccess(userInfo));
                }
                else {
                    dispatch(loginFormDoneSubmitting());
                    dispatch(receivedLoginError(response.data.statusText));
                }
            })
            .catch(error => { 
                dispatch(loginFormDoneSubmitting());
                dispatch(receivedLoginError(error));
            })
    };
}

export function logout() {
    return (dispatch) => { 
        dispatch({ type: types.AUTH_RECEIVED_LOGOUT }) 
    };
}

export function cleanErrors() {
    return (dispatch) => { 
        dispatch({ type: types.FORM_LOGINFORM_CLEANERROR }) 
    };
}