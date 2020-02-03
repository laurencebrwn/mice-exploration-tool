# asp-mice-exploration-tool
Visual Data Exploration tool for Musculoskeletal Phenotyping in mice.

# Repo Details
This repository is the work of the Group 34 TSE team creating a data visualisation tool for DICOM images to support the work of the IMPC. 

# How to run experimental web app
cd into directory

terminal - dotnet 

visit localhost:5000

# Database details
The database is in MySql and is hosted locally (i.e. a users machine). The user will need to install MySql for their machine and create a user and password. Currently, 'user=root' and 'password=TSEGroup34'. If these values change for the user then the program will need to be changed accordingly. These values are used to create a connection string to the local MySql database. 

The user will then be required to upload the miceSchema.sql file to create the database called 'MICE' with its associated tables and values. A query will then be applied to this datbase using the c# program to find all female mice within the database and return their associated URLs. These URLs could be used to fetch the DICOM images ?

Additional requirements for connection to MySql using Visual Studio .net core is adding a Nuget called 'MySql.Data'. This can be done by opening the miceDatabaseConnection.sln in VS (not VS Code), right clicking on 'dependencies > add NuGet > search MySql.Data > apply.

# Acknowledgements
The DICOM images and parsing tools to support the site is the excellent JS library by Chris Hafey and the Cornerstone tools.

https://github.com/cornerstonejs/cornerstone

