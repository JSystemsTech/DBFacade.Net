# Setup Connection
## Directory Structure
Create a directory structure as follows in your project directory.

```
|- MyProjectDirectory
|	|- DomainLayer
|	|	|- Connections
|	|	|	|- MyProjectSQLConnection.cs
```

## Add Connection file
Create a `MyProjectSQLConnection.cs` class file in `MyProjectDirectory/DomainLayer/Connections` as follows:

```csharp
    internal class MyProjectSQLConnection : SqlConnectionConfig<MyProjectSQLConnection>
    {
        private string ConnectionString { get; set; }
        private string ProviderName { get; set; }

        private MyProjectSQLConnection(string connectionString, string providerName) 
        { 
            ConnectionString = connectionString;
            ProviderName = providerName;
        }        
        
        protected override string GetDbConnectionString() => ConnectionString;
        protected override string GetDbConnectionProvider() => ProviderName;

        protected override async Task<string> GetDbConnectionStringAsync()
        {
            await Task.CompletedTask;
            return ConnectionString;
        }

        protected override async Task<string> GetDbConnectionProviderAsync()
        {
            await Task.CompletedTask;
            return ProviderName;
        }

        public static void RegisterConnection(string connectionString, string providerName) 
        => new MyProjectSQLConnection(connectionString, providerName).Register();
    }
```

## Add Command Configurations
Add a SQL stored procedure command definition to class `MyProjectSQLConnection` as a static property as follows
```csharp
    public static IDbCommandConfig GetSampleData = DbCommandConfigFactory<MyProjectSQLConnection>.CreateFetchCommand("[dbo].[SampleData_Get]", "Get Sample Data");
```

## Complete Class Code

```csharp
    internal class MyProjectSQLConnection : SqlConnectionConfig<MyProjectSQLConnection>
    {
        private string ConnectionString { get; set; }
        private string ProviderName { get; set; }

        private MyProjectSQLConnection(string connectionString, string providerName) 
        { 
            ConnectionString = connectionString;
            ProviderName = providerName;
        }        
        
        protected override string GetDbConnectionString() => ConnectionString;
        protected override string GetDbConnectionProvider() => ProviderName;

        protected override async Task<string> GetDbConnectionStringAsync()
        {
            await Task.CompletedTask;
            return ConnectionString;
        }

        protected override async Task<string> GetDbConnectionProviderAsync()
        {
            await Task.CompletedTask;
            return ProviderName;
        }

        public static void RegisterConnection(string connectionString, string providerName) 
        => new MyProjectSQLConnection(connectionString, providerName).Register();

        public static IDbCommandConfig GetSampleData = DbCommandConfigFactory<MyProjectSQLConnection>.CreateFetchCommand("[dbo].[SampleData_Get]", "Get Sample Data");
    }
```

