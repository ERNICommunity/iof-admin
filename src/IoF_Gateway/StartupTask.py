import urllib.request
import json
import datetime
import paho.mqtt.client as mqtt

# The callback for when the client receives a CONNACK response from the server.
def on_connect(client, userdata, flags, rc):
    print("Connected with result code "+str(rc))

    # Subscribing in on_connect() means that if we lose the connection and
    # reconnect then subscriptions will be renewed.
    client.subscribe("iof/config")

# The callback for when a PUBLISH message is received from the server.
def on_message(client, userdata, msg):
    print(datetime.datetime.now().strftime("%y-%m-%d %H:%M:%S") +" Topic: "+msg.topic+" Payload: "+str(msg.payload,'utf-8'))
    #wjdata = json.loads(msg.payload)
    #print(wjdata['aquariumId'])
    url = "http://iof.azurewebsites.net/api/configuration/"+str(msg.payload,'utf-8')
    print(url)
    with urllib.request.urlopen(url) as response:
        content = response.read()
        print(content)

client = mqtt.Client()
client.on_connect = on_connect
client.on_message = on_message

client.connect("iot.eclipse.org", 1883, 60)

# Blocking call that processes network traffic, dispatches callbacks and
# handles reconnecting.
# Other loop*() functions are available that give a threaded interface and a
# manual interface.
client.loop_forever()
