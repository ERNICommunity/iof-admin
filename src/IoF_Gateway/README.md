# iof-gateway
IoF MQTT to REST gateway

This gateway subscribes to IoF config topic and forwards the request to the IoF Admin WebApp (WebAPI).

It's a simple python script which can be run in any environment connect to the internet. No open incoming ports on firewalls are required to run it.

## Docker
The gateway can be run in a docker container using docker or docker-compose (second is recommended).

`sudo docker-compose build` to build the image

`sudo docker-compose run` to run it with console output

`sudo docker-compose run -d` to run it in daemon mode (use `sudo docker-compose logs` to see the output)

