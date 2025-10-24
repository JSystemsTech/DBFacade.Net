#Parameters

##Input Parameters
```csharp
public static readonly IDbCommandMethod<Guid,SampleDataModel> GetSampleData
    = AdventureWorksConnection.GetSampleData.CreateMethod<Guid,SampleDataModel>(
    p => {
        p.Add("Guid", p.Factory.Create(m => m));
    });
```
##Output Parameters
```csharp
public static readonly IDbCommandMethod<Guid,SampleDataModel> GetSampleData
    = AdventureWorksConnection.GetSampleData.CreateMethod<Guid,SampleDataModel>(
    p => {
        p.Add("MyOutputGuidParam", p.Factory.CreateOutputGuid());
    });
```
## Non class Parameters 
```csharp
public static readonly IDbCommandMethod<Guid,SampleDataModel> GetSampleData
    = AdventureWorksConnection.GetSampleData.CreateMethod<Guid,SampleDataModel>(
    p => {
        p.Add("Guid", p.Factory.Create(m => m));
    });
```
```csharp
public static readonly IDbCommandMethod<(Guid guid, string str),SampleDataModel> GetSampleData
    = AdventureWorksConnection.GetSampleData.CreateMethod<(Guid guid, string str),SampleDataModel>(
    p => {
        p.Add("Guid", p.Factory.Create(m => m.guid));
        p.Add("String", p.Factory.Create(m => m.str));
    });
```
```csharp
public static readonly IDbCommandMethod<(Guid guid, string[] strArr),SampleDataModel> GetSampleData
    = AdventureWorksConnection.GetSampleData.CreateMethod<(Guid guid, string str),SampleDataModel>(
    p => {
        p.Add("Guid", p.Factory.Create(m => m.guid));
        p.Add("StringArr", p.Factory.Create(m => string.Join(",",m.strArr)));
    });
```