FROM microsoft/dotnet:2.1-sdk-alpine as build-env
WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-runtime-alpine3.7 AS runtime
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "DockerConsole.dll"]

# Helper commands
#RUN ["sh", "-c", "echo Hello"]
#RUN ["sh", "-c", "ls"]

# Docker CLI commands
# docker build . -t dockerconsole:latest
# docker run -t dockerconsole:latest -e "SettingA=docker" -e "AppSettings:NestedSetting=docker2"