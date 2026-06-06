Repo contains 2 solutions:

1. Cros folder for Customer Registration & Onboarding System
2. BrewCoffee folder for brew-coffee API

How to run:

Cros

a. Using VS2026
  - Open solution in VS2026
  - Make sure Cros project is Startup Project (it should be)
  - Press Run in toolbar using http or IIS Express  
  - For tests, just run VS test runner
  
b. Using cmd
  - Open cmd
  - Change directory to where Cros project resides
  - Type "dotnet run"
  - Note the domain and port it's running on and type/paste it in your browser
  - For tests, change directory to where test project resides and type "dotnet test"
  
  
BrewCoffee

There are 2 runnable projects in BrewCoffee, one without weather checking and one with weather checking.

a. Using VS2026
  - Open solution in VS2026
  - Choose the project you wish to run and make it as Startup Project
  - Press Run in toolbar using http or IIS Express  
  - For tests, just run VS test runner
  
b. Using cmd
  - Open cmd
  - Change directory to the project you wish to run
  - Type "dotnet run"
  - Note the domain and port it's running on and type/paste it in your browser and add "/brew-coffee" for path
  - For tests, change directory to where test project resides and type "dotnet test"
  
  
 
