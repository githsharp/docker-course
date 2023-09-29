FROM node:18

WORKDIR /app

COPY package.json .

RUN npm install

COPY . .

EXPOSE 80

# detta är en anonym volym
#VOLUME { "app/groceries"}

CMD [ "node", "server.js" ]