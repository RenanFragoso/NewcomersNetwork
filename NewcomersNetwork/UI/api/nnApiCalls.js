import axios from 'axios';
import * as apiConfig from './nnApiConfig';
import qs from 'qs';
import querystring from 'querystring';
export default {
    
    // General
    
    //Values Lists
    getValuesLists: () => {
        return axios.get(apiConfig.apiUrl + "/values", apiConfig.authConfig());
    },

    getValuesListsByName: (listName) => {
        return axios.get(apiConfig.apiUrl + "/values/list/" + listName, apiConfig.authConfig());
    },

    getValuesListsByGroup: (groupName) => {
        return axios.get(apiConfig.apiUrl + "/values/group/" + groupName, apiConfig.authConfig());
    },

    // Auth Endpoints
    login: ({username, password}) => {
        return axios({  method: 'POST',
                        url: apiConfig.apiUrl + "/token",
                        headers: {
                            'Content-Type': 'application/x-www-form-urlencoded',
                        },
                        data: querystring.stringify({   "grant_type": "password", 
                                                        "username": username, 
                                                        "password": password })
                        });
    },
    
    userInfo: () => {
        return axios.get(apiConfig.apiUrl + "/nnauth/userinfo", apiConfig.authConfig());
    },

    // User Details
    getUserDetails: () => {
        return axios.get(apiConfig.apiUrl + "/users/getdetails", apiConfig.authConfig());
    },

    // Home Page Endpoints
    loadBanners: () => {
        return axios.get(apiConfig.apiUrl + "/widgets/banners");
    },

    loadSBlocks: () => {
        return axios.get(apiConfig.apiUrl + "/widgets/service-blocks");
    },

    loadJustShare: () => {
        return axios.get(apiConfig.apiUrl + "/widgets/justshare");
    },

    loadServices: () => {
        return axios.get(apiConfig.apiUrl + "/widgets/services");
    },

    loadEvents: () => {
        return axios.get(apiConfig.apiUrl + "/widgets/events");
    },

    loadTestimonials: () => {
        return axios.get(apiConfig.apiUrl + "/widgets/testimonials");
    },

    // JustShare Endpoints
    loadJustShareGridData: (dataParams) => {
        return axios.post(apiConfig.apiUrl + "/justshare/getfiltered", dataParams, apiConfig.authConfig());
    },

    loadJustShareDetails: (justShareId) => {
        return axios.get(apiConfig.apiUrl + "/justshare/" + justShareId, apiConfig.authConfig());
    },

    justShareFormSubmit: (formData) => {
        return axios.post(apiConfig.apiUrl + "/justshare", formData, apiConfig.authConfig());
    },

    justShareImageSubmit: (formData) => {
        return axios.post(apiConfig.apiUrl + "/justshare/create", formData, apiConfig.authConfig());
    },

    getNewJustShareId: () => {
        return axios.get(apiConfig.apiUrl + "/justshare/requestid", apiConfig.authConfig());
    },

    addJustShareMessage: (formData) => {
        return axios.post(apiConfig.apiUrl + "/justshare/addmessage", formData, apiConfig.authConfig());
    },

    finishJustShare: (justShareId) => {
        return axios.post(apiConfig.apiUrl + "/justshare/finish", { justShareId }, apiConfig.authConfig())
    },

    // User Adm EndPoints
    sendSignUpFform: (values) => {
        return axios.post(apiConfig.apiUrl + "/UserProfile/SignUp", values);
    },
    
    getUserDetailsForm: () => {
        return axios.get(apiConfig.apiUrl + "/users/userdetailsform", apiConfig.authConfig());
    },
 
    updateUserDetails: (formData) => {
        return axios.put(apiConfig.apiUrl + "/users/details", formData, apiConfig.authConfig());
    },

    passwdChange: (formData) => {
        return axios.post(apiConfig.apiUrl + "/account/changepassword", formData, apiConfig.authConfig());
    },

    forgotPassword: (formData) => {
        return axios.post(apiConfig.apiUrl + "/account/forgotsend", formData);
    },

    resetPassword: (formData) => {
        return axios.post(apiConfig.apiUrl + "/account/resetpassword", formData);
    },

    registerUser: (formData) => {
        return axios.post(apiConfig.apiUrl + "/account/register", formData);
    },

    confirmAccount: (accountConfirmation) => {
        return axios.post(apiConfig.apiUrl + "/account/confirmation", accountConfirmation);
    },

    getUserJustShareWidget: (page, offset) => {
        return axios.get(apiConfig.apiUrl + "/widgets/userjustshare/" + page + "/" + offset, apiConfig.authConfig())
    },
    
    updateUserAvatar: (imageData) => {
        return axios.put(apiConfig.apiUrl + "/users/avatar", imageData, apiConfig.authConfig())
    },

    // Image EndPoints
    updateJustShareImage: (imageData) => {
        return axios.put(apiConfig.apiUrl + "/image/justshareimage", imageData, apiConfig.authConfig())
    },

    //Events
    getEventById: (eventId) => {
        return axios.get(apiConfig.apiUrl + "/events/" + eventId, apiConfig.authConfig())
    },

    // Services
    getServiceById: (serviceId) => {
        return axios.get(apiConfig.apiUrl + "/services/" + serviceId, apiConfig.authConfig())
    },

    getServiceGroups: () => {
        return axios.get(apiConfig.apiUrl + "/services/groups", apiConfig.authConfig())
    },
   
    getServiceGroup: (groupId) => {
        return axios.get(apiConfig.apiUrl + "/services/groups/" + groupId, apiConfig.authConfig())
    },

    getServicesByGroup: (groupId) => {
        return axios.get(apiConfig.apiUrl + "/services/bygroup" + groupId, apiConfig.authConfig())
    }
}