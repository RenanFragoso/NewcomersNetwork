'use strict';

import * as types from './actionTypes';
import {apiCalls, admApiCalls, apiConfig} from 'api';

function receivedUserInfo(userInfo) {
    return { type: types.USER_INFO_SUCCESS, userInfo} ;
}

function receivedUserInfoError(error) {
    return { type: types.USER_INFO_SUCCESS, error };
}

function receivedError(error) {
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