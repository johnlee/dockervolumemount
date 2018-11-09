FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["volumeMounting.csproj", "./"]
RUN dotnet restore "./volumeMounting.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "volumeMounting.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "volumeMounting.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
#ENTRYPOINT ["/bin/bash"]
ENTRYPOINT ["dotnet", "volumeMounting.dll"]

# Docker commands (these can be called by runDocker.sh script):
# docker build --rm -f "docker-debug.Dockerfile" -t volume .
# docker run --rm -it -p 80:80/tcp volume
# docker run --rm -it -v $(pwd)/logs:/app/logs volume