#Getting Column Values

## Basic GetColumn<T>
Generally this is what you will need to use to get values from a data row

```csharp
GetColumn<string>("ColumnName");
```
Generally you can use any type T as long as it is a struct or a class  

### Async
```csharp
await GetColumnAsync<string>("ColumnName");
```

##Special
### IGetEnumerableColumn
Sometimes you have values returned as a delimited string that needs to be translated into an enumerable.
We have that covered for you already with `GetEnumerableColumn<T>(string columnName)`!

```csharp
    GetEnumerableColumn<string>("ColumnName");
```
```csharp
    GetEnumerableColumn<int>("IntColumnName");
```
By default the assumed delimeter is `","` but you can pass in any string as the second parameter if necessary

```csharp
    GetEnumerableColumn<string>("ColumnName",";");
```

### GetFlagColumn
Typically most boolean column values are sent from the database as a `BIT` (a `bool` in `C#`).
But Sometimes you may be dealing with a legacy system where the values coing back are not type `BIT` but treated like a such.
In that case we have the solution for that with `GetFlagColumn<T>(string col, T trueValue)`

```csharp
    GetFlagColumn("FlagColumnName", 'Y');
```
### GetDateTimeColumn
Generally to get a datetime from the database using `GetColumn<DateTime>("ColumnName")` will suffice,
but incase there are any specialized date formatting on the stored value in the database the following Helpers are available.

```csharp
    GetDateTimeColumn(string col, string format, DateTimeStyles style = DateTimeStyles.None)
```
```csharp
    GetDateTimeColumn(string col, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
```
### GetFormattedDateTimeStringColumn
Have a DateTime value in the database but only need a `string` formatted version of the value?
We have you covered!

```csharp
    GetFormattedDateTimeStringColumn(string col, string format)
```

### Async
All of the helpers above have `async` versions to use in `InitAsync` 
```csharp
    await GetEnumerableColumnAsync<string>("ColumnName");
```
```csharp
    await GetFlagColumnAsync("FlagColumnName", 'Y');
```
```csharp
    await GetDateTimeColumnAsync(string col, string format, DateTimeStyles style = DateTimeStyles.None)
```
```csharp
    await GetFormattedDateTimeStringColumnAsync(string col, string format)
```

## Data Binding Errors
When building data models if there are exceptions thrown when using any of the `GetColumn` helpers the error message is collected and stored
in the `DataBindingErrors` model property. 
This is espacially helpful durring debugging if you need to identify if you have incorrectly accessed a column value;

## Using same Model for multiple calls
If you are using the same model on multiple calls, its best practice to make sure that the column names remain the same on each call.
However if that is not the case you can consume the `CommandId` property to dictate which column name to use.

```csharp
    GetColumn<string>(CommandId == MyProjectSQLConnection.GetSampleDataAlt.CommandId ? "ColumnNameAlt" : "ColumnName");
```