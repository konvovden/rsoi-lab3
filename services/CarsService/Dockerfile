FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

EXPOSE 8070
EXPOSE 8071

COPY CarsServiceBuild/ .

ENTRYPOINT ["dotnet", "CarsService.Server.dll"]