# ExVi - mice-exploration-tool
[![Build Status](https://travis-ci.com/laurencebrwn/mice-exploration-tool.svg?token=sX5CMpv3R8hjH5qNNqFT&branch=master)](https://travis-ci.com/laurencebrwn/mice-exploration-tool)

ExVi is visual data exploration tool for musculoskeletal phenotyping in mice - built with ASP.NET, CornerstoneJS, MySQL and Json.NET.

This repository is the work of University of Lincoln's, Level 2, TSE Group 34. Creating a data visualisation tool for DICOM images to support the work of the IMPC, for Assignment 2 of the Team Software Engineering module.

# ExVi - Demo WebApp
[The ExVi Demo WebApp](https://github.com/laurencebrwn/mice-exploration-tool/tree/demo) is a lightweight version of ExVi for demo use. 

The ExVi Demo WebApp can be accessed from any device anywhere in the world [here](https://met.azurewebsites.net/).

Limitations of the Demo  WebApp include:
* No DICOM viewer
* Limited dataset of only 40 mice - (Populated by IMPC's SOLR API)
* No visual breakdown of the dataset - (Inluding charting of results and entire database)

# ExVi - Full App
[The ExVi Full App](https://github.com/laurencebrwn/mice-exploration-tool/) is the full locally run solution to explore your own datasets from the IMPC. Please follow the instructions below on how to get it set up and running.

### Installation
cd into directory

terminal - dotnet run

visit localhost:5000   << This may show you an error due to HTTPS being required. 

The database is in MySql and is hosted locally (i.e. a users machine). The user will need to install MySql for their machine and create a user and password. Currently, 'user=root' and 'password=TSEGroup34'. If these values change for the user then the program will need to be changed accordingly. These values are used to create a connection string to the local MySql database. 

The user will then be required to upload the miceSchema.sql file to create the database called 'MICE' with its associated tables and values. A query will then be applied to this datbase using the c# program to find all female mice within the database and return their associated URLs. These URLs could be used to fetch the DICOM images ?

Additional requirements for connection to MySql using Visual Studio .net core is adding a Nuget called 'MySql.Data'. This can be done by opening the miceDatabaseConnection.sln in VS (not VS Code), right clicking on 'dependencies > add NuGet > search MySql.Data > apply.

# Acknowledgements
* [ASP.NET](https://dotnet.microsoft.com/apps/aspnet) - Microsoft's open source framework for building web apps and services with .NET and C#.
* [CornerstoneJS](https://github.com/cornerstonejs/cornerstone) - The DICOM images and parsing tools to support the site is the excellent JS library by Chris Hafey and the Cornerstone tools.
* [Dexie.JS](https://dexie.org/) - The online JS database tool, used in the Demo WebApp, allows complex querying and long term client storage of the sample dataset.
* [MySQL](https://www.mysql.com/) - World's most popular open source database, used for the storing, sorting and refining of mice in the full app.
* [Json.NET](https://www.newtonsoft.com/json) - A high-performance JSON framework for .NET, used for importing and processing user datasets in the full app.



