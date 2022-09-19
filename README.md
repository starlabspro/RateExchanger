# RateExchanger

RateExchanger has 2 main services:
* RateExchanger Service
* UserRateExchanger Service

## RateExchanger Service
Does the following:
* Fetches the latest rates from Fixer.io
* Stores the rates in a database
* Stores the rates in a cache
* Provides an API to get the latest rates through a REST API

## UserRateExchanger Service
Does the following:
* Provides an API for the User to get the latest rates through a REST API
* Checks the request limit for the User
* Calls the RateExchanger Service to get the latest rates

## How to run the application
* Locally the project cab be run using VS Code or Visual Studio
* Since there is no docker support, the application can be run locally using the following steps:
    * Run the RateExchanger Service
    * Run the UserRateExchanger Service
* Or the two projects can be run at once in this way: [img](docs/multiple-projects.png)
* Call the API using Postman or any other tool
* The API is documented using Swagger
* The API to be called is https://localhost:5021/api/v1/UserRateExchanger
* The request and response samples can be found here: [link](docs/requests-and-responses)

## NOTES
* Caching is done in memory - meaning that if the service is restarted, the cache will be empty
* The security is done using a simple API key - but it is disabled
* The API Key for communicating with Fixer is limited to 100 requests per hour (since it's free)
* There is no docker support
* Tests are limited and only contain some of the functionality which needs to be tested