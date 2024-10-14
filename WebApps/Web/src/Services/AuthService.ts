import { Log, User, UserManager } from 'oidc-client-ts';

import { oidcConfig } from '../Vendor/OidcConfig';

export class AuthService {
    public userManager: UserManager;

    constructor() {
        this.userManager = new UserManager(oidcConfig);

        Log.setLogger(console);
        Log.setLevel(Log.INFO);
    }

    public getUser(): Promise<User | null> {
        return this.userManager.getUser();
    }

    public login(): Promise<void> {
        return this.userManager.signinRedirect();
    }

    public renewToken(): Promise<User | null> {
        return this.userManager.signinSilent();
    }

    public logout(): Promise<void> {
        return this.userManager.signoutRedirect();
    }
}
