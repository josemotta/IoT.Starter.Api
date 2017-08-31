# IO.Swagger - ASP.NET Core 1.0 Server

An API for keeping track of your contacts and the companies they work for.   Don't forget to take it for a spin by clicking on the **Try in Client** button next to each operation! All read operations are public and don't require authentication.  <div> <img src='https://thecontactsapi.apispark.net/v1/img/cropped-contact-img.png' width='90%'> <div>  <!- - ![](https://thecontactsapi.apispark.net/v1/img/cropped-contact-img.png) - ->  

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
