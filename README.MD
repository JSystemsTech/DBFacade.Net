# DbFacade
[![.NET](https://github.com/JSystemsTech/DBFacade.Net/actions/workflows/NetFxCI.yml/badge.svg?branch=master)](https://github.com/JSystemsTech/DBFacade.Net/actions/workflows/NetFxCI.yml)
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
#### PostgreSQL
```bash
Install-Package DbFacade.PostgreSQL
```

## Documentation
Read full Documentation [Here](https://jsystemstech.github.io/DBFacade.Net/index.html)

## Utilities 
DbFacade depends on the NuGet Package DbFacade.Utils.
DbFacade.Utils is a set of Utilities useful for data parseing. 

### Installation
```bash
Install-Package DbFacade.Utils
```
The latest version can also be downloaded directly from NuGet.org [Here](https://www.nuget.org/packages/DbFacade.Utils/)

### Documentation
Read full Documentation [Here](https://jsystemstech.github.io/DBFacade.Net/index.html)




 