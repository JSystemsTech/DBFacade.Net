# Define Models
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
```

## Add Parameter Model
Create a `GetSampleDataParameters.cs` class file in `MyProjectDirectory/DomainLayer/Models/Parameters` as follows:

```csharp
    public class GetSampleDataParameters
    {
        public Guid Guid { get; set; }
        public string String { get; set; }
    }
```

## Add Data Model
Create a `SampleDataModel.cs` class file in `MyProjectDirectory/DomainLayer/Models/Data` as follows:

```csharp
    public class SampleDataModel: DbDataModel
    {
        public Guid Guid { get; set; }
        public string String { get; set; }
        public bool Bool { get; set; }
        public int Integer { get; set; }

        protected override void Init()
        {
            Guid = GetColumn<Guid>("Guid");
            String = GetColumn<string>("String");
            Bool = GetColumn<bool>("Bool");
            Integer = GetColumn<int>("Integer");
        }

        // if you plan to use asynchronous programing you must add the following to support asynchronous model binding. 
        // Otherwise ignore this method override.
        protected override async Task InitAsync()
        {
            Guid = await GetColumnAsync<Guid>("Guid");
            String = await GetColumnAsync<string>("String");
            Bool = await GetColumnAsync<bool>("Bool");
            Integer = await GetColumnAsync<int>("Integer");
        }
    }
```