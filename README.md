# iof_admin
IoF admin tool

 ASP.net core application to configure the IoF things.

 It can either be deployed to a local IIS or IIS Express using Kesterel or as a Docker container behind any other Reverse-Proxy like Nginx

## Compile in Visual Studio
Simply open the iof_admin.sln in Visual Studio and run it. There are different run configurations (IISExpress, web, Docker).
Just select the best one for you.

Tested in Visual Studio 2015 Community and Professional.

## Compile the docker image and run it 
Under Windows you need to have Docker for Windows installed. I only could get it running under Windows 10 Professional.

For Linux it should work on any installation which supports Docker version 1.12+ and docker-compose version 1.9.0+.
Tested under Ubuntu 16.04.

### Build the application
- in solution root directroy run `docker-compose up`. 
- This will build the application in release mode and create a docker-container to run the application under `publish/web`
- To rebuild the new container run `docker-compose -f ./publish/web/docker-compose.yml build`


### Run the built container
- in solution root directroy run `docker-compose -f ./publish/web/docker-compose.yml up` which will open the app using a console
- in solution root directroy run `docker-compose -f ./publish/web/docker-compose.yml up -d` which will open the app in deamon mode.

 
