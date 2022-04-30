'use strict';

import * as types from './actionTypes';
import {admApiCalls, apiCalls, apiConfig} from 'api';
import { toast } from 'react-toastify';

// User List
function receivedUserList(data) {
    return { type: types.USERS_RECEIVED_LIST, data} ;
}

function receivedUserListError(error) {
    return { type: types.USERS_RECEIVED_LISTERROR, error };
}

export function getUserList(filterParams){
    return (dispatch) => {
        admApiCalls.getUsersList(filterParams)
            .then(response => {
                dispatch(receivedUserList(response.data));
                toast.success("Users loaded");
            })
            .catch(error => { 
                dispatch(receivedUserListError(error));
                toast.error("Users loading error, please try again later");
            })
    };
}

// Roles
function receivedRoles(data) {
    return { type: types.USERS_RECEIVED_ROLES, data} ;
}

function receivedRolesError(error) {
    return { type: types.USERS_RECEIVED_ROLES_ERROR, error };
}

export function getRoles(){
    return (dispatch) => {
        admApiCalls.getRoles()
            .then(response => {
                dispatch(receivedRoles(response.data));
            })
            .catch(error => { 
                dispatch(receivedRolesError(error));
            })
    };
}