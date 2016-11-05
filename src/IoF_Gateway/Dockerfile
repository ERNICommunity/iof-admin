FROM python:3-slim

# File Author / Maintainer
MAINTAINER Chris Zürcher

# Get pip to download and install requirements:
RUN pip install paho-mqtt

# Copy the application folder inside the container
ADD . /code
WORKDIR /code

CMD [ "python", "-u","StartupTask.py" ]

