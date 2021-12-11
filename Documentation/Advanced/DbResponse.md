# DbResponse
This is the default response object from method executions. 
`DbRespnse<TDbDataModel>` is also an IEnumerable<TDbDataModel> which returns a fetched dataset.

## Return Value
If you need to access a stored procedures return value you can call the `ReturnValue` property of the object.

## Output Variables
Some stored procedures define output variables which return on the call.
To access those values you can use the following:

###Single Parmameter Value 
```csharp
T value = MyDbResonse.GetOutputValue<T>(string key);
```
or with `async`
```csharp
T value = await MyDbResonse.GetOutputValueAsync<T>(string key);
```

###Multiple Values as DbDataModel 
```csharp
public class MyOutputDataModel : DbDataModel
{
    public string Value { get; private set; }

    protected override void Init()
    {
        Value = GetColumn<string>("OutputParameterName");
    }
    protected override async Task InitAsync()
    {
        Value = await GetColumnAsync<string>("ColumnName");
    }
}
```
```csharp
MyOutputDataModel model = MyDbResonse.GetOutputModel<MyOutputDataModel>();
```
or with `async`
```csharp
MyOutputDataModel model = await MyDbResonse.GetOutputModelAsync<MyOutputDataModel>();
```

## Error
If the method execution call fires an exception the error is captured and set in the `Error` property and the `HasError` property  is set to `true`.

## HasDataBindingErrors
If any of the data models have data binding errors the `HasDataBindingErrors` will be sert to `true`
