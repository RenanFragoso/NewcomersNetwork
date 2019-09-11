'use strict';

import * as types from './actionTypes';

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