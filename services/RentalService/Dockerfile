FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

EXPOSE 8060
EXPOSE 8061

COPY RentalServiceBuild/ .

ENTRYPOINT ["dotnet", "RentalService.Server.dll"]