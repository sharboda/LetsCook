# from clarifai import rest
from clarifai.rest import ClarifaiApp
import base64


def getVeg(bytes):
    app = ClarifaiApp(api_key = 'db84d6c8a2df47e9a492c3ffd5bbfef3')
    model = app.models.get('food-items-v1.0')
    data = model.predict_by_base64(bytes,max_concepts = 10)
    data = data['outputs']
    data = data[0]
    data = data['data']
    data = data['concepts']
    veggies = []
    for food in data:
        print (food['name'])
        veggies.append(food['name'])
    return veggies