FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

EXPOSE 8080

COPY GatewayServiceBuild/ .

ENTRYPOINT ["dotnet", "GatewayService.Server.dll"]