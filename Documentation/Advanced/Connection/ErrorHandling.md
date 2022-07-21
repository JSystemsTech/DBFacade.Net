# Error Handling
While DbResponse objects provide an `Error` property for Exceptions thrown in the method execution pipeline, 
the connection object also provides a means of capturing and handling exceptions.

## Why Use this option?
If you have an external error logging call that captures application errors 
this is a convienient way to capture those errors upfront without having to
replicate for each call on the DomainFacade file.

## Use Cases

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