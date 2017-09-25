FROM josemottalopes/gpio-base
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "aspnet.webapi.rpi.gpio.dll"]
