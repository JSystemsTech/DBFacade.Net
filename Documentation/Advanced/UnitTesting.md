#Unit Testing Connection

If you plan to write unit tests for your db calls there is a simple way to enable the Mock Mode of a connection.
Enabling Mock Mode will prevent the connection from actually calling the database and will instead parse the mapped data provided for the response value 


```csharp
IDictionary<Guid, MockResponseData> mockDataDictionary = new Dictionary<Guid, MockResponseData>()
{
    { MyDbConnection.SomeDbCall.CommandId, MockResponseData.Create(new SomeModelArray[] { new SomeModel { MyString = "test string", MyEnum = 1 } }, null, 0)},
    { MyDbConnection.SomeDbCallWithOutputParameters.CommandId, MockResponseData.Create(new SomeModelArray[] { new SomeModel { MyString = "test string", MyEnum = 1 } }, outputVariables => { outputVariables.Add("MyStringOutputParam", "output response"); }, 1)}
};
DbConnectionService.EnableMockMode<MyDbConnection>(mockDataDictionary);
```

Whene you are finished with your unit tests be sure to run the following to reset the connection config to run normally 

```csharp
DbConnectionService.DisableMockMode<MyDbConnection>();
```