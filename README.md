<h1 align="center">
  <br>
  people-location-api <br>
    <a href="https://github.com/AminurRouf/people-location-api"><img src="https://raw.githubusercontent.com/AminurRouf/assets/master/images/api.png" alt="Markdownify" width="200"></a>
    <br>
    <img src="https://github.com/AminurRouf/people-location-api/workflows/Build%20and%20deploy%20ASP.Net%20Core%20app%20to%20Azure%20Web%20App%20-%20people-location-api/badge.svg" alt="Status badge"/>

</h1>

<h4 align="center">An OpenAPI standard API for listing people who either live in London or whose current coordinates are within fifty miles of London </h4>

<p><img src="https://raw.githubusercontent.com/AminurRouf/assets/master/images/api.gif" alt="screenshot" /></p>


The people-location-api ("The API") is a RESTful API that retrieves users from another app API, who are either living in London or  whose coordinates are currently within 50 miles of London. Full instructions can be found  [here](http://bpdts-test-app.herokuapp.com/instructions) .

This web API is built with C# and .NET Core 3.1 framework using a [Swagger](https://swagger.io/) tooling package called [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) to meet OpenAPI 3.0 specifications.

<p align="center">
  <a href="#live-demo">Live Demo</a> •
  <a href="#requirements">Requirements</a> •
  <a href="#setup">Setup</a> •
  <a href="#endpoints">Endpoints</a> •
  <a href="#design">Design</a>  •
  <a href="#next-steps">Next Steps</a> •
  <a href="#disclaimer">Disclaimer</a>
</p>

## Live Demo

A live demonstration of the API can be found at https://people-location-api.azurewebsites.net/swagger/index.html (assuming there are enough Azure monthly credits to keep the meter running). 

## Requirements

This API is built on .NET Core 3.1 framework with Visual Studio 2019 IDE. To run the API locally you need the <strong>.NET Core 3.1 SDK </strong> installed on the machine. Running the project through Visual Studio is the easiest way to get up and running, an alternative is to use Visual Studio Code. Both are optional as you can use the dotnet CLI tool which installs with the SDK to build and run the project.The dotnet CLI documentation can be found [here](https://docs.microsoft.com/en-us/dotnet/core/tools/).

#### Download Links
- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [Visual Studio 2019 IDE](https://visualstudio.microsoft.com/vs/) (Optional community edition is free)
- [Visual Code with .NET Core](https://code.visualstudio.com/docs/languages/dotnet) (Optional)


## Setup

Click  <button style='background-color:green; color:white'>&darr; Code.</button> and either download ZIP or clone the master branch.
```bash
# If cloning in a command window
# Clone this repository
 git clone https://github.com/AminurRouf/people-location-api.git

# Go into the repository
 cd people-location-api

# Build and restore dependencies using dotnet CLI.
# (You shouldn't  have to run dotnet restore because build implicitly restores all)
$ dotnet build 

# Run tests
 dotnet test

# After testing finishes
 cd PeopleLocationApi

# Run the app
$ dotnet Run
```

## Run
##### dotnet CLI
If you've used the CLI to build and run the project, open a browser and navigate to https://localhost:5001/swagger/index.html . <strong>Ignore and advance through any browser security warnings you may encounter</strong> .This should launch the Swagger UI page with a list of available endpoints. Click on any of the presented endpoint to expand the accordian, click "Try it out" and press the "Execute" button. This will make a request to the API and respond with an status code and the response body should contain a list of people who meet the criteria - if the status code is 200.

##### Visual Studio 2019
With VS 2019 navigate to the project solution PeopleLocationApi.sln, open it. Press <button type="button"> CTRL+SHIFT+B</button> to build it. <button type="button">F5</button> to run the project. it should automatically launch and open a browser page https://localhost:44349/swagger/index.html and present the Swagger UI as described above.


## Endpoints

There are three specific endpoints in this API.
 - <strong>/city/london/people</strong> - This gets a list of people who are listed as living in London.
 - <strong>/city/london/coordinates-within-fifty-miles/people</strong> - This gets a list of people whose coordinates are within fifty miles of London.
 - <strong>/city/london/living-in-or-coordinates-within-fifty-miles/people</strong> - This gets a list of <em>distinct</em> people who are either living in London, or whose current coordinates are within 50 miles of London. i.e. If a person appears in both above endpoints, they will only be listed here once.

## Design

##### Considerations

Without a business user or an analyst to clarify the requirements, a number of assumptions have been made with regards to the instructions provided.
- The term <strong>"within fifty miles"</strong> has been interepreted as <em>distance <= 50 miles</em> rather than <em>distance < 50 miles</em>.
- It was not altogether clear if the instructions were asking for a single endpoint to list people who are either living in London or whose coordinates are currently within fity miles of London; or to list them seperately using two different endpoints. The instructions did say <em>design an API</em>, on that note the API has implemented three endpoints to cover both scenarios. It also means that  the API is flexible to allow it's clients to make the implementation choice for their own requirements.    
- The longitudal and latitudal coordinates of London city are citied differently by different sources. Latitude of 51.509865 and Longitude of -0.118092 are citied here https://www.latlong.net/place/london-the-uk-14153.html and are used by the API to calculate distances.
- There are different methods to calculate the distances for a given longitude and latitude. .NET core 3.1 framework does not have a built in GeoCoordinate class. The API used a cut down version of the source code from the  GeoCoordinate class in .NET 4.8 framework and derived it using the Haversine Formula as detailed here http://mathforum.org/library/drmath/view/51879.html .

##### Implementation

In designing the API, I've tried to use the <strong>SOLID</strong> design principles of object-oriented programming, which the programming langauge C# provides full support for. I've have tried to keep the classes specific to the task, implementing only interfaces that it needs and used inheritance only where appropiate. The aim of all this is to achieve as best as possible the Single Responsiblity Principle so that the software can deal with the ripple effects of possible future changes without over engineering it or catering for things not needed yet or possibly ever.

One good side effect of adhering to these principles, is that the testability of the components improve and the boundaries of what needs to be tested are better defined. I've used xUnit and TDD to drive the design of the API, using the classic Arrange, Assert, Act formula. The mocking package Moq was used to mock external dependecies of the unit under test. More than 85% test code coverage was achieved.

Another benefit is that should the need arise, to extend the API in the future so that it can deal with any city or any given distance, it would be fairly trivial to implement these extra conditions with minimal code changes and therefore limiting any potential side effects. The core business logic of the API is built in a reusable fashion and hopefully it should not need to change at all.

##### Semantics

The bdpdts test app returns a list of Users, however the instructions speak of listing People. Whilst the difference maybe a subtle, I felt the semantic difference was important in the context of a domain driven design approach and hence have gone to some lengths to ensure that this API returns specifically People rather than Users. Whilst User inherits from Person, it carries extra properties such as longitude, latitude and ip address which I felt should be omitted in the response as these properties are not what i would attribute to a person in normal langauge usage.

#### Best Practices

The  bdpdts test app returns json properties using snake-case. The API returns people properties using camelCase naming convention as per google json standard guide lines found [here](https://google.github.io/styleguide/jsoncstyleguide.xml) .

The endpoint names and designs tried to follow the microsoft Web API design guidelines found [here](https://docs.microsoft.com/en-us/azure/architecture/best-practices/api-design).

Most importantly I have tried to be consistant in whatever conventions or standards I have adopted to use through out the project.

## Dependancies

A full list of the dependancies used by the API can be found [here](https://github.com/AminurRouf/people-location-api/network/dependencies
)
## Next Steps
This API could be extended to allow it to return people from any city and within any distance. A CityPeople controller as oppossed to just LondonPeople.

## Disclaimer
This API was written entirely by me Aminur Rouf (some commits may show up as nar25 - that's also me!). This is a test API and  uses dummy test data provided by bpdts test app and should not be used for anything important - like trying to locate a real person within fifty miles of London.

