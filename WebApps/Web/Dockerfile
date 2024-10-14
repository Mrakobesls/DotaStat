FROM node:21-alpine as build
WORKDIR /app
COPY package.json /app/package.json
RUN npm install
#    --only=prod
COPY . /app
RUN npm run build

FROM nginx:1.25-alpine
COPY --from=build /app/build /usr/share/nginx/html
EXPOSE 5035
CMD ["nginx", "-g", "daemon off;"]



#FROM node:21-alpine AS build
#
## Add a work directory
#WORKDIR /app
#
## Copy app files
#COPY . .
#
## Install dependencies
## Dev dependencies are included because we treat build tooling as dev dependencies 
## separate from application dependencies. 
## I.e., "typescript" is a dev dependency while "react" is a regular dependency.
#RUN npm ci --include=dev
#
## Build/Bundle the client side application
## The build tools output into /build
#RUN npm run build
#
## Bundle static assets (front end) and serve with nginx
#FROM nginx:1.25-alpine
#
## Copy the build static files (front end) to the nginx share
#COPY --from=build /app/build /usr/share/nginx/html
#
## Expose our ports
## SSL terminates at the application gateway so we only use port 80
#EXPOSE 80
#
## included in nginx documentation
#STOPSIGNAL SIGQUIT
#
## Start the server
#CMD ["nginx", "-g", "daemon off;"]
