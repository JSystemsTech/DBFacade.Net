# Declare Methods
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
```

## Add Methods class
Create a `MyProjectSQLMethods.cs` class file in `MyProjectDirectory/DomainLayer/Methods` as follows:

```csharp
    internal class MyProjectSQLMethods
    {
        public static readonly IDbCommandMethod<GetSampleDataParameters,SampleDataModel> GetSampleData
            = AdventureWorksConnection.GetSampleData.CreateMethod<GetSampleDataParameters,SampleDataModel>(
            p => {
                p.Add("Guid", p.Factory.Create(m => m.Guid));
                p.Add("String", p.Factory.Create(m => m.String));
            });
    }
```
When mapping parameters to the method like `p.Add("Guid", p.Factory.Create(m => m.Guid));` notice that you do not need
to add the leading `@` expected by SQL stored procedures. This is automaticlly resolved before the method is executed.
