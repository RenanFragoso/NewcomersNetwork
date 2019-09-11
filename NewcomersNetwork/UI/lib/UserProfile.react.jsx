import React from 'react';
import UserProfilePage from './layout/UserAdmin/UserProfilePage.react';

class UserProfile extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return( 
            <UserProfilePage />
        );
    }
};

export default UserProfile;
