import React, {Children, ReactNode, useCallback, useContext, useEffect, useMemo, useState} from "react";
import {AuthService} from "../Services/AuthService";

type AuthValues = {
    claims: string[] | null;
    errors: any;
    isAuthenticated: boolean;
    isInitialized: boolean;
    user: any;
};

type AuthContext = {
    appState: {
        isReady: boolean;
        hasError: boolean;
    };
    authValues: AuthValues;
    login: (userInfo: any) => void;
    logout: () => void;
};

const DEFAULT_AUTH_VALUES: AuthValues = {
    claims: null,
    errors: null,
    isAuthenticated: false,
    isInitialized: false,
    user: null
};

const authContext = React.createContext<AuthContext | undefined>(undefined);

const AuthProvider: React.FC<{ children?: ReactNode | undefined }> = ({children}) => {
    const [appState, setAppState] = useState({
        isReady: false,
        hasError: false
    });
    const [authValues, setAuthValues] = useState<AuthValues>(DEFAULT_AUTH_VALUES);
    const [authService, setAuthService] = useState<AuthService>(new AuthService());


    const state: AuthContext = useMemo(
        () => {
            // Take the bearer token received in a header and check auth status against identity server
            // This method sets the auth status of the FrontEnd UI
            // TODO: Figure out the contract and make types for user and userinfo
            const login = async () => {
                await authService.login();
                setAuthValues({
                    ...DEFAULT_AUTH_VALUES
                    // claims: userInfo.claims,
                    // isAuthenticated: true,
                    // isInitialized: true,
                    // errors: null,
                    // user: userInfo
                });
            };

            // This method clears the auth status in the FrontEnd UI
            const logout = async () => {
                await authService.logout();
                // Do logout request here to server
                setAuthValues(DEFAULT_AUTH_VALUES);
                return Promise.resolve(true);
            };

            return {
                appState,
                authValues,
                login,
                logout
            }
        },
        [appState, authValues, authService]
    );

    return (
        <authContext.Provider value={state}>{children}</authContext.Provider>
    );
};

export default AuthProvider;

/**
 * @description
 * This hook returns the current state of the application including user auth status, ui site settings, and access
 * to the login and logout functions.
 */
export const useAuth = (): AuthContext => {
    const context = useContext(authContext);

    if (context === undefined) {
        throw new Error(
            "AuthContext is not initialized."
        );
    }

    if (process.env.NODE_ENV === "development") {
        if (context === null) {
            throw new Error(
                "The component consuming useApp is not nested under an instance of AuthContext."
            );
        }
    }

    return context;
};
