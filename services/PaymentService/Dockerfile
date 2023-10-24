FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

EXPOSE 8050
EXPOSE 8051

COPY PaymentServiceBuild/ .

ENTRYPOINT ["dotnet", "PaymentService.Server.dll"]