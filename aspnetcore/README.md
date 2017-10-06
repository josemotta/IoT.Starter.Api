# IO.Swagger - ASP.NET Core 1.0 Server

Teste para workflow API-based:   - Restlet gera API e exporta swagger.json   - NSwag importa arquivo e gera server stub e client class    

## Run

Linux/OS X:

```
sh build.sh
```

Windows:

```
build.bat
```

## Run in Docker

```
cd src/IO.Swagger
docker build -t IO.Swagger .
docker run -p 5000:5000 IO.Swagger
```
