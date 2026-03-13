FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src
COPY ["src/AARS.Web.Host/AARS.Web.Host.csproj", "src/AARS.Web.Host/"]
COPY ["src/AARS.Web.Core/AARS.Web.Core.csproj", "src/AARS.Web.Core/"]
COPY ["src/AARS.Application/AARS.Application.csproj", "src/AARS.Application/"]
COPY ["src/AARS.Core/AARS.Core.csproj", "src/AARS.Core/"]
COPY ["src/AARS.EntityFrameworkCore/AARS.EntityFrameworkCore.csproj", "src/AARS.EntityFrameworkCore/"]
WORKDIR "/src/src/AARS.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/AARS.Web.Host", "src/AARS.Web.Host"]
COPY ["src/AARS.Web.Core", "src/AARS.Web.Core"]
COPY ["src/AARS.Application", "src/AARS.Application"]
COPY ["src/AARS.Core", "src/AARS.Core"]
COPY ["src/AARS.EntityFrameworkCore", "src/AARS.EntityFrameworkCore"]
WORKDIR "/src/src/AARS.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "AARS.Web.Host.dll"]
