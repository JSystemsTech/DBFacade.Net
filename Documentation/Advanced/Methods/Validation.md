#Validation
```csharp
public static readonly IDbCommandMethod<GetSampleDataParameters,SampleDataModel> GetSampleData
    = AdventureWorksConnection.GetSampleData.CreateMethod<GetSampleDataParameters,SampleDataModel>(
    p => {
        p.Add("Guid", p.Factory.Create(m => m.Guid));
        p.Add("String", p.Factory.Create(m => m.String));
    },
    v => {
        v.Add(v.Rules.Required(m => m.String));
    });
```
For doumentation on all available rules see [Rules](~/api/DbFacade.Factories.ValidationRuleFactory-1.html)