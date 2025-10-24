# Validating Parameters

# [Example Base](#tab/ExampleBase)

Snipets below all go into this example.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AdvancedValidation_Base)]

# [Class Reference](#tab/ClassReference)

Parameters class used in validation example
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_MyParametersModelForValidation)]

---


## Date String Validation
If you need to validate date strings you can use the following.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AdvancedValidation_DateString)]

## IComparable Validation
If you need to validate an value that is also an IComparable object (int, double, float, DateTime etc) you can do so like the following.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AdvancedValidation_Comparable)]

## Numeric Object Validation
If you need to validate if an object is numeric or not use the following.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AdvancedValidation_NumericCheck)]

## Email Validation
If you need to do any sort of email address validation use the following.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AdvancedValidation_Email)]

## General String Validation
If you need to do any sort of general string validation use the following.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AdvancedValidation_String)]

## Regex String Validation
If you have a specialized string validation done via Regex use the following.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AdvancedValidation_Regex)]

## Custom Model Validation
If you have a custom model validation method use the following.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AdvancedValidation_Custom)]

## Adding Custom Validation Extensions
If you have a custom model validation method that you want to be made available for common use in your project, you can do the following.

### Create the extension class method
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_CustomValidatorExtensions)]

### Use extension class method
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AdvancedValidation_CustomExtensions)]

## Validation When Binding Multiple Expected Parameter Model Types
If you are binding multiple parameter model types and wish to validate you can do the following.
> [!NOTE]
> When you declare model validation for one expected model type you must also declare it for all expected types. Otherwise you will recieve a validation error.
[!code-csharp[](~/CodeExamples/DefineEndpoints.cs#DefineEndpoint_AdvancedValidation_MultipleExpectedTypes)]