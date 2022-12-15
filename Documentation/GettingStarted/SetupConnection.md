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

        private MyProjectSQLConnection(string connectionString) 
        { 
            ConnectionString = connectionString;
        }        
        
        protected override string GetDbConnectionString() => ConnectionString;

        protected override async Task<string> GetDbConnectionStringAsync()
        {
            await Task.CompletedTask;
            return ConnectionString;
        }

        public static void RegisterConnection(string connectionString) 
        => new MyProjectSQLConnection(connectionString).Register();
    }
```

## Add Command Configurations
Add a SQL stored procedure command definition to class `MyProjectSQLConnection` as a static property as follows
```csharp
    public static IDbCommandConfig GetSampleData = CreateFetchCommand("[dbo].[SampleData_Get]", "Get Sample Data");
```

## Complete Class Code

```csharp
    internal class MyProjectSQLConnection : SqlConnectionConfig<MyProjectSQLConnection>
    {
        private string ConnectionString { get; set; }

        private MyProjectSQLConnection(string connectionString) 
        { 
            ConnectionString = connectionString;
        }        
        
        protected override string GetDbConnectionString() => ConnectionString;

        protected override async Task<string> GetDbConnectionStringAsync()
        {
            await Task.CompletedTask;
            return ConnectionString;
        }

        public static void RegisterConnection(string connectionString) 
        => new MyProjectSQLConnection(connectionString).Register();

        public static IDbCommandConfig GetSampleData = CreateFetchCommand("[dbo].[SampleData_Get]", "Get Sample Data");
    }
```

## Speciallized Setup for Connection types that support for a credential object 
'OracleConnectionConfig' and 'SqlConnectionConfig' types support an aditional optional credendial object  

Add a SQL stored procedure command definition to class `MyProjectSQLConnection` as a static property as follows
```csharp
    internal class MyProjectSQLConnection : SqlConnectionConfig<MyProjectSQLConnection>
    {
        private string ConnectionString { get; set; }        
        private SqlCredential Credential { get; set; }

        private MyProjectSQLConnection(string connectionString, SqlCredential credential) 
        { 
            ConnectionString = connectionString;
            Credential = credential;
        }    
        
        protected override SqlCredential GetCredential() => credential;
        
        protected override string GetDbConnectionString() => ConnectionString;

        protected override async Task<string> GetDbConnectionStringAsync()
        {
            await Task.CompletedTask;
            return ConnectionString;
        }

        public static void RegisterConnection(string connectionString, SqlCredential credential) 
        => new MyProjectSQLConnection(connectionString, credential).Register();

        public static IDbCommandConfig GetSampleData = CreateFetchCommand("[dbo].[SampleData_Get]", "Get Sample Data");
    }

    internal class MyProjectOracleConnection : OracleConnectionConfig<MyProjectOracleConnection>
    {
        private string ConnectionString { get; set; }
        private OracleCredential Credential { get; set; }

        private MyProjectSQLConnection(string connectionString, OracleCredential credential) 
        { 
            ConnectionString = connectionString;
            Credential = credential;
        }    
        
        protected override OracleCredential GetCredential() => Credential;
        
        protected override string GetDbConnectionString() => ConnectionString;

        protected override async Task<string> GetDbConnectionStringAsync()
        {
            await Task.CompletedTask;
            return ConnectionString;
        }

        public static void RegisterConnection(string connectionString, OracleCredential credential) 
        => new MyProjectSQLConnection(connectionString, credential).Register();

        public static IDbCommandConfig GetSampleData = CreateFetchCommand("[dbo].[SampleData_Get]", "Get Sample Data");
    }
```

