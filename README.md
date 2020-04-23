# ExVi - mice-exploration-tool
ExVi is visual data exploration tool for musculoskeletal phenotyping in mice - built with ASP.NET, CornerstoneJS, MySQL and Json.NET.

This repository is the work of University of Lincoln's, Level 2, TSE Group 34. Creating a data visualisation tool for DICOM images to support the work of the IMPC, for Assignment 2 of the Team Software Engineering module.

[![Build Status](https://travis-ci.com/laurencebrwn/mice-exploration-tool.svg?token=sX5CMpv3R8hjH5qNNqFT&branch=master)](https://travis-ci.com/laurencebrwn/mice-exploration-tool)

## ExVi - Demo WebApp
[The ExVi Demo WebApp](https://github.com/laurencebrwn/mice-exploration-tool/tree/demo) is a lightweight version of ExVi for demo use. 

The ExVi Demo WebApp can be accessed from any device anywhere in the world [here](https://met.azurewebsites.net/).

Limitations of the Demo  WebApp include:
* No DICOM viewer.
* Limited dataset of only 40 mice - (Populated by IMPC's SOLR API).
* No visual breakdown of the dataset - (Inluding charting of results and entire database).

## ExVi - Full App
[The ExVi Full App](https://github.com/laurencebrwn/mice-exploration-tool/) is the full locally run solution to explore your own datasets from the IMPC. Please follow the instructions below on how to get it set up and running.

### Installation
1. Download the latest release from [here](https://github.com/laurencebrwn/mice-exploration-tool/releases).

1. ExVi requires a locally hosted MySQL database to operate.
    1. Download MySQL Installer for Windows from [here](https://dev.mysql.com/downloads/) and install.
    1. You will need to create a user and password for your database. The default the details which ExVi uses are, ```'user=root'``` and ```'password=TSEGroup34'```. If these values change for the user then the program will need to be changed accordingly. These values are used to create a connection string to the local MySql database.
    1. You will need to SymLink your data storage directory so that the program can retrieve your DICOM images and JSON metadata to populate the database. The name of the symlink dir needs to be 'dicomImages'. If these values change for the user then the program will need to be changed accordingly.
    1. Details on how to create a SymLink are [here](https://www.shellhacks.com/symlink-create-symbolic-link-linux/).
            * A example command is as follows: 
            
                    $ ln -s /path/to/original /path/to/symlink
    
1. Download and install the latest release of the .NET runtime from [here](https://dotnet.microsoft.com/download).

1. Once the MySQL database is running and the .NET runtime is installed on your machine, you may then launch ExVi.
    1. Open up a terminal and navigate to ExVi's directory.
    1. Run the command: ```dotnet run```
    1. Using your browser visit: ```localhost:5001```
    
1. Once the program is running in your browser you will need to populate the database. This is done automatically by clicking on the ```UPDATE-DATABASE``` menu option on the ```HOME``` page.

## Acknowledgements
* [ASP.NET](https://dotnet.microsoft.com/apps/aspnet) - Microsoft's open source framework for building web apps and services with .NET and C#.
* [CornerstoneJS](https://github.com/cornerstonejs/cornerstone) - The DICOM images and parsing tools to support the site is the excellent JS library by Chris Hafey and the Cornerstone tools.
* [Dexie.JS](https://dexie.org/) - The online JS database tool, used in the Demo WebApp, allows complex querying and long term client storage of the sample dataset.
* [MySQL](https://www.mysql.com/) - World's most popular open source database, used for the storing, sorting and refining of mice in the full app.
* [Json.NET](https://www.newtonsoft.com/json) - A high-performance JSON framework for .NET, used for importing and processing user datasets in the full app.



