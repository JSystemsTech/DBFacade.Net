# Using Schemas
If you have stored procedures defined in in its own Schema like 'dbo' then you can define a new Schema object as follows
```csharp
Schema MySchema = DbConnectionConfig.CreateSchema("MySchema");
```

Then define endpoints as usual. You do not have to fully qualify the stored procedure name.
```csharp
IDbCommandMethod MyEndpoint = MySchema.DefineEndpoint("MyEndpoint", o => {
    o.AsStoredProcedure("MyEndpointStoredProcedureName")
    .WithParameters<MyParametersModel>(p =>
    {
        p.AddInput("ParameterA", model => model.ParameterA);
        p.AddInput("ParameterB", model => model.ParameterB);
    })
    .WithValidation<MyParametersModel>(v => {
        v.AddIsNotNullOrWhiteSpace(m => m.ParameterA, "ParameterA is required.");
    });
});
```

## Best Practice
'DbConnectionConfig' instances already provide a Dbo default Schema so when defining stored procedure endpoints that target the dbo schema in your database use the following.
```csharp
IDbCommandMethod MyEndpoint = DbConnectionConfig.Dbo.DefineEndpoint("MyEndpoint", o => {
    o.AsStoredProcedure("MyEndpointStoredProcedureName")
    .WithParameters<MyParametersModel>(p =>
    {
        p.AddInput("ParameterA", model => model.ParameterA);
        p.AddInput("ParameterB", model => model.ParameterB);
    })
    .WithValidation<MyParametersModel>(v => {
        v.AddIsNotNullOrWhiteSpace(m => m.ParameterA, "ParameterA is required.");
    });
});
```