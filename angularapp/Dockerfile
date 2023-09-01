FROM node:lts as build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build --prod

FROM nginx:latest
COPY --from=build /app/dist/fuse /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]