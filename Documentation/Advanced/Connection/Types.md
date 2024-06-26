﻿#Types
DbFacade supports diferent connection types depending on the type of database you are connecting to.

## Built-in Types

### SQL/SQL Server
```csharp
internal class MyProjectSQLConnection : SqlConnectionConfig<MyProjectSQLConnection>
{
    ...
}
```

### SQLite
```csharp
internal class MyProjectSQLiteConnection : SqLiteConnectionConfig<MyProjectSQLiteConnection>
{
    ...
}
```

### OleDb
```csharp
    internal class MyProjectOleDbConnection : OleDbConnectionConfig<MyProjectOleDbConnection>
    {
        ...
    }
```

### Odbc
```csharp
internal class MyProjectOdbcConnection : OdbcConnectionConfig<MyProjectOdbcConnection>
{
    ...
}
```

### Oracle
```csharp
internal class MyProjectOracleConnection : OracleConnectionConfig<MyProjectOracleConnection>
{
    ...
}
```
## Custom Connection Type
If you are trying to connect to a database type that is not listed above you may define your own connection type.

### Create Base abstract class
```csharp
public abstract class CustomConnectionConfig<TDbConnectionConfig> : DbConnectionConfigFull<TDbConnectionConfig>
where TDbConnectionConfig : IDbConnectionConfig 
{
    public static void Configure(OnGetConnectionString getConnectionString, Action<IErrorHandlerOptions> handler)
    {
        handler(GetConnectionOptions().ErrorHandlerOptions);
        GetConnectionOptions().SetOnGetDbDataAdapter(() => new CustomDataAdapter());
        GetConnectionOptions().SetOnGetConnectionString(getConnectionString);
        GetConnectionOptions().SetOnCreateDbConnection(connectionString => new CustomDbConnection(connectionString));
    }
}
```
If the database type does not support transactions use the following.
```csharp
public abstract class CustomConnectionConfig<TDbConnectionConfig> : DbConnectionConfigNoTransaction<TDbConnectionConfig>
where TDbConnectionConfig : IDbConnectionConfig 
{
    public static void Configure(OnGetConnectionString getConnectionString, Action<IErrorHandlerOptions> handler)
    {
        handler(GetConnectionOptions().ErrorHandlerOptions);
        GetConnectionOptions().SetOnGetDbDataAdapter(() => new CustomDataAdapter());
        GetConnectionOptions().SetOnGetConnectionString(getConnectionString);
        GetConnectionOptions().SetOnCreateDbConnection(connectionString => new CustomDbConnection(connectionString));
    }
}
```

### Create the Connection class
```csharp
internal class MyProjectCustomConnection : CustomConnectionConfig<MyProjectCustomConnection>
{
    ...
}
```