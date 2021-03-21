# Contact Management Demo

This is a Contact Management Demo created as job interview exercise.

## Development Environment Setup

### Prerequesites

The following need to be installed:
1. Visual Studio 2019
2. Node.js
3. npm
4. SQL Server (Development Edition or and Azure instance works)
5. Git

### Source

The source can be downloaded by using the following command:

> git checkout https://github.com/sourcevelocity-andy/cms-demo.git

### Database Setup

Create a database instance.

There is a SQL script in the root folder of the project named "Database Setup Script.sql." Run this script against the database instance and it will setup the required tables.

### Configuration

The application uses a connection string labeled "DefaultConnection" to access the database. In order to not expose database settings to the world, the connection string is stored in the secrets.json file.

This can be access by opening the project in Visual Studio, right-clicking the project, and selecting "Manager User Secrets." Add the following key (with the fields updated to reflect your SQL login):

> {
> 	"ConnectionStrings": {
> 		"DefaultConnection": "Server=localhost;Database=cms-demo;User Id=[cms-demo];Password=[password]"
> 	}
> }

### Execution

At this point the application should run without any problems.

