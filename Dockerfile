FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /App

COPY . ./

RUN dotnet restore
RUN dotnet publish -c Debug -o out



FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Web.dll"]
