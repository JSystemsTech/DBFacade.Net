# Command Configurations

## Fetch Command
Add a SQL stored procedure command definition to class `MyProjectSQLConnection` as a static property as follows
```csharp
    public static IDbCommandConfig GetSampleData = CreateFetchCommand("[dbo].[SampleData_Get]", "Get Sample Data");
```

## Transaction Command
If you have a stored procedure that you want to execute as a transaction then you can declare it as follows.
<div class="NOTE">
  <h5>NOTE</h5>
  <p>Transaction commands parameter validation is enforced on methods.</p>
</div>

```csharp
public static IDbCommandConfig GetSampleData = CreateTransactionCommand("[dbo].[My_Transaction]", "My Transaction);
```

<div class="WARNING">
  <h5>WARNING</h5>
  <p>Some Db connection types do not support transations. Please consult appropriate ADO.NET documentation for more information.</p>
  <p>If transactions are not supported then use a fetch command instead</p>
</div>

## Schema Command Config Factories 
Add a IDbCommandConfigSchemaFactory object definition to class `MyProjectSQLConnection` as a static property, 
and then add a SQL stored procedure command definition to class `MyProjectSQLConnection` as a static property as follows
```csharp
    private static IDbCommandConfigSchemaFactory MySchema = CreateSchemaFactory("MySchema");
    public static IDbCommandConfig GetOtherSampleData = MySchema.CreateFetchCommand("SampleData_Get_Other", "Get Other Sample Data");
```

Why use this over calling the standard 'MyProjectSQLConnection.CreateFetchCommand'?
It is up to you which you which option you prefer. Both are valid, but here are some of the benefits
1.  You define the target schema name once.
2.  You only need to worry about the name of the stored procedure
3.  The command text will automatically be fully qualified and formated 
    AKA `MySchema.CreateFetchCommand("SampleData_Get_Other", "Get Other Sample Data")` calculates the command text to `[MySchema].[SampleData_Get_Other]`
4.  Less time spent on writing out the full command text especially if you have many calls to define
5.  The default schema for most databases is `dbo` so that Factory instance is already available to use. 

```csharp
    public static IDbCommandConfig GetSampleData = Dbo.CreateFetchCommand("SampleData_Get", "Get Sample Data");
```   