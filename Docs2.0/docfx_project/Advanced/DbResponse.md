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
## Additional DataSets
If you stroed procedure has multiple data sets returning you can access the additional data sets as follows:
```csharp
int index = 1; //index 0 is always the first data set and is already a part of the response;
IEnumerable<MyDataModel> data = MyDbResonse.DataSets.ElementAt(index).ToDbDataModelList<MyDataModel>();
```
or with `async`
```csharp
int index = 1; //index 0 is always the first data set and is already a part of the response;
IEnumerable<MyDataModel> data = await MyDbResonse.DataSets.ElementAt(index).ToDbDataModelListAsync<MyDataModel>();
```

## Raw DataSet Object
If you have a need for accessing the raw C# 'DataTable' type objects you can access them in the following way:
```csharp
int index = 1; //index 0 is always the first data set and is already a part of the response;
IEnumerable<MyDataModel> data = MyDbResonse.DataSets.ElementAt(index).ToDbDataModelList<MyDataModel>();
```
Note that this is the raw data response from the database call. You do not have access to any of the specialized column parser extensions that the 'DbDataModel' type class provides.

This is especially useful if you are importing data into another service.
Ex. the Aspose libraries make use of a DataTable to construct a MS Office table object.

## Error
If the method execution call fires an exception the error is captured and set in the `Error` property and the `HasError` property  is set to `true`.

## Error Message
If the method execution call fires an exception the error is captured and the `ErrorMessage` property is set to the value of the `Message` property of the error

## Error Details
If the method execution call fires an exception the error is captured and is of type `FacadeException` the `ErrorDetails` property is set to the value of the `ErrorDetails` property of the error

## HasDataBindingErrors
If any of the data models have data binding errors the `HasDataBindingErrors` will be set to `true`
