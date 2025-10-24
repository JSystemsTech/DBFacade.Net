# Define Endpoints

Create a `DbConnectionConfig` instance from Connection Provider
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#GetDbConnectionConfig)]


## Define Endpoint 
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_Base)]

### Configure as Stored Procedure 
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AsStoredProcedure)]

### Configure as query  
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AsQuery)]

### Configure as non-query  
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AsNonQuery)]

### Configure as XML Query
> [!NOTE]
> Only applies to SQL connections.

[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AsXml)]

### Configure as table direct  

> [!NOTE]
> As stated by microsoft this is only supported by the .NET Framework Data Provider for OLE DB.

[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AsTableDirect)]




## Configure as transaction
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AsTransaction)]



## Binding Parameters
See for [Binding Parameters](ParameterBinding.md) for more details.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_ParameterBinding_Base)]

### Binding Single Parameter Value
In the case of an endpoint only having a single input parameter with a struct type like int, string, Guid etc you may use the following
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_ParameterBinding_SingleParameterValue)]

## Validating Parameters 
Optional if model validation handled separately
See for [Validating Parameters](ParameterValidation.md) for more details.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_Validation)]

## Best Practice  
For most of my projects I work with I use the following approach:
 1.  Create a internal class to hold the endpoint definitions.
 2.  Make the 'IDbCommandMethod' class property values private set. 
 3.  Initialize the properties in the logially broken down Init methods.
 4.  If I have a lot of endpoints I make the class a partial class and separted 'IDbCommandMethod' class properties and Init methods.
 [!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_BestPractice)]