using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models.Parameters;
using DbFacade.Extensions;
using DbFacade.UnitTest.DataLayer.Models.Parameters;
using DbFacade.UnitTest.Services;
using System.Data.SqlTypes;
using System.Xml;

namespace DbFacade.UnitTest.DataLayer.EndpointLayer
{
    internal partial class Endpoints
    {
        internal IDbCommandMethod TestTransaction { get; private set; }
        internal IDbCommandMethod TestParametersMismatch2 { get; private set; }
        internal IDbCommandMethod TestParameters { get; private set; }
        internal IDbCommandMethod TestReal { get; private set; }
        internal IDbCommandMethod TestBadConnStr { get; private set; }

        private void OnInit_Transaction()
        {
            TestTransaction = SQLConnection.DefineEndpoint("TestTransaction", o =>
            {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsNonQuery("TestTransaction")
                .WithParameters<UnitTestDbParams>(p =>
                {
                    p.AddInput("MyStringParam", model => model.CustomString);
                    p.AddInput("MyStaticParam", "test");
                })
                .WithValidation<UnitTestDbParams>(v =>
                {
                    v.AddIsNotNullOrWhiteSpace(m => m.CustomString, "CustomString is required.");
                })
                .BindOnBeforeExecute((command, model) => { })
                .AsTransaction();
            });

            TestParametersMismatch2 = SQLConnection.DefineEndpoint("TestParameters", o =>
            {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsNonQuery("TestParameters")
                .WithParameters<UnitTestDbParams>(p =>
                {
                    p.AddInput("MyStringParam", model => model.CustomString);
                    p.AddInput("MyStaticParam", "test");
                })
                .BindOnBeforeExecute((command, model) => { })
                .AsTransaction();
            });

            TestParameters = SQLConnection.Dbo.DefineEndpoint("TestParameters", o =>
            {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestParameters")
                .WithParameters(p =>
                {
                    p.AddInput("AnsiString_IN", new AnsiString("AnsiString"));
                    p.AddInput("AnsiStringFixedLength_IN", new AnsiStringFixedLength("AnsiStringFixedLength"));
                    p.AddInput("StringFixedLength_IN", new StringFixedLength("StringFixedLength"));
                    p.AddInput("Currency_IN", new Currency(123.34m));

                    p.AddOutput<AnsiString>("AnsiString_OUT");
                    p.AddOutput<AnsiStringFixedLength>("AnsiStringFixedLength_OUT", 25);
                    p.AddOutput<StringFixedLength>("StringFixedLength_OUT", 20);
                    p.AddOutput<Currency>("Currency_OUT");
                    p.AddOutput<string>("Null_OUT");

                    p.AddInputOutput("AnsiString_IN_OUT", new AnsiString("AnsiString"), 10);
                    p.AddInputOutput("AnsiStringFixedLength_IN_OUT", new AnsiStringFixedLength("AnsiStringFixedLength"), 25);
                    p.AddInputOutput("StringFixedLength_IN_OUT", new StringFixedLength("StringFixedLength"), 20);
                    p.AddInputOutput("Currency_IN_OUT", new Currency(123.34m));


                    string testXml = "<TestDataXml><DataName>testing</DataName></TestDataXml>";
                    XmlTextReader reader = new XmlTextReader(new StringReader(testXml));
                    
                    DateTime date1 = new DateTime(2010, 1, 1, 8, 0, 15);
                    DateTime date2 = new DateTime(2010, 8, 18, 13, 30, 30);
                    TimeSpan interval = date2 - date1;
                    p.AddInput("TimeSpan", interval);
                    p.AddInput("DateTimeOffset", DateTimeOffset.Now);
                    p.AddInput("CharArr", new char[] { 't', 'e', 's', 't' });
                    p.AddInput("ByteArr", new byte[] { });
                    p.AddInput("Xml", reader);

                    var sqlXml = new SqlXml(reader);
                    p.AddInput("SqlXml", sqlXml);
                    p.AddInput("SqlBytes", new SqlBytes(new byte[] { }));
                    p.AddInput("SqlChars", new SqlChars(new char[] { }));
                    p.AddInput("SqlDateTime", new SqlDateTime(DateTime.Now));
                    p.AddInput("SqlDecimal", new SqlDecimal(10));
                    p.AddInput("SqlDouble", new SqlDouble(20));
                    p.AddInput("SqlGuid", new SqlGuid(Guid.NewGuid()));
                    p.AddInput("SqlInt16", new SqlInt16(16));
                    p.AddInput("SqlInt32", new SqlInt32(32));
                    p.AddInput("SqlInt64", new SqlInt64(64));
                    p.AddInput("SqlMoney", new SqlMoney(12.95m));
                    p.AddInput("SqlSingle", new SqlSingle(12.95f));
                    p.AddInput("SqlString", new SqlString("test"));                    
                });
            });

            TestReal = SQLConnection_WithSqlCredendials.Dbo.DefineEndpoint("TestReal", o =>
            {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestReal");
            });

            TestBadConnStr = SQL_BadConnStr.Dbo.DefineEndpoint("TestBadConnStr", o =>
            {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestBadConnStr");
            });
        }
    }
}
