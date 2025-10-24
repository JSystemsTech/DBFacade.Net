#Validation
```csharp
public static readonly IDbCommandMethod<GetSampleDataParameters,SampleDataModel> GetSampleData
    = AdventureWorksConnection.GetSampleData.CreateMethod<GetSampleDataParameters,SampleDataModel>(
    p => {
        p.Add("Guid", p.Factory.Create(m => m.Guid));
        p.Add("String", p.Factory.Create(m => m.String));
        p.Add("Details", p.Factory.Create(m => m.Details));
    },
    v => {
        v.Add(v.Rules.Required(m => m.String), "String"); // will set the parameter name to "String"
        v.Add(v.Rules.Required(m => m.Details)); // will set the parameter name to "Unspecified Pramameter" default value
    });
```
For doumentation on all available rules see [Rules](~/api/DbFacade.Factories.ValidationRuleFactory-1.html)