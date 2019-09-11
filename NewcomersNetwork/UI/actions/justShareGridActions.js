import * as types from './actionTypes';
import {apiCalls, apiConfig} from '../api';

export function receivedBanners(banners) {
    return { type: types.HPAGE_RECEIVED_BANNERS, banners};
}

export function receivedSBlocks(serviceBlocks) {
    return { type: types.HPAGE_RECEIVED_SBLOCKS, serviceBlocks };
}

export function receivedJustShre(justShare) {
    return { type: types.HPAGE_RECEIVED_JSHARE, justShare };
}

export function receivedServices(services) {
    return { type: types.HPAGE_RECEIVED_SERVICES, services };
}

export function receivedEvents(events) {
    return { type: types.HPAGE_RECEIVED_EVENTS, events };
}

export function receivedTestimonials(testimonials) {
    return { type: types.HPAGE_RECEIVED_TESTIMONIALS, testimonials };
}

export function receivedError(error) {
    return { type: types.HPAGE_ERROR_MESSAGE, error };
}

export function loadBannersWidget(){
    return (dispatch) => {
        apiCalls.loadBanners()
            .then(response => {
                dispatch(receivedBanners(response.data.banners));
            })
            .catch(error => { 
                dispatch(receivedError(error));
            })
    };
}

export function loadSBlocksWidget(){
    return (dispatch) => {
        apiCalls.loadSBlocks()
            .then(response => {
                dispatch(receivedSBlocks(response.data.serviceBlocks));
            })
            .catch(error => dispatch(receivedError(error)))
    };
}

export function loadJustShareWidget(){
    return (dispatch) => {
        apiCalls.loadJustShare()
            .then(response => {
                dispatch(receivedJustShre(response.data.justShare));
            })
            .catch(error => dispatch(receivedError(error)))
    };
}

export function loadServicesWidget(){
    return (dispatch) => {
        apiCalls.loadServices()
            .then(response => {
                dispatch(receivedServices(response.data.services));
            })
            .catch(error => dispatch(receivedError(error)))
    };
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

export function loadTestimonialsWidget(){
    return (dispatch) => {
        apiCalls.loadTestimonials()
            .then(response => {
                dispatch(receivedTestimonials(response.data.testimonials));
            })
            .catch(error => dispatch(receivedError(error)))
    };
}