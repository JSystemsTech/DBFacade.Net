# Binding Parameters

## Adding Input Parameters
Bind input parameters as follows
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_ParameterBinding_Input)]

## Adding Output Parameters
Bind output values as follows.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_ParameterBinding_Output)]

## Adding InputOutput Parameters
If you are using model parameters that are also used as output values you may define them as follows.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_ParameterBinding_InputOutput)]

## Adding Only Hard Coded params
In the case of only needing hard coded parameters and no parameter type is required you may use the following.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_ParameterBinding_HardCodededOnly)]

## Binding Multiple Expected Parameter Model Types
In the case of you endpoint could expect multiple object types that are handled slightly diferently you can do the following.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_ParameterBinding_MultipleExpectedTypes)]
