FROM microsoft/aspnetcore-build:1.0-2.0 as builder
WORKDIR /app
ADD ./ /app/
RUN /bin/bash -c "cd /app && dotnet restore ./IO.Swagger.csproj -r linux-arm && dotnet publish ./IO.Swagger.csproj -c Release -r linux-arm -o ./obj/Docker/publish"

FROM josemottalopes/gpio-base
WORKDIR /app
EXPOSE 5010
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1
ENV ASPNETCORE_URLS "http://*:5010"
COPY --from=builder /app/obj/Docker/publish/ /app/
ENTRYPOINT /app/IO.Swagger

