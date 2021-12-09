# Command Configurations

## Fetch Command
Add a SQL stored procedure command definition to class `MyProjectSQLConnection` as a static property as follows
```csharp
    public static IDbCommandConfig GetSampleData = DbCommandConfigFactory<MyProjectSQLConnection>.CreateFetchCommand("[dbo].[SampleData_Get]", "Get Sample Data");
```

## Transaction Command
If you have a stored procedure that you want to execute as a transaction then you can declare it as follows.
<div class="NOTE">
  <h5>NOTE</h5>
  <p>Transaction commands parameter validation is enforced on methods.</p>
</div>

```csharp
public static IDbCommandConfig GetSampleData = DbCommandConfigFactory<MyProjectSQLConnection>.CreateTransactionCommand("[dbo].[My_Transaction]", "My Transaction);
```

<div class="WARNING">
  <h5>WARNING</h5>
  <p>Some Db connection types do not support transations. Please consult appropriate ADO.NET documentation for more information.</p>
  <p>If transactions are not supported then use a fetch command instead</p>
</div>