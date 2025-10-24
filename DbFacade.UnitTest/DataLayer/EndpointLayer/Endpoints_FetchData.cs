using DbFacade.DataLayer.CommandConfig;
using DbFacade.Extensions;
using DbFacade.UnitTest.DataLayer.Models.Parameters;
using DbFacade.UnitTest.Services;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace DbFacade.UnitTest.DataLayer.EndpointLayer
{
    internal partial class Endpoints
    {
        internal IDbCommandMethod TestFetchData { get; private set; }
        internal IDbCommandMethod TestFetchDataNoSchema { get; private set; }
        internal IDbCommandMethod TestMultipleDataSets { get; private set; }
        internal IDbCommandMethod TestFetchDataAlt { get; private set; }
        internal IDbCommandMethod TestFetchDataAltWithParams { get; private set; }
        internal IDbCommandMethod TestFetchDataWithBadDbColumn { get; private set; }
        internal IDbCommandMethod TestFetchDataWithNested { get; private set; }
        internal IDbCommandMethod TestFetchDataWithOutput { get; private set; }
        internal IDbCommandMethod TestNoData { get; private set; }
        internal IDbCommandMethod TestFetchDataWithModel { get; private set; }
        internal IDbCommandMethod TestValidation { get; private set; }
        internal IDbCommandMethod TestValidationFail { get; private set; }
        internal IDbCommandMethod TestTableDirect { get; private set; }
        internal IDbCommandMethod TestSqlCredendials { get; private set; }
        internal IDbCommandMethod TestXml { get; private set; }
        internal IDbCommandMethod TestXml2 { get; private set; }
        internal IDbCommandMethod TestScalar { get; private set; }
        internal IDbCommandMethod TestScalar2 { get; private set; }
        internal IDbCommandMethod TestQuery { get; private set; }
        internal IDbCommandMethod TestNonQuery { get; private set; }
        internal IDbCommandMethod TestFetchDataWithOnBeforeAsync { get; private set; }

        private void OnInit_FetchData()
        {
            TestFetchDataWithOnBeforeAsync = SQLConnection.Dbo.DefineEndpoint("TestFetchDataWithOnBeforeAsync", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestFetchDataWithOnBeforeAsync");
                o.BindOnBeforeExecute((cmd, model) => {
                    if (model is string throwErrorMessage)
                    {

                    }
                });
                o.BindOnBeforeExecuteAsync((cmd, model, cancellationToken) => {
                    if(model is Func<Task> cb && cb != null)
                    {
                        return cb();
                    }
                    return Task.CompletedTask;
                });
            });

            TestFetchData = SQLConnection.Dbo.DefineEndpoint("TestFetchData", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestFetchData");
            });
            TestFetchDataNoSchema = SQLConnection.DefineEndpoint("TestFetchDataNoSchema", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("dbo.TestFetchData");
            });
            TestMultipleDataSets = SQLConnection.Dbo.DefineEndpoint("TestMultipleDataSets", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestTransaction");
            });
            TestFetchDataAlt = SQLConnection.Dbo.DefineEndpoint("TestFetchDataAlt", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestFetchData");
            });
            TestFetchDataAltWithParams = SQLConnection.Dbo.DefineEndpoint("TestFetchDataAltWithParams", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestFetchDataAltWithParams")
                .WithParameters<UnitTestDbParams>((m, p) =>
                {
                    p.AddInput("Today", m.Today);
                });
            });

            TestFetchDataWithBadDbColumn = SQLConnection.Dbo.DefineEndpoint("TestFetchDataWithBadDbColumn", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestFetchData");
            });
            TestFetchDataWithNested = SQLConnection.Dbo.DefineEndpoint("TestFetchDataWithNested", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestFetchData");
            });
            TestFetchDataWithOutput = SQLConnection.Dbo.DefineEndpoint("TestFetchDataWithOutput", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestFetchData")
                .WithParameters(p => {
                    p.AddOutput<string>("MyStringOutputParam", 8000);
                });
            });
            TestNoData = SQLConnection.Dbo.DefineEndpoint("TestNoData", o =>
            {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestTransaction")
                .WithParameters<Guid>((m, p) =>
                {
                    p.AddInput("Guid", m);
                    p.AddInput("Nullable", (int?)null);
                });
            });
            TestFetchDataWithModel = SQLConnection.Dbo.DefineEndpoint("TestFetchDataWithModel", o =>
            {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsNonQuery("TestTransaction")
                .AsTransaction();
            });

            TestValidation = SQLConnection.Dbo.DefineEndpoint("TestValidation", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestValidation")
                .WithParameters<UnitTestParamsForValidation>((m, p) =>
                {
                    p.AddInput("Guid", Guid.NewGuid());
                })
                .WithValidation<UnitTestParamsForValidation>(v =>
                {
                    v.AddHasMaxLength(m => m.StringLen10, 10, "string too long");
                    v.AddHasMinLength(m => m.StringLen10, 10, "string too short");
                    v.AddIsDateTime(m => m.DateTimeNullable, "incorrect Date");
                    v.AddIsDateTime(m => m.DateStr, "incorrect Date");
                    v.AddIsDateTime(m => m.DateStr, "dd/MM/yyyy", "incorrect Date");
                    v.AddIsDateTime(m => m.DateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, "incorrect Date");
                    v.AddIsDateTime(m => m.DateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, "incorrect Date");
                    v.AddIsDateTime(m => m.DateStr, "dd/MM/yyyy", DateTimeStyles.None, "incorrect Date");
                    v.AddIsEqualTo(m => m.Int10, 10, "Not Equal To");
                    v.AddIsNotEqual(m => m.Int10, 11, "Is Equal To");
                    v.AddIsGreaterThanOrEqualTo(m => m.Int10, 9, "Not Greater Than Or Equal To");
                    v.AddIsGreatorThan(m => m.Int10, 9, "Not Greater Than");
                    v.AddIsLessThanOrEqualTo(m => m.Int10, 11, "Not Less Than Or Equal To");
                    v.AddIsLessThan(m => m.Int10, 11, "Not Less Than");

                    v.AddIsEqualTo(m => m.Int10, m => m.Int10, "Not Equal To");
                    v.AddIsNotEqual(m => m.Int10, m => m.Int11, "Is Equal To");
                    v.AddIsGreaterThanOrEqualTo(m => m.Int10, m => m.Int9, "Not Greater Than Or Equal To");
                    v.AddIsGreatorThan(m => m.Int10, m => m.Int9, "Not Greater Than");
                    v.AddIsLessThanOrEqualTo(m => m.Int10, m => m.Int11, "Not Less Than Or Equal To");
                    v.AddIsLessThan(m => m.Int10, m => m.Int11, "Not Less Than");

                    v.AddIsNumeric(m => m.Int10, "Is Not Numeric");
                    v.AddIsNumeric(m => m.StringNumeric, "Is Not Numeric");
                    v.AddIsNDigitString(m => m.StringNumeric,10, "Is Not 10 Digit String");
                    v.AddIsMailAddress(m => m.Email, "Is Not Mail Address");
                    v.AddIsInDomain(m => m.Email,new string[] { "testdomain.com" }, "Is Not In Domain");
                    v.AddIsInDomain(m => m.MailAddress, new string[] { "testdomain.com" }, "Is Not In Domain");
                    v.AddIsNotInDomain(m => m.Email, new string[] { "baddomain.com" }, "Is In Domain");
                    v.AddIsNotInDomain(m => m.MailAddress, new string[] { "baddomain.com" }, "Is In Domain");
                    v.AddIsNull(m => m.StringNull, "Is Not Null");
                    v.AddIsNullOrEmpty(m => m.StringEmpty, "Is Not Null Or Empty");
                    v.AddIsNullOrWhiteSpace(m => m.StringWhiteSpace, "Is Not Null Or White Space");
                    v.AddIsNotNull(m => m.StringLen10, "Is Null");
                    v.AddIsNotNullOrEmpty(m => m.StringLen10, "Is Null Or Empty");
                    v.AddIsNotNullOrWhiteSpace(m => m.StringLen10, "Is Null Or White Space");

                    string regexAlphanumeric = @"^[a-zA-Z0-9\s,]*$";
                    Regex rg = new Regex(regexAlphanumeric);
                    v.AddIsRegexMatch(m => m.StringAlphaNumeric, regexAlphanumeric, "Is Not alphanumeric");
                    v.AddIsRegexMatch(m => m.StringAlphaNumeric, rg, "Is Not alphanumeric");
                    v.AddIsRegexMatch(m => m.StringAlphaNumeric, regexAlphanumeric, RegexOptions.None, "Is Not alphanumeric");
                });
            });
            TestValidationFail = SQLConnection.Dbo.DefineEndpoint("TestValidationFail", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestValidationFail")
                .WithParameters<UnitTestParamsForValidation>((m, p) =>
                {
                    p.AddInput("Guid", Guid.NewGuid());
                })
                .WithValidation<UnitTestParamsForValidation>(v =>
                {
                    v.AddHasMaxLength(m => m.StringLen10, -10, "invalid max length");
                    v.AddHasMinLength(m => m.StringLen10, -10, "invalid min length");
                    
                    v.AddIsNDigitString(m => "", 10, "invalid empty N digit string");
                    v.AddIsNDigitString(m => "123", 10, "invalid N digit string");


                    v.AddIsInDomain(m => "somebademail", new string[] { "testdomain.com" }, "invalid email IsInDomain");
                    v.AddIsNotInDomain(m => "somebademail", new string[] { "baddomain.com" }, "invalid email IsNotInDomain");
                    
                });
            });
            TestTableDirect = SQLConnection.Dbo.DefineEndpoint("TestTableDirect", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsTableDirect("SomeTestTable");                
            });
            TestSqlCredendials = SQLConnection_WithSqlCredendials.Dbo.DefineEndpoint("TestSqlCredendials", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestSqlCredendials");
            });
            TestXml = SQLConnection_WithSqlCredendials.Dbo.DefineEndpoint("TestXml", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsXml("SELECT * FROM [dbo].[SomeTestTable] FOR XML AUTO, XMLDATA");
            });
            TestXml2 = SQLConnection_WithSqlCredendials.Dbo.DefineEndpoint("TestXml2", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsXml(m=> { return "SELECT * FROM [dbo].[SomeTestTable] FOR XML AUTO, XMLDATA"; });
            });
            TestScalar = SQLConnection_WithSqlCredendials.Dbo.DefineEndpoint("TestScalar", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsScalar("SELECT * FROM [dbo].[SomeTestTable]");
            });
            TestScalar2 = SQLConnection_WithSqlCredendials.Dbo.DefineEndpoint("TestScalar2", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsScalar( m => { return "SELECT * FROM [dbo].[SomeTestTable]"; });
            });

            TestQuery = SQLConnection_WithSqlCredendials.Dbo.DefineEndpoint("TestQuery", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsQuery(m => { return "SELECT * FROM [dbo].[SomeTestTable]"; });
            });
            TestNonQuery = SQLConnection_WithSqlCredendials.Dbo.DefineEndpoint("TestQuery", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsNonQuery(m => { return "UPDATE [T] SET Updated = 1 FROM [dbo].[SomeTestTable] [T] WHERE Updated = 0"; });
            });
        }
    }
}

