using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.Extensions;
using System;
using System.EnterpriseServices;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Docs2._0.docfx_project.CodeExamples
{
    public class DefineEndpoints
    {
        public void BasicExample()
        {
            #region InitProvider
            SqlDbConnectionProvider connectionProvider = new SqlDbConnectionProvider();
            #endregion

            #region BindConnectionString
            connectionProvider.BindConnectionString("Some Safely Stored Configured Connection String");
            #endregion

            #region GetDbConnectionConfig
            DbConnectionConfig dbConnectionConfig = connectionProvider.DbConnectionConfig;
            #endregion

            #region DefineEndpoint_Base
            IDbCommandMethod myEndpoint = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
            });
            #endregion

            #region DefineEndpoint_AsStoredProcedure
            IDbCommandMethod myEndpoint_AsStoredProcedure = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                o.AsStoredProcedure("[dbo].[MyEndpointStoredProcedureName]");
                //...
            });
            #endregion

            #region DefineEndpoint_AsQuery
            IDbCommandMethod myEndpoint_AsQuery = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                o.AsQuery("SELECT ColumnA, ColumnB, ColumnC FROM MySampleTable");
                //...
            });
            #endregion

            #region DefineEndpoint_AsNonQuery
            IDbCommandMethod myEndpoint_AsNonQuery = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                o.AsNonQuery("UPDATE MySampleTable SET ColumnA = 1, ColumnB = 2, ColumnC = 3 FROM MySampleTable WHERE ColumnD = 5");
                //...
            });
            #endregion

            #region DefineEndpoint_AsTableDirect
            IDbCommandMethod myEndpoint_AsTableDirect = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                o.AsTableDirect("TableName");
                //...
            });
            #endregion

            #region DefineEndpoint_AsXml
            IDbCommandMethod myEndpoint_AsXml = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                o.AsXml("SELECT * FROM [dbo].[SomeTestTable] FOR XML AUTO, XMLDATA");
                //...
            });
            #endregion

            #region DefineEndpoint_AsTransaction
            IDbCommandMethod myEndpoint_AsTransaction = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //... 
                o.AsTransaction();
                //...
            });
            #endregion

             
            #region DefineEndpoint_ParameterBinding_Base
            IDbCommandMethod myEndpointWithParameters = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                o.WithParameters<MyParametersModel>(p =>
                {
                    p.AddInput("ParameterA", model => model.ParameterA);
                    p.AddInput("ParameterB", model => model.ParameterB);
                });
                //...
            });
            #endregion

            #region DefineEndpoint_ParameterBinding_SingleParameterValue
            IDbCommandMethod myEndpointSingleParameterValue = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                o.WithParameters<Guid>(p=>
                {
                    p.AddInput("SomeGuid", m=>m);
                });
                //...
            });
            #endregion

            #region DefineEndpoint_Validation
            IDbCommandMethod myEndpointWithValidation = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                o.WithValidation<MyParametersModel>(v => {
                    v.AddIsNotNullOrWhiteSpace(m => m.ParameterA, "ParameterA is required.");
                });
                //...
            });
            #endregion


            //Advanced Parameter Binding
            #region DefineEndpoint_ParameterBinding_Input
            IDbCommandMethod myEndpointWithParametersInput = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                o.WithParameters<MyParametersModel>(p =>
                {
                    p.AddInput("ParameterA", model => model.ParameterA);
                    p.AddInput("ParameterB", model => model.ParameterB);
                    p.AddInput("ParameterC", "SomeHardedCodedValue");
                });
                //...
            });
            #endregion

            #region DefineEndpoint_ParameterBinding_Output
            IDbCommandMethod myEndpointWithParametersOutput = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                o.WithParameters<MyParametersModel>(p =>
                {
                    p.AddOutput<string>("OutputString");
                    p.AddOutput<string>("OutputStringWithSize", 100);
                    p.AddOutput<int>("OutputInteger");
                });
                //...
            });
            #endregion

            #region DefineEndpoint_ParameterBinding_InputOutput
            IDbCommandMethod myEndpointWithParametersInputOutput = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                o.WithParameters<MyParametersModel>(p =>
                {
                    p.AddInputOutput("ParameterD", model => model.ParameterD);
                    p.AddInputOutput("ParameterEWithSize", model => model.ParameterE, 100);
                });
                //...
            });
            #endregion

            #region DefineEndpoint_ParameterBinding_HardCodededOnly
            IDbCommandMethod myEndpointWithParametersHardCodededOnly = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                o.WithParameters(p =>
                {
                    p.AddInput("ParameterA", "ParameterAValue");
                    p.AddInput("ParameterB", 23);
                    p.AddInput("ParameterC", "SomeHardedCodedValue");
                });
                //...
            });
            #endregion

            #region DefineEndpoint_ParameterBinding_MultipleExpectedTypes
            IDbCommandMethod myEndpointWithParametersMultipleExpectedTypes = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                //Set first accepted Parameter Type
                o.WithParameters<MyParametersModel>(p =>
                {
                    p.AddInput("ParameterA", model => model.ParameterA);
                    p.AddInput("ParameterB", model => model.ParameterB);
                })
                //Set second accepted Parameter Type
                .WithParameters<MyParametersModelOther>(p =>
                {
                    p.AddInput("ParameterA", model => model.ParameterOne);
                    p.AddInput("ParameterB", model => model.ParameterTwo);
                })
                // Set Common parameters
                .WithParameters(p =>
                {
                    p.AddInput("ParameterC", "SomeHardedCodedValue");
                });
                //...
            });
            #endregion

            //Advanced Parameter Validation
            #region DefineEndpoint_AdvancedValidation_Base
            IDbCommandMethod myEndpointValidation = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                o.WithValidation<MyParametersModelForValidation>(v =>
                {
                    //Implement Model Validation here
                });
                //...
            });
            #endregion
            IDbCommandMethod myEndpointValidationDetails = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                o.WithValidation<MyParametersModelForValidation>(v =>
                {
                    //...
                    #region DefineEndpoint_AdvancedValidation_DateString
                    //Check if model value is non null DateTime object
                    v.AddIsDateTime(m => m.DateTimeNullable, "incorrect Date");
                    //Check if model value string can be parsed to DateTime object
                    v.AddIsDateTime(m => m.DateString, "DateString is incorrect Date");
                    //Check if model value string can be parsed to DateTime object with specific format
                    v.AddIsDateTime(m => m.DateString, "dd/MM/yyyy", "DateString is incorrect Date");
                    //Check if model value string can be parsed to DateTime object with specific format and format provider
                    v.AddIsDateTime(m => m.DateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, "DateString is incorrect Date");
                    //Check if model value string can be parsed to DateTime object with specific format, format provider, and style
                    v.AddIsDateTime(m => m.DateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, "DateString is incorrect Date");
                    //Check if model value string can be parsed to DateTime object with specific format, and style
                    v.AddIsDateTime(m => m.DateString, "dd/MM/yyyy", DateTimeStyles.None, "DateString is incorrect Date");
                    #endregion
                    #region DefineEndpoint_AdvancedValidation_Comparable
                    //Check if model value is equal to a value
                    v.AddIsEqualTo(m => m.IntegerValue, 10, "IntegerValue Not Equal To 10");
                    //Check if model value is equal to another model value
                    v.AddIsEqualTo(m => m.IntegerValue, m => m.IntegerValueHigh, "IntegerValue Not Equal To IntegerValueHigh");
                    //Check if model value is not equal to a value
                    v.AddIsNotEqual(m => m.IntegerValue, 11, "IntegerValue Is Equal To 11");
                    //Check if model value is not equal to another model value
                    v.AddIsNotEqual(m => m.IntegerValue, m => m.IntegerValueHigh, "IntegerValue Is Equal To IntegerValueHigh");
                    //Check if model value is greator than or equal to a value
                    v.AddIsGreaterThanOrEqualTo(m => m.IntegerValue, 9, "IntegerValue Not Greater Than Or Equal To 9");
                    //Check if model value is greator than or equal to another model value
                    v.AddIsGreaterThanOrEqualTo(m => m.IntegerValue, m => m.IntegerValueLow, "IntegerValue Not Greater Than Or Equal To IntegerValueLow");
                    //Check if model value is greator than a value
                    v.AddIsGreatorThan(m => m.IntegerValue, 9, "IntegerValue Not Greater Than 9");
                    //Check if model value is greator than another model value
                    v.AddIsGreatorThan(m => m.IntegerValue, m => m.IntegerValueLow, "IntegerValue Not Greater Than IntegerValueLow");
                    //Check if model value is less than or equal to a value
                    v.AddIsLessThanOrEqualTo(m => m.IntegerValue, 11, "IntegerValue Not Less Than Or Equal To 11");
                    //Check if model value is less than or equal to another model value
                    v.AddIsLessThanOrEqualTo(m => m.IntegerValue, m => m.IntegerValueHigh, "IntegerValue Not Less Than Or Equal To IntegerValueHigh");
                    //Check if model value is less than a value
                    v.AddIsLessThan(m => m.IntegerValue, 11, "IntegerValue Not Less Than 11");
                    //Check if model value is less than another model value
                    v.AddIsLessThan(m => m.IntegerValue, m => m.IntegerValueHigh, "IntegerValue Not Less Than IntegerValueHigh");
                    #endregion
                    #region DefineEndpoint_AdvancedValidation_NumericCheck
                    //Check if model value is numeric type
                    v.AddIsNumeric(m => m.IntegerValue, "IntegerValue Is Not Numeric");
                    //Check if model value string can be parsed to numeric type
                    v.AddIsNumeric(m => m.StringNumeric, "StringNumeric Is Not Numeric");
                    #endregion
                    #region DefineEndpoint_AdvancedValidation_Email
                    //Check if model string value can be parsed to MailAddress object
                    v.AddIsMailAddress(m => m.Email, "Email Is Not Mail Address");
                    //Check if model string value can be parsed to MailAddress object and is in allowed domain list.
                    v.AddIsInDomain(m => m.Email, new string[] { "testdomain.com" }, "Email is not in allowed domains");
                    //Check if model MailAddress object is in allowed domain list.
                    v.AddIsInDomain(m => m.MailAddress, new string[] { "testdomain.com" }, "MailAddress is not in allowed domains");
                    //Check if model string value can be parsed to MailAddress object and is not in unallowed domain list.
                    v.AddIsNotInDomain(m => m.Email, new string[] { "baddomain.com" }, "Email is in unallowed domains");
                    //Check if model MailAddress object is not in unallowed domain list.
                    v.AddIsNotInDomain(m => m.MailAddress, new string[] { "baddomain.com" }, "MailAddress is in unallowed domains");
                    #endregion
                    #region DefineEndpoint_AdvancedValidation_String
                    //Check if model string value length is at or below max lenth allowed
                    v.AddHasMaxLength(m => m.MyString, 10, "MyString too long");
                    //Check if model string value length is at or above min lenth allowed
                    v.AddHasMinLength(m => m.MyString, 10, "MyString too short");
                    //Check if model string value is a digit only string of a given length
                    v.AddIsNDigitString(m => m.StringNumeric, 10, "StringNumeric Is Not 10 Digit String");
                    //Check if model string value is null
                    v.AddIsNull(m => m.StringNull, "StringNull Is Not Null");
                    //Check if model string value is null or empty
                    v.AddIsNullOrEmpty(m => m.StringEmpty, "StringEmpty Is Not Null Or Empty");
                    //Check if model string value is null or whitespace
                    v.AddIsNullOrWhiteSpace(m => m.StringWhiteSpace, "StringWhiteSpace Is Not Null Or White Space");
                    //Check if model string value is not null (useful for checking required)
                    v.AddIsNotNull(m => m.MyString, "MyString Is Null");
                    //Check if model string value is not null or empty (useful for checking required)
                    v.AddIsNotNullOrEmpty(m => m.MyString, "MyString Is Null Or Empty");
                    //Check if model string value is not null or whitespace (useful for checking required)
                    v.AddIsNotNullOrWhiteSpace(m => m.MyString, "MyString Is Null Or White Space");
                    #endregion
                    #region DefineEndpoint_AdvancedValidation_Regex
                    string regexAlphanumeric = @"^[a-zA-Z0-9\s,]*$";
                    Regex rg = new Regex(regexAlphanumeric);

                    //Check if model value matches regex string 
                    v.AddIsRegexMatch(m => m.StringAlphaNumeric, regexAlphanumeric, "Is Not alphanumeric");
                    //Check if model value matches regex object 
                    v.AddIsRegexMatch(m => m.StringAlphaNumeric, rg, "Is Not alphanumeric");
                    //Check if model value matches regex string with options
                    v.AddIsRegexMatch(m => m.StringAlphaNumeric, regexAlphanumeric, RegexOptions.None, "Is Not alphanumeric");
                    #endregion

                    #region DefineEndpoint_AdvancedValidation_Custom
                    v.Add(m => m.CustomCheck(), "Does Not Pass Custom Check");
                    #endregion

                    #region DefineEndpoint_AdvancedValidation_CustomExtensions
                    v.AddIsAlphaNumeric(m => m.StringAlphaNumeric, "StringAlphaNumeric");
                    #endregion
                });
                //...
            });
            #region DefineEndpoint_AdvancedValidation_MultipleExpectedTypes
            IDbCommandMethod myEndpointWithValidationMultipleExpectedTypes = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                //Set first accepted Parameter Type
                o.WithParameters<MyParametersModel>(p =>
                {
                    p.AddInput("ParameterA", model => model.ParameterA);
                    p.AddInput("ParameterB", model => model.ParameterB);
                })
                //Set second accepted Parameter Type
                .WithParameters<MyParametersModelOther>(p =>
                {
                    p.AddInput("ParameterA", model => model.ParameterOne);
                    p.AddInput("ParameterB", model => model.ParameterTwo);
                })
                // Set Common parameters
                .WithParameters(p =>
                {
                    p.AddInput("ParameterC", "SomeHardedCodedValue");
                })
                .WithValidation<MyParametersModel>(v =>
                {
                    //Implement first accepted Model type Validation here
                })
                .WithValidation<MyParametersModelOther>(v =>
                {
                    //Implement second accepted Model type Validation here
                });
                //...
            });
            #endregion

            #region DefineEndpoint_WithErrorHandling
            IDbCommandMethod myEndpointWithErrorHandling = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                o.BindErrorHandler(errorInfo => {
                    if(errorInfo.ExceptionType != DbExecutionExceptionType.ValidationError)
                    {
                        //Do something with non validation error
                    }
                });
                //...
            });
            #endregion
            #region DefineEndpoint_WithErrorHandlingData
            IDbCommandMethod myEndpointWithErrorHandlingData = dbConnectionConfig.DefineEndpoint("MyEndpoint", o => {
                //...
                o.BindErrorHandler(errorInfo => {
                    //log only errors that are not validation errors
                    bool logError = errorInfo.ExceptionType != DbExecutionExceptionType.ValidationError;
                    errorInfo.ErrorData["logError"] = logError;
                });
                //...
            });
            #endregion
        }

    }
    #region DefineEndpoint_CustomValidatorExtensions
    internal static class CustomValidatorExtensions
    {
        private static Regex AlphaNumericRegex = new Regex(@"^[a-zA-Z0-9\s,]*$");
        public static void AddIsAlphaNumeric<T>(this Validator<T> validator, Func<T, string> getValue, string parameterName)
            where T : class
        => validator.Add(m => AlphaNumericRegex.Match(getValue(m)).Success, $"{parameterName} is not alphanumeric");
    }
    #endregion
    #region DefineEndpoint_MyParametersModelForValidation
    internal class MyParametersModelForValidation
    {

        internal DateTime? DateTimeNullable = DateTime.Parse("01/01/1979");
        internal string DateString = "01/01/1979";
        internal int IntegerValue = 10;

        internal int IntegerValueLow = 9;
        internal int IntegerValueHigh = 11;
        internal string MyString = "1234567890";
        internal string StringNumeric = "1234567890";
        internal string StringNull = null;
        internal string StringEmpty = "";
        internal string StringWhiteSpace = " ";
        internal string StringAlphaNumeric = "abc123ABC";

        internal bool CustomCheck() { return true; }

        internal string Email = "my.name@testdomain.com";
        internal MailAddress MailAddress = new MailAddress("my.name@testdomain.com");
    }
    #endregion
    #region DefineEndpoint_MyParametersModel
    public class MyParametersModel
    {
        public string ParameterA { get; set; }
        public string ParameterB { get; set; }
        public string ParameterC { get; set; }
        public string ParameterD { get; set; }
        public string ParameterE { get; set; }
    }
    #endregion
    #region DefineEndpoint_MyParametersModelOther
    public class MyParametersModelOther
    {
        public string ParameterOne { get; set; }
        public string ParameterTwo { get; set; }
    }
    #endregion
    #region DefineEndpoint_BestPractice
    internal class Endpoints
    {
        internal IDbCommandMethod MyEndpoint { get; private set; }
        internal IDbCommandMethod LookupListA { get; private set; }
        internal IDbCommandMethod LookupListB { get; private set; }
        private readonly SqlDbConnectionProvider ConnectionProvider;
        private DbConnectionConfig DbConnectionConfig => ConnectionProvider.DbConnectionConfig;
        internal Endpoints()
        {
            ConnectionProvider = new SqlDbConnectionProvider();
            ConnectionProvider.BindConnectionString("Some Safely Stored Configured Connection String");
            InitBaseMethods();
            InitLookupListMethods();
        }
        private void InitBaseMethods()
        {
            MyEndpoint = DbConnectionConfig.Dbo.DefineEndpoint("MyEndpoint", o => {
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
        }
        private void InitLookupListMethods()
        {
            LookupListA = DbConnectionConfig.Dbo.DefineEndpoint("LookupListA", o => {
                o.AsStoredProcedure("LookupListA__StoredProcedureName");
            });
            LookupListB = DbConnectionConfig.Dbo.DefineEndpoint("LookupListB", o => {
                o.AsStoredProcedure("LookupListB__StoredProcedureName");
            });
        }
    }
    #endregion
}