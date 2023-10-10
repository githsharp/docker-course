FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /app

COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o build

FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

COPY --from=build-env /app/build .

EXPOSE 5011

ENV ASPNETCORE_URLS=http://+:5011

ENTRYPOINT ["dotnet", "movieflix-app.dll"]
