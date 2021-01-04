FROM node:13.12.0-alpine

WORKDIR /app

COPY moviemates_frontend/package.json /app

RUN npm install 

COPY moviemates_frontend/. /app

CMD ["npm", "run", "start"] 