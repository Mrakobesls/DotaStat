import React, {useEffect, useState} from 'react';
import logo from './logo.svg';
import './App.css';
import axios from "./Vendor/axios";

const App: React.FC = () => {
    const loginUrl = 'http://localhost:5040/Account/Login?returnUrl=' + window.location.href;
    const [text, setText] = useState("Works");
    
    useEffect(() => {
        axios.get(`/TextOk`)
            .then((response) => {
                setText(response.data);
            });
    });

    return <div className="App">
        <header className="App-header">
            <img src={logo} className="App-logo" alt="logo"/>
            <a href={loginUrl}>
                Login
            </a>
            <div>{text}</div>
            <p>
                Edit <code>src/App.tsx</code> and save to reload.
            </p>
            <a
                className="App-link"
                href="https://reactjs.org"
                target="_blank"
                rel="noopener noreferrer"
            >
                Learn React
            </a>
        </header>
    </div>
}

export default App;
