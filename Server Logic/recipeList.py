import os
import json
import csv
import os.path
from urllib import request
from bs4 import BeautifulSoup
import sys
import ssl
from recipe_scrapers import scrape_me


HEADERS = {
    'User-Agent': 'Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.7) Gecko/2009021910 Firefox/3.0.7'
}

urls = [{'italian':'https://www.allrecipes.com/recipes/723/world-cuisine/european/italian/'},
	{'indian':'https://www.allrecipes.com/recipes/233/world-cuisine/asian/indian/'},
	{'asian':'https://www.allrecipes.com/recipes/227/world-cuisine/asian/'},
	{'mexican':'https://www.allrecipes.com/recipes/728/world-cuisine/latin-american/mexican/'},
	{'southern':'https://www.allrecipes.com/recipes/15876/us-recipes/southern/'}
]
selectors = ["li[class~=trending-story]","li[class~=list-recipes__recipe]", "div[class~=grid-card-image-container]"]

fname = '/home/sharpi/mysite/recipeList.csv'
title_key = 'lblRecipeName'
total_time_key = 'lblTiming'
ingredients_key = 'lblIngredients'
instructions_key = 'lblInstructions'
img_key = 'imgRecipe'

def getHeader():
	fieldnames = [title_key, total_time_key, ingredients_key, instructions_key, img_key];
	return fieldnames

def writeDataToFile(title, total_time, ingredients, instructions, img):
	fieldnames = getHeader()
	writeHeader = False
	if not os.path.isfile(fname):
		writeHeader = True
	with open(fname, 'a', encoding='utf-8') as csvfile:
		writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
		if writeHeader:
			writer.writeheader()
		row = {}
		row[title_key] = title
		row[total_time_key] = total_time + ' min'
		row[ingredients_key] = ingredients
		row[instructions_key] = instructions
		row[img_key] = img
		writer.writerow(row)

def getDataAndWrite(url, img):
	try:
		data = scrape_me(url)
		if data.title() is "" or data.ingredients() is "" or data.instructions() is "":
			return
		writeDataToFile(data.title(), data.total_time(), data.ingredients(), data.instructions(), img)
	except:
		print("error for url: "+url)
		return

def getAllRecipesUrl(pageUrl):
	response = request.urlopen(request.Request(pageUrl, headers=HEADERS), context=ssl.SSLContext(ssl.PROTOCOL_TLSv1)).read()
	soup = BeautifulSoup(response, "html.parser")
	for selector in selectors:
		recipe_links = soup.select(selector)
		print(len(recipe_links))
		for recipe_link in recipe_links:
			recipe = recipe_link.select("a")
			if len(recipe) == 1:
				href = recipe[0]['href']
				img = recipe[0].select("img")[0]['data-original-src']
				getDataAndWrite(href, img)

def parseAllUrls():
	for url in urls:
		for urlValue in url.values():
			print(urlValue)
			getAllRecipesUrl(urlValue)


def retrieveRecipeList():
	with open(fname, 'rt', encoding='utf-8') as csvfile:
		fileReader = csv.reader(csvfile)
		fileContent = [row for row in fileReader]
		header = getHeader()
		fileContent.pop(0)
		fileContent.pop(0)  # skip blank line
		reqData = []
		for row in fileContent:
			rowdict = dict(zip(header, row))
			reqData.append(rowdict)
			fileContent.pop(0)
		return reqData


def retrieveRecipeListBasedOnIngredient(ingredientList):
	with open(fname, 'rt', encoding='utf-8') as csvfile:
		fileReader = csv.reader(csvfile)
		fileContent = [row for row in fileReader]
		header = getHeader()
		fileContent.pop(0)
		fileContent.pop(0)  # skip blank line
		reqData = []
		for row in fileContent:
			rowdict = dict(zip(header, row))
			ingredients = rowdict[ingredients_key];
			for ingredient in ingredientList:
				if ingredient in ingredients:
					reqData.append(rowdict)
					break
			fileContent.pop(0)
		return reqData

def resetServer():
	if os.path.isfile(fname):
		os.remove(fname)
	parseAllUrls()