#Nested Models
Sometimes you will have to design data models with inner model nesting.
DbFacade supports that design pattern.

```csharp
public class MyDataModel : DbDataModel
{
    public MyNestedDataModel MyNestedDataModel { get; private set; }

    protected override void Init()
    {
        MyNestedDataModel = CreateNestedModel<MyNestedDataModel>();
    }
    protected override async Task InitAsync()
    {
        MyNestedDataModel = await CreateNestedModelAsync<MyNestedDataModel>();
    }
}
public class MyNestedDataModel : DbDataModel
{
    public string Value { get; private set; }

    protected override void Init()
    {
        Value = GetColumn<string>("ColumnName");
    }
    protected override async Task InitAsync()
    {
        Value = await GetColumnAsync<string>("ColumnName");
    }
}
```