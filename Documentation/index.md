# DbFacade
[![.NET](https://github.com/JSystemsTech/DBFacade.Net/actions/workflows/NetFxCI.yml/badge.svg?branch=master)](https://github.com/JSystemsTech/DBFacade.Net/actions/workflows/NetFxCI.yml)
 [![NuGet](https://img.shields.io/nuget/v/DbFacade?label=nuget%20DbFacade)](https://nuget.org/packages/DbFacade) [![NuGet](https://img.shields.io/badge/Target%20Framework-.NET%20Framework%204.8-blue)](https://nuget.org/packages/DbFacade)
## About

Confused about how C# .NET projects set up calls to SQL databases? Need a well structured design pattern to organize calls to database stored procedures? 

Then `DbFacade` is the solution! DbFacade builds upon `ADO.NET` and simplifies structuring calls to SQL database stored procedures.

## Features
* Simplified parameter binding
* Built-In configurable parameter validation
* Easy data model binding
* Supports syncronous or asyncronous methods
* Supports .Net Framework and .Net Core projects 

## Installation

```bash
Install-Package DbFacade
```
The latest version can also be downloaded directly from NuGet.org [Here](https://www.nuget.org/packages/DbFacade/)

### Connection Type Helpers
For support with the following data base types please additionally install the following helpers 
#### Oracle
```bash
Install-Package DbFacade.Oracle
```
#### SQLite
```bash
Install-Package DbFacade.SQLite
```
#### Odbc
```bash
Install-Package DbFacade.Odbc
```
#### OleDb
```bash
Install-Package DbFacade.OleDb
```

## Documentation
For the latest README updates and documentation please visit [Here](https://github.com/JSystemsTech/DBFacade.Net) 

## Utilities 
DbFacade depends on the NuGet Package DbFacade.Utils.
DbFacade.Utils is a set of Utilities useful for data parsing. 

### Installation
```bash
Install-Package DbFacade.Utils
```
The latest version can also be downloaded directly from NuGet.org [Here](https://www.nuget.org/packages/DbFacade.Utils/)

### Documentation
Read full Documentation [Here](https://jsystemstech.github.io/DBFacade.Net/index.html)