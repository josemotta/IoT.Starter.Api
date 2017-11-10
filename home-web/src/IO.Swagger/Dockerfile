# See dockerfile template from Alex Ellis
# https://blog.alexellis.io/dotnetcore-on-raspberrypi/?mkt_tok=eyJpIjoiWldVeE16ZGhOV1poWm1ReSIsInQiOiJpSUo1UlZTOTFteGhydElPRG9JY3Vlb1JXUUtreUMyQjFPb3NOXC9CRnREbGdhMXhoQmZ3NlZTNVpHK2ZYXC90MkNwTkNzNUZLY3ZUK1M3b0ltXC9acEQ0dm9PY05xTGhFOFVmdmR1YUFKMktDOXhjb3BYWXYwM2FcLzdreDZNY2d3eFAifQ%3D%3D
#
FROM microsoft/dotnet:2.0-sdk as builder
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1
WORKDIR /app
COPY . /app
# Previous version deprecated
# RUN /bin/bash -c "cd /app && dotnet restore ./IO.Swagger.csproj -r linux-arm && dotnet publish ./IO.Swagger.csproj -c Release -r linux-arm -o ./obj/Docker/publish"
RUN dotnet restore ./IO.Swagger.csproj
RUN dotnet publish -c release -r linux-arm -o published

FROM microsoft/dotnet:2.0.0-runtime-stretch-arm32v7
WORKDIR /app
EXPOSE 5010
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1
ENV ASPNETCORE_URLS "http://*:5010"
COPY --from=builder /app/published .
ENTRYPOINT ./IO.Swagger

