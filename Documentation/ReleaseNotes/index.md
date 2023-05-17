# Release Notes

## 1.12.1
### Issues Resolved
- Resolved an issue with unit testing feature not properly handling annonomous objects as data row values

### Additional Notes
- bumped NuGet package dependancy versions

## 1.12.0

### Features Depreciated/Removed
- Depreciated .NET Core specific nuget package. Separate Non SQL Client connections into separate packages


## 1.11.0
### Features Added
- Added Access to raw C# 'DataSet' object
- Added support for defining a credential object for DbConnection types that support it

### Features Depreciated/Removed
- Depreciated GetDbConnectionProvider and GetDbConnectionProviderAsync methods from TDbConnectionConfig classes. Replaced with CreateDbConnection method which in most use cases will already be built in.

### Issues Resolved
- Resolved issiue with output parameter values being set to null when actual values exist

## 1.10.0
### Features Added
- Added Ability to define Db Schema command builder factories
- Added ability to fetch multiple data sets from one db call
- Added new methods to toggle running connections in Mock mode for unit testing 
- Added Error Details for responses to make it easier to debug issues

### Features Depreciated/Removed
- Depreciated IDbCommandMethod Mock/MockAsync calls
- Depreciated DbCommandConfigFactory<TDbConnectionConfig> method (moved to TDbConnectionConfig class)

### Issues Resolved
- Resolved unhandled exceptions thrown in the DbDataModel Init() method.
- Resolved unhandled exceptions thrown when getting output value from response.

## 1.9.0
`No Release Notes available at this time`

## 1.8.0
`No Release Notes available at this time`

## 1.7.0
`No Release Notes available at this time`

## 1.6.0
`No Release Notes available at this time`

## 1.5.0
`No Release Notes available at this time`

## 1.4.0
`No Release Notes available at this time`

## 1.3.0
`No Release Notes available at this time`

## 1.2.0
`No Release Notes available at this time`

## 1.1.0
`No Release Notes available at this time`

## 1.0.0
`No Release Notes available at this time`