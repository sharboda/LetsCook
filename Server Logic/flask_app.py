
# A very simple Flask Hello World app for you to get started with...
import recipeList
import veggie
from flask import Flask, request
from flask_restful import Resource, Api
import json

app = Flask(__name__)

class Recipes(Resource):
	def get (self):
		return recipeList.retrieveRecipeList()

	def post (self):
		data = json.loads(request.data)
		return recipeList.retrieveRecipeListBasedOnIngredient(data)

class Veggies(Resource):
    def post (self):
        data = request.data
        return veggie.getVeg(data)

api = Api(app)
api.add_resource(Recipes, '/recipes')
api.add_resource(Veggies, '/veggies')


@app.route('/')
def hello_world():
    return 'Hello from Flask!'