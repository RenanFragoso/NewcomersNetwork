import * as types from './actionTypes';
import {apiCalls, apiConfig} from '../api';

function receivedError(error) {
    return { type: types.SERVICES_RECEIVED_ERROR, error };
}

function receivedServiceGroups(serviceGroups) {
    return { type: types.SERVICES_RECEIVED_SERVICEGROUPS, serviceGroups };
}

export function getServiceGroups(){
    return (dispatch) => {
        apiCalls.getServiceGroups()
            .then(response => {
                dispatch(receivedServiceGroups(response.data));
            })
            .catch(error => dispatch(receivedError(error)))
    };
}

function receivedServiceGroup(serviceGroup) {
    return { type: types.SERVICES_RECEIVED_SERVICEGROUP, serviceGroup };
}

export function getServiceGroup(groupId){
    return (dispatch) => {
        apiCalls.getServiceGroup(groupId)
            .then(response => {
                dispatch(receivedServiceGroup(response.data));
            })
            .catch(error => dispatch(receivedError(error)))
    };
}

function receivedServicesByGroup(services) {
    return { type: types.SERVICES_RECEIVED_SERVICES, services };
}

export function getServicesByGroup(groupId){
    return (dispatch) => {
        apiCalls.getServicesByGroup(groupId)
            .then(response => {
                dispatch(receivedServicesByGroup(response.data));
            })
            .catch(error => dispatch(receivedError(error)))
    };
}

function receivedService(service) {
    return { type: types.SERVICES_RECEIVED_SERVICE, service };
}

export function getServiceById(serviceId){
    return (dispatch) => {
        apiCalls.getServiceById(serviceId)
            .then(response => {
                dispatch(receivedService(response.data));
            })
            .catch(error => dispatch(receivedError(error)))
    };
}