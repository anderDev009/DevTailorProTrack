FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY TailorProTrack.sln ./
COPY TailorProTrack.Api/TailorProTrack.Api.csproj TailorProTrack.Api/
COPY TailorProTrack.Application/TailorProTrack.Application.csproj TailorProTrack.Application/
COPY TailorProTrack.Ioc/TailorProTrack.Ioc.csproj TailorProTrack.Ioc/
COPY TailorProTrack.infraestructure/TailorProTrack.infraestructure.csproj TailorProTrack.infraestructure/
COPY TailorProTrack.domain/TailorProTrack.domain.csproj TailorProTrack.domain/
RUN dotnet restore TailorProTrack.Api/TailorProTrack.Api.csproj

COPY . .
RUN dotnet publish TailorProTrack.Api/TailorProTrack.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TailorProTrack.Api.dll"]
