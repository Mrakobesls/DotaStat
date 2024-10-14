import React, {useEffect, useState} from 'react';
import {useAuth} from "../../Providers/AuthProvider";

const Auth: React.FC = () => {
    const [text, setText] = useState("Works");
    const {login, logout} = useAuth();

    return <div>
        <button onClick={login}>Login</button>
        <button onClick={logout}>Logout</button>
    </div>
}

export default Auth;
