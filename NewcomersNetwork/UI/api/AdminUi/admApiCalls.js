'use strict';

import axios from 'axios';
import { apiConfig } from 'api';

export default {

    userInfo: () => {
        return axios.get(apiConfig.apiUrl + "/nnauth/userinfo", apiConfig.authConfig());
    },

    // Users Lists
    getUsersList: (filterParams) => {
        return axios.post(apiConfig.apiUrl + "/users", filterParams, apiConfig.authConfig());
    },

    // Roles
    getRoles: () => {
        return axios.get(apiConfig.apiUrl + "/roles", apiConfig.authConfig());
    },
    
    // Values Lists
    getValuesLists: () => {
        return axios.get(apiConfig.apiUrl + "/values", apiConfig.authConfig());
    }

}