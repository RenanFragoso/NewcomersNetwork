import * as types from './actionTypes';
import {apiCalls, apiConfig} from '../api';

function receivedError(error) {
    return { type: types.EVENTS_RECEIVED_ERROR, error };
}

function receivedEvents(events) {
    return { type: types.HPAGE_RECEIVED_EVENTS, events };
}

export function loadEventsWidget(){
    return (dispatch) => {
        apiCalls.loadEvents()
            .then(response => {
                dispatch(receivedEvents(response.data.events));
            })
            .catch(error => dispatch(receivedError(error)))
    };
}

function receivedEvent(event) {
    return { type: types.EVENTS_RECEIVED_EVENT, event };
}

export function getEventById(eventId){
    return (dispatch) => {
        apiCalls.getEventById(eventId)
            .then(response => {
                dispatch(receivedEvent(response.data));
            })
            .catch(error => dispatch(receivedError(error)))
    };
}






