dotnet restore
dotnet publish -c Release -r linux-arm


na Raspberry Pi, com comando elevado:

sudo -i
cd /home/pi/publish
chmod +x aspnet.webapi.rpi.gpio
export ASPNETCORE_URLS="http://*:5000"
./aspnet.webapi.rpi.gpio

