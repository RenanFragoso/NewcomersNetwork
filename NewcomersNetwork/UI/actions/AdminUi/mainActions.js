'use strict';

import * as types from './actionTypes';
import {admApiCalls} from 'api';

// User List
function receivedValuesLists(data) {
    return { type: types.VALUESLISTS_RECEIVED, data} ;
}

function receivedValuesListsError(error) {
    return { type: types.VALUESLISTS_RECEIVED_ERROR, error };
}

export function getValuesLists(){
    return (dispatch) => {
        admApiCalls.getValuesLists()
            .then(response => {
                dispatch(receivedValuesLists(response.data));
            })
            .catch(error => { 
                dispatch(receivedValuesListsError(error));
            })
    };
}
