export default {
    auth: {
        userInfo: {
            UserName: '',
            Name: '',
            Title: '',
            Picture: '',
            token: '',
            Roles: [],
            logged: false
        },
        result: true,
        status: false,
        errorMessage: {}
    },
    homePage: {
        banners: [],
        serviceBlocks: [],
        justShare: [],
        services: [],
        events: [],
        testimonials: [],
        errorMessage: {}
    },
    justShare: {
        justShareGrid: {
            mode: '',
            filters: [],
            items: [],
            pagination: {},
            status: false,
            result: false,
            categories: [],
            filterData: {
                Page: 1,
                PageSize: 10,
                Categories: [],
                Type: '',
                Word: ''
            }
        },
        justShareDetails: {
        },
        newJustShareId: '',
        errorMessage: {},
        userJustShares: {
            justShareWidget: [],
            result: false,
            status: false,
            error: ''
        }
    },
    events: {
        event: {}
    },
    services: {
        serviceGroups: [],
        serviceGroup: {},
        services: [],
        service: {}
    },
    forms: {
        loginForm: {
            status: false,
            result: false,
            submitting: false
        },
        userDetailsForm: { 
            Captcha: '', 
            AvatarFile: '',
            status: true,
            result: false,
            errors: {}
        },
        updateAvatarForm: { 
            Captcha: '',
            status: true,
            result: false,
            errors: {}
        },
        changePasswordForm: { 
            Captcha: '',
            status: true,
            result: false,
            error: {}
        },
        addJustShareForm: {
            Captcha: '',
            Id: '',
            Type: '',
            status: true,
            result: false,
            error: {}
        },
        jsMsgForm: {
            Captcha: '',
            Message: '',
            ClassifiedId: '',
            ResponseTo: 0,
            status: true,
            result: false,
            error: {}
        },
        signUpForm: {
            Captcha: '',
            status: true,
            result: false,
            errors: {},
            confirmMail: ''
        },
        forgotPasswordForm:
        {
            Captcha: '',
            status: true,
            result: false,
            errors: {}
        },
        valuesLists: [],
    },
    userAdm: {
        signUpStatus: {
            result: false,
            status: false,
            error: '',
            confirmMail: ''
        },
        accountConfirmation: {
            result: false,
            status: false,
            error: ''
        },
        userDetails: {},
        userMessages: {
            messages: [
                {   message: 'No messages for you at this moment.' },
                {   
                    message: 'Please complete your personal information.', 
                    msgType: 'warning' 
                },
                {   
                    message: 'Please complete the Intake Form.', 
                    msgType: 'danger' 
                }
            ],
            result: false,
            status: false,
            error: ''
        },
        changePasswordStatus: {
            result: false,
            status: false,
            error: ''
        }
    },
    modal: {
        login: {
            show: false
        }
    }
}