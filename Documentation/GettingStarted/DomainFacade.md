# Domain Facade
Putting it all together
## Directory Structure
Modify the current directory structure as follows in your project directory.

```
|- MyProjectDirectory
|	|- DomainLayer
|	|	|- Connections
|	|	|	|- MyProjectSQLConnection.cs
|   |   |- Models
|   |   |   |- Data
|   |   |   |   |- SampleDataModel.cs
|   |   |   |- Parameter
|   |   |   |   |- GetSampleDataParameters.cs
|   |   |- Methods
|	|	|	|- MyProjectSQLMethods.cs
|   |   |- IDomainFacade.cs
|   |   |- DomainFacade.cs
```

## Add Domain Facade class

Create a `IDomainFacade.cs` interface file in `MyProjectDirectory/DomainLayer` as follows:
```csharp
public interface IDomainFacade
{
    public IDbResponse<SampleDataModel> GetSampleData(GetSampleDataParameters parameters);
}
```
Create a `DomainFacade` class file in `MyProjectDirectory/DomainLayer` as follows:

### As of Version 1.13.0

```csharp
internal class DomainFacade: IDomainFacade
{
    public DomainFacade(){
        // MyProjectSQLConnection must be configured before any calls can be made
        MyProjectSQLConnection.Configure(string connectionString);
    }

    public IDbResponse<SampleDataModel> GetSampleData(GetSampleDataParameters parameters)
    => MyProjectSQLMethods.GetSampleData.Execute(parameters);
}
```

### Version 1.12.1 and earlier

```csharp
internal class DomainFacade: IDomainFacade
{
    public DomainFacade(){
        string connectionString = "MyConnectionString"; // Get this value from a configuration source in real code;

        // MyProjectSQLConnection must be registered before any calls can be made
        MyProjectSQLConnection.RegisterConnection(string connectionString);
    }

    public IDbResponse<SampleDataModel> GetSampleData(GetSampleDataParameters parameters)
    => MyProjectSQLMethods.GetSampleData.Execute(parameters);
}
```

## Using Domain Facade 
You may now use 

```csharp
public class MyClass
{
    private IDomainFacade DomainFacade {get; set;}
    public MyClass(){
        DomainFacade = new DomainFacade();
    }

    public void DoSomething(){
        var results = DomainFacade.GetSampleData(new GetSampleDataParameters(){
            Guid = Guid.NewGuid(), // use a real Guid here not a new one( this is example only)
            String = "Some string value"
        });

        if(results.HasError){
            Exception error = result.Error;
            // Uh Oh! Need to handle error result.
        }else{
            // Everything is Good!
        }
    }
}
```

## Recomendations
Since your project code will likely call the DomainFacade object many times consider creating a global instance of it in your project.
In Core projects this can be done with dependancy injection.

## Core Specific Notes
When using the Core version of this library you may need to include some extra code in `Startup.cs` to configure the connection strings.
Please consult the official .NET Core doumentation about adding Connection strings for the latest information.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    ...
        
    services.AddConnectionStrings(connectionStrings =>
    {
        connectionStrings.MySQLConnectionString = connectionStrings.GetSqlConnection(
            Configuration.GetConnectionString("MySQLConnectionString"),
            Configuration["MySQLConnectionString:ProviderName"],
            builder => {
                builder.UserID = Configuration["MySQLConnectionString:UserID"];
                builder.Password = Configuration["MySQLConnectionString:Password"];
            });
    });
    ...
}
```