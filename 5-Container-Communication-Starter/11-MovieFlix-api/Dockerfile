# set baseimage for docker
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# set working directory
WORKDIR /app

# copy package.json to working directory
COPY . ./

# install dependencies
RUN dotnet restore

# build and publish
RUN dotnet publish -c Release -o build

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# set working directory
WORKDIR /app

# copy from build enviroment to working directory
COPY --from=build-env /app/build .

# expose port
EXPOSE 80

# set command to run when container starts
ENTRYPOINT ["dotnet", "MovieFlix.dll"]
# Path: 5-Container-Communication-Starter/11-MovieFlix-api/server.js
# import express
# const express = require('express');

