FROM node:18

WORKDIR /app

COPY package.json .

RUN npm install

COPY . .
ARG DEFAULT_PORT=80

#miljövariabelporten här, ska inte ha 80 längre, utan ska ha default porten
ENV PORT=${DEFAULT_PORT}

EXPOSE $PORT

CMD [ "node", "server.js" ]





# FROM node:18

# WORKDIR /app

# COPY package.json .

# RUN npm install

# COPY . .

# de två raderna har nu skapat en dynamisk variabel
# detta är en miljövariabel 

# ENV PORT=80

# detta är en port som ska exponeras
# EXPOSE $PORT


# EXPOSE 80

# detta är en anonym volym
# VOLUME { "app/groceries"}

# detta tar med node modules från hosten
# VOLUME {"/app/node_modules"}

# CMD [ "node", "server.js" ]