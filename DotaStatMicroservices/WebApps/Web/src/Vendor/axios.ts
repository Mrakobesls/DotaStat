// Import from this file wherever you would otherwise include:
// import axios from "axios";
// There should be no direct axios imports.

import axios from "axios";

const instance = axios.create({
    baseURL: 'http://localhost:5050',
    
    // Our authorization is stored in a cookie.
    // That cookie needs to be included in requests to the API which are at a different domain
    // than where the UI is served from.
    // withCredentials: true,

    // We never want the browser to cache requests to the API.
    headers: {
        "Cache-Control": "no-cache",
        Expires: "0"
    }
});

export default instance;

export * from "axios";
