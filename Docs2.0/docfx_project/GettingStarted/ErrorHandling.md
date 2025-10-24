# Error Handling
The DbFacade package offers 


## Global Error Handling

Define an error handler method
[!code-csharp[](~/CodeExamples/ConnectionProvider.cs#OnError)]

Then bind the method to the Connection Provider
[!code-csharp[](~/CodeExamples/ConnectionProvider.cs#BindErrorHandler)]

## Endpoint Error Handling

### Basic Setup
Basic Endpoint Specific Error Handling
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_WithErrorHandling)]

### Add Custom Data To Error Info
If there is additional error data you wish to return to either the global error handler or resonse object you may use the following.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_WithErrorHandlingData)]
