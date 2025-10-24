# Define Data Models
For the most part data models should impliment the 'IDbDataModel' interface.
This should be used where there is a 1 to 1 pairing of data model type to endpoint.

[!code-csharp[](~/CodeExamples/CreateDataLayer.cs#CreateDataLayer_DefineDataModels)]

There are cases where a common class are prefrered, such as a data for a lookup list

[!code-csharp[](~/CodeExamples/CreateDataLayer.cs#CreateDataLayer_DefineDataModels_LookupItem)]
See 