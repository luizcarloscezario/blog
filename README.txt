Challenge Statement

Blogs is a backend blogging application written by a startup company called WritingForAll. It allows users to create / update / delete their articles, accepting comments for each article.

Prerequisites
	- Any IDE
	- .NET Core SDK 2.1.4
	- Docker (follow this link to install https://store.docker.com/search?type=edition&offering=community)

=====================================
Development Environment
=====================================
MySQL:
- We assume that no MySQL is available in your machine, if it is running please stop it to avoid port conflicts, and use following command to run pre-configured MySQL

docker-compose up -d db

Cross-blogs application:
- On any terminal move to the "crossblog" folder (the folder containing the "crossblog.csproj" file) and execute these commands:

dotnet restore
dotnet build
dotnet ef database update
dotnet run

- The application will be listening on http://localhost:5000
- Now you can call the api using any tool, like Postman, Curl, etc (See "Some Curl command examples" section)

=====================================
Production Environment
=====================================
This is how we are going to run and evaluate your submission, so please make sure to run below steps before submit your answer.
If you add any new 3rd party application, please add it to docker-compose.yml file exactly like MySQL.

1) On any terminal move to the folder containing the "docker-compose.yml" file and execute this command:

docker-compose up -d

- The application will be listening on http://localhost:5000
- Now you can call the api using any tool, like Postman, Curl, etc (See "Some Curl command examples" section)

2) Please add your project to a zip file with the name cross-blogs-dotnet-<YOUR-FULL-NAME>.zip (use dash "-" instead of any space).

=====================================
To run unit tests
=====================================
- On any terminal move to the "crossblog.tests" folder (the folder containing the "crossblog.tests.csproj" file) and execute these commands:

dotnet restore
dotnet build
dotnet test

- To check code coverage, execute the batch script:

coverage.bat