<h1 align="center">
  <br>
  people-location-api <br>
    <a href="https://github.com/AminurRouf/people-location-api"><img src="https://raw.githubusercontent.com/AminurRouf/assets/master/images/api.png" alt="Markdownify" width="200"></a>
    <br>
    <img src="https://github.com/AminurRouf/people-location-api/workflows/Build%20and%20deploy%20ASP.Net%20Core%20app%20to%20Azure%20Web%20App%20-%20people-location-api/badge.svg" alt="Status badge"/>

</h1>

<h4 align="center">An OpenAPI standard API for listing people who either live in London or whose current coordinates are within fifty miles of London </h4>

<p><img src="https://raw.githubusercontent.com/AminurRouf/assets/master/images/api.gif" alt="screenshot" /></p>


The people-location-api ("The API") retrieves users from another app API, who are either living in London or  whose coordinates are currently within 50 miles of London. Full instructions can be found  [here](http://bpdts-test-app.herokuapp.com/instructions) .

This web API is built with C# and .NET Core 3.1 framework using a  [Swagger](https://swagger.io/) tooling package called [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) to meet OpenAPI 3.0 specifications.

<p align="center">
  <a href="#live-demo">Live Demo</a> •
  <a href="#requirements">Requirements</a> •
  <a href="#Setup">Setup</a> •
  <a href="#Run">Run</a> •
  <a href="#Endpoints">Endpoints</a> •
  <a href="#license">License</a>
</p>

## Live Demo

A live demonstration of the API can be found at https://people-location-api.azurewebsites.net/swagger/index.html (assuming there are enough Azure monthly credits to keep the meter running). 

## Requirements

This API is built on .NET Core 3.1 framework with Visual Studio 2019 IDE. To run the API locally you need the .NET Core 3.1 SDK installed on the machine. Running the project through Visual Studio is the easiest way to get up and running, an alternative is to use Visual Studio Code. Both are optional as you can use the dotnet CLI tool which installs with the SDK to build and run the project.The dotnet CLI documentation can be found [here](https://docs.microsoft.com/en-us/dotnet/core/tools/).

#### Download Links
- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [Visual Studio 2019 IDE](https://visualstudio.microsoft.com/vs/) (Optional community edition is free)
- [Visual Code with .NET Core](https://code.visualstudio.com/docs/languages/dotnet) (Optional)


## Setup

Click  <button style='background-color:green; color:white'>&darr; Code.</button> and either download ZIP or clone the master branch.
```bash
# Clone this repository
$ git clone https://github.com/AminurRouf/people-location-api.git

# Go into the repository
$ cd PeopleLocationApi

# Build and restore dependencies using dotnet CLI.
# (You shouldn't  have to run dotnet restore because build implicitly restores all)
$ dotnet build 

# Run the app
$ dotnet Run
```

## Run
##### dotnet CLI
If you've used the CLI to build and run the project, open a browser and navigate to https://localhost:5001/swagger/index.html . This should launch the Swagger UI to with a list of available endpoints. Click on any of the presented endpoint to expand the accordian, click "Try it out" and the hit the execute button. This will make a request to the API and respond with a 200 status code and the response body should contain a list of people who meet the criteria.

##### Visual Studio 2019
With VS 2019 navigate to the project solution PeopleLocationApi.sln, open it. Press <button> CTRL+SHIFT+B</button> to build it. <button>F5</button> to run the project, it should automatically launch and open a browser page https://localhost:44349/swagger/index.html and present the Swagger UI as described above.


## Endpoints

## Considerations

## Next Steps

## Disclaimer



