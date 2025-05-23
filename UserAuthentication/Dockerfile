FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln . 
COPY UserAuthentication/*.csproj ./UserAuthentication/
COPY UnitTest/*.csproj ./UnitTest/

RUN dotnet restore --no-cache

COPY UserAuthentication/. ./UserAuthentication/
COPY UnitTest/. ./UnitTest/

WORKDIR /source/UserAuthentication
RUN dotnet publish UserAuthentication.csproj -c release -o /app 

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app ./ 
EXPOSE 8080
ENTRYPOINT ["dotnet", "UserAuthentication.dll"]
