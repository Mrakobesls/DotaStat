import {UserManagerSettings} from "oidc-client-ts";

export const oidcConfig: UserManagerSettings = {
    client_id: 'webapp',
    automaticSilentRenew: true,
    redirect_uri: 'http://dotastat:5035/signin-oidc',
    post_logout_redirect_uri: "http://dotastat:5035/signout-oidc",
    response_type: 'code',
    scope: "openid profile statistics",
    authority: 'http://identity-api:5041',
};
