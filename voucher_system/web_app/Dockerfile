FROM node:20-alpine3.17 AS build
WORKDIR /app

COPY . .
RUN npm install
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/dist/voucher-management-system/ /usr/share/nginx/html
EXPOSE 80
