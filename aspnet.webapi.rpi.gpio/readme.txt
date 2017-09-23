dotnet restore
dotnet publish -c Release -r linux-arm


na Raspberry Pi, com comando elevado:

sudo -i
cd /home/pi/publish
chmod +x aspnet.webapi.rpi.gpio
export ASPNETCORE_URLS="http://*:5000"
./aspnet.webapi.rpi.gpio

DOCKER

$ docker run -p 5000:5000 aspnet.webapi.rpi.gpio

DOCKER-COMPOSE (usado pelo VS)

docker-compose -f "C:\_git\Api\aspnet.webapi.rpi.gpio\docker-compose.yml" -f "C:\_git\Api\aspnet.webapi.rpi.gpio\docker-compose.override.yml" -f "C:\_git\Api\aspnet.webapi.rpi.gpio\obj\Docker\docker-compose.vs.release.g.yml" -p dockercompose17839059008373070602 config
services:
  aspnet.webapi.rpi.gpio:
    build:
      args:
        source: obj/Docker/publish/
      context: C:\_git\Api\aspnet.webapi.rpi.gpio
      dockerfile: Dockerfile
    entrypoint: tail -f /dev/null
    environment:
      ASPNETCORE_ENVIRONMENT: Development
    image: aspnet.webapi.rpi.gpio
    labels:
      com.microsoft.visualstudio.debuggee.arguments: ' --additionalProbingPath /root/.nuget/packages
        --additionalProbingPath /root/.nuget/fallbackpackages  aspnet.webapi.rpi.gpio.dll'
      com.microsoft.visualstudio.debuggee.killprogram: /bin/bash -c "if PID=$(pidof
        -x dotnet); then kill $PID; fi"
      com.microsoft.visualstudio.debuggee.program: dotnet
      com.microsoft.visualstudio.debuggee.workingdirectory: /app
    ports:
    - 80/tcp
    volumes:
    - C:\Users\jo\vsdbg:/remote_debugger:ro
version: '3.0'
docker  ps --filter "status=running" --filter "name=dockercompose17839059008373070602_aspnet.webapi.rpi.gpio_" --format {{.ID}} -n 1
83b6136c5aba
docker  inspect --format="{{json .NetworkSettings.Ports}}" 83b6136c5aba
{"80/tcp":[{"HostIp":"0.0.0.0","HostPort":"32769"}]}
Waiting for response from http://localhost:32769/ ...

