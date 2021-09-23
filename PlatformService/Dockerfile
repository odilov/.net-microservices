FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# copy PlatformService.csproj to workdir
COPY *.csproj ./
# install packages
RUN dotnet restore

# copy src to build it
COPY . ./
RUN dotnet publish -c Release -o out

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
    # copy output from build environment
COPY --from=build-env /app/out .

# run image
ENTRYPOINT ["dotnet", "PlatformService.dll"]