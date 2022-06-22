# .Net Super Heath Inc. API







### Description

Super Health Inc. is a small regional healthcare company that operates a series of clinics. The
company has an existing application for tracking patient encounter data. This application has
been in service for a number of years and is in need of a rewrite. Super Health has hired you to
rewrite the application in a modern way using the technologies that you have trained in. At this
point, Super Health Inc. is looking for a proof of concept, and will not require any authentication
or authorization.
The database stores patient and encounter data, and the design of the database is up to you.
Any user will be able to review, create and update patient information and encounters.
The client has expressed that they would like the project to be documented and easily
maintainable.


### Pre-requisites 

## Dependencies

# Postman

Postman is used to send request to the API just like a browser would

# .Net Runtime

You must have a .Net runtime installed on your machine.

# Postgres

This server requires that you have Postgres installed and running on the default Postgres port of 5432. It requires that you have a database created on the server with the name of `postgres`
- Your username should be `postgres`
- Your password should be `root`

#### Entity Framework Core
This is the heart of the Entity Framework tools.
`Install-Package Microsoft.EntityFrameworkCore -Version 5.0.10`
https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/6.0.0-rc.1.21452.10

#### Entity Framework Core Design
Used for creating EF migrations
`Install-Package Microsoft.EntityFrameworkCore.Design -Version 5.0.10`

#### Swashbuckle
Also known as Swagger, this tool creates an interface for the API when you run the application.  You can use markup in the controllers to show documentation on the interface.  It is preinstalled with basic configuration (Startup.cs) when creating a new API with the newest versions of Visual Studio.
https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio


### Usage

###Postman Collection
https://www.getpostman.com/collections/fb9c51000e35be16d017

## Getting Started

# Start the Server

- Click the drop down arrow next to the App Runner button and select `Catalyte.Apparel.API`
- Click Build > Build Solution
- Click the App Runner button

-- Connections

By default, this service starts up on port 8085 and accepts cross-origin requests from `*`.

Once you have started the API, you can create your CRUD requests. You can verify your requests work by using Postman. 

For a website, your fetch requests URLs need to match the URLs from the API.

## Linting

C# has base level Linting that does a good job making sure you cover your bases as far as syntax goes. 
You can lint documents one at atime by going to Edit/Advanced/FormatDocument or by using the keyboard shortcut, 
ctrl+k, ctrl+d. 

### Testing
Currently, there is no testing for this project.
 