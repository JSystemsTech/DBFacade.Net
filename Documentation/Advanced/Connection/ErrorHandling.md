﻿# Error Handling
While DbResponse objects provide an `Error` property for Exceptions thrown in the method execution pipeline, 
the connection object also provides a means of capturing and handling exceptions.

## Why Use this option?
If you have an external error logging call that captures application errors 
this is a convienient way to capture those errors upfront without having to
replicate for each call on the DomainFacade file.

## Use Cases

### As of Version 1.13.0

```csharp
internal class MyProjectSQLConnection : SqlConnectionConfig<MyProjectSQLConnection>
{
    public static void Configure()
    {
        Configure(GetDbConnectionString, o => {
            o.SetOnErrorHandler(OnError);
            o.SetOnValidationError(OnValidationError);
            o.SetOnSQLExecutionError(OnSQLExecutionError);
            o.SetOnFacadeError(OnFacadeError);
        });
    }
    
    //Handle any Error
    //Generally the most common one to use
    private static void OnError(Exception ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    //Handle method Validation Errors
    private static void OnValidationError<TDbParams>(ValidationException<TDbParams> ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    //Handle SQL execution Errors
    private static void OnSQLExecutionError(SQLExecutionException ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    //Handle rare Facade Error. 
    //This shouldn't normally be hit all that often when an error occurs, so this is best used in debug mode.
    private static void OnFacadeError(FacadeException ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    ...
}
```
If your code Executes methods asynchronsly you will need to define the async error handler(s).


```csharp
internal class MyProjectSQLConnection : SqlConnectionConfig<MyProjectSQLConnection>
{
    public static void Configure()
    {
        Configure(GetDbConnectionString, o => {
            o.SetOnErrorHandlerAsync(OnErrorAsync);
            o.SetOnValidationErrorAsync(OnValidationErrorAsync);
            o.SetOnSQLExecutionErrorAsync(OnSQLExecutionErrorAsync);
            o.SetOnFacadeErrorAsync(OnFacadeErrorAsync);
        });
    }
    //Handle any Error
    //Generally the most common one to use
    private static async Task OnErrorAsync(Exception ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    //Handle method Validation Errors
    private static async Task OnValidationErrorAsync<TDbParams>(ValidationException<TDbParams> ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    //Handle SQL execution Errors
    private static async Task OnSQLExecutionErrorAsync(SQLExecutionException ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    //Handle rare Facade Error. 
    //This shouldn't normally be hit all that often when an error occurs, so this is best used in debug mode.
    private static async Task OnFacadeErrorAsync(FacadeException ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    ...
}
```

### Version 1.12.1 and earlier

```csharp
internal class MyProjectSQLConnection : SqlConnectionConfig<MyProjectSQLConnection>
{
    ...
    //Handle any Error
    //Generally the most common one to use
    protected override void OnError(Exception ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    //Handle method Validation Errors
    protected override void OnValidationError<TDbParams>(ValidationException<TDbParams> ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    //Handle SQL execution Errors
    protected override void OnSQLExecutionError(SQLExecutionException ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    //Handle rare Facade Error. 
    //This shouldn't normally be hit all that often when an error occurs, so this is best used in debug mode.
    protected override void OnFacadeError(FacadeException ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    ...
}
```
If your code Executes methods asynchronsly you will need to define the async error handler(s).


```csharp
internal class MyProjectSQLConnection : SqlConnectionConfig<MyProjectSQLConnection>
{
    ...
    //Handle any Error
    //Generally the most common one to use
    protected override async Task OnErrorAsync(Exception ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    //Handle method Validation Errors
    protected override async Task OnValidationErrorAsync<TDbParams>(ValidationException<TDbParams> ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    //Handle SQL execution Errors
    protected override async Task OnSQLExecutionErrorAsync(SQLExecutionException ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    //Handle rare Facade Error. 
    //This shouldn't normally be hit all that often when an error occurs, so this is best used in debug mode.
    protected override async Task OnFacadeErrorAsync(FacadeException ex, IDbCommandSettings commandSettings) { 
        //Run custom logic to log error
    }
    ...
}
```

## Recomendation
Generally you should only need to override the `OnError` / `OnErrorAsync` methods to do the bulk of error handling.
But if you need to run specialized error handling for SQL exceptions or validation errors, 
or if you only want to handle specific types of exceptions then the other methods are available.