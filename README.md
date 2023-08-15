# Galytix - CountryGwp
This project implements Web APIs to process GWP (Gross Written Premium) for various lines of businesses in multiple countries. 
The data model is defined in the Models/ directory as following:
* GwpItem : Defines the schema of how an GWP item is stored int the database
* GwpAvgRequest: Defines the input model for the gwp/avg api
* GwpAvgResponse: Defines the output model for the gwp/avg api

We define two types of APIs
1. GwpItem CRUD APIs (http://localhost:9091/server/api/gwp/[controller]): These APIs are used to do CRUD operations on the database.
2. Gwp Average API (http://localhost:9091/server/api/gwp/avg): This API is used to calculate the average GWP for given lines of businesses in the specified country.

## How to Execute:
This is an ASP .NET Core 6.0 Web API

Pre-requisites:
https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio#prerequisites

## Assumptions:
1. Cache : I am caching the subquery {country, business} instead of {country, List< business >} under the assumption that there will be multiple queries which will not be identical but will have significant overlap (country, business) with other queries. Based on the usage pattern, we might choose to cache the complete query itself.
2. Database Seeding : I am using an in-memory database, therefore, the CRUD operations will be reset on program restart. Also, I am using a seed json file to initialize the databse: Data/GwpSeed.json.

## Directory Structure:
* __Controller/__: Controllers for the APIs
* __Models/__: Data models
* __Data/__: Seed data for the database
* __Screenshots/__: Screenshots of the working of the APIs
* __Properties/__: Launch settings