# Types

## With no expected Data

### No input parameters
```csharp
public static readonly IDbCommandMethod SomeParameterlessMethod
    = AdventureWorksConnection.SomeParameterlessMethod.CreateMethod();
```
### With input parameters
```csharp
public static readonly IDbCommandMethod<GetSampleDataParameters> GetSampleData
    = AdventureWorksConnection.GetSampleData.CreateMethod<GetSampleDataParameters>(
    p => {
        p.Add("Guid", p.Factory.Create(m => m.Guid));
        p.Add("String", p.Factory.Create(m => m.String));
    });
```
## With data expected

### No input parameters
```csharp
public static readonly IParameterlessDbCommandMethod<SampleDataModel> GetSampleData
    = AdventureWorksConnection.GetSampleData.CreateParameterlessMethod<SampleDataModel>();
```
### With input parameters
```csharp
public static readonly IDbCommandMethod<GetSampleDataParameters,SampleDataModel> GetSampleData
    = AdventureWorksConnection.GetSampleData.CreateMethod<GetSampleDataParameters,SampleDataModel>(
    p => {
        p.Add("Guid", p.Factory.Create(m => m.Guid));
        p.Add("String", p.Factory.Create(m => m.String));
    });
```