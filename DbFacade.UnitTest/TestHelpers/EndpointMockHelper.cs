using DbFacade.DataLayer.Models;
using DbFacade.UnitTest.DataLayer.EndpointLayer;

namespace DbFacade.UnitTest.TestHelpers
{
    internal class EndpointMockHelper
    {
        private readonly Endpoints Endpoints;
        public EndpointMockHelper(Endpoints endpoints)
        {
            Endpoints = endpoints;
        }
        public void DisableMockMode()
        {
            Endpoints.TestFetchData.DisableMockMode();
            Endpoints.TestFetchDataNoSchema.DisableMockMode();
            Endpoints.TestFetchDataAlt.DisableMockMode();
            Endpoints.TestFetchDataAltWithParams.DisableMockMode();
            Endpoints.TestFetchDataWithOutput.DisableMockMode();
            Endpoints.TestFetchDataWithBadDbColumn.DisableMockMode();
            Endpoints.TestFetchDataWithNested.DisableMockMode();
            Endpoints.TestTransaction.DisableMockMode();
            Endpoints.TestNoData.DisableMockMode();
            Endpoints.TestMultipleDataSets.DisableMockMode();
            Endpoints.TestException.DisableMockMode();
            Endpoints.TestParameters.DisableMockMode();
            Endpoints.TestValidation.DisableMockMode();
            Endpoints.TestValidationFail.DisableMockMode();
            Endpoints.TestParametersMismatch2.DisableMockMode();
            Endpoints.TestTableDirect.DisableMockMode();
            Endpoints.TestSqlCredendials.DisableMockMode();
            Endpoints.TestXml.DisableMockMode();
            Endpoints.TestScalar.DisableMockMode();
            Endpoints.TestMockException_ExecuteQuery.DisableMockMode();
            Endpoints.TestMockException_ExecuteNonQuery.DisableMockMode();
            Endpoints.TestMockException_ExecuteScalar.DisableMockMode();
            Endpoints.TestMockException_ExecuteXml.DisableMockMode();
            Endpoints.TestMockException_Transaction.DisableMockMode();
            Endpoints.TestQuery.DisableMockMode();
            Endpoints.TestNonQuery.DisableMockMode();
            Endpoints.TestFetchDataWithOnBeforeAsync.DisableMockMode();
            Endpoints.TestNullableParams.DisableMockMode();

        }
        public void TestNullableParams_EnableMockMode()
        {
            Endpoints.TestNullableParams.EnableMockMode(b => {
                b.ReturnValue = 1;
            });
        }
        public void TestFetchDataWithOnBeforeAsync_EnableMockMode()
        {
            Endpoints.TestFetchDataWithOnBeforeAsync.EnableMockMode(b => {
                b.Add(new ResponseData { MyString = "test string", MyEnum = 1, Integer = 5 });
                b.ReturnValue = 11;
            });
        }
        public void TestFetchData_EnableMockMode()
        {
            Endpoints.TestFetchData.EnableMockMode(b => {
                b.Add(new ResponseData { MyString = "test string", MyEnum = 1, Integer = 5 });
                b.ReturnValue = 11;
            });
        }
        public void TestFetchDataNoSchema_EnableMockMode()
        {
            Endpoints.TestFetchDataNoSchema.EnableMockMode(b => {
                b.Add(new ResponseData { MyString = "test string", MyEnum = 1, Integer = 5 });
                b.ReturnValue = 11;
            });
        }
        public void TestFetchDataAlt_EnableMockMode()
        {
            Endpoints.TestFetchDataAlt.EnableMockMode(b => {
                b.Add(new ResponseDataAlt { MyStringAlt = "test string", MyEnumAlt = 1 });
                b.ReturnValue = 0;
            });
        }
        public void TestFetchDataAltWithParams_EnableMockMode()
        {
            Endpoints.TestFetchDataAltWithParams.EnableMockMode(b => {
                b.Add(new ResponseDataAlt { MyStringAlt = "test string", MyEnumAlt = 1 });
                b.ReturnValue = 0;
            });
        }
        public void TestFetchDataWithOutput_EnableMockMode()
        {
            Endpoints.TestFetchDataWithOutput.EnableMockMode(b => {
                b.Add(new ResponseData { MyString = "test string", MyEnum = 1 });
                b.OutputValues["MyStringOutputParam"] = "output response";
                b.ReturnValue = 0;
            });
        }
        public void TestFetchDataWithBadDbColumn_EnableMockMode()
        {
            Endpoints.TestFetchDataWithBadDbColumn.EnableMockMode(b => {
                b.Add(new ResponseData { MyString = "test string", MyEnum = 1 });
                b.ReturnValue = 0;
            });
        }
        public void TestFetchDataWithNested_EnableMockMode()
        {
            Endpoints.TestFetchDataWithNested.EnableMockMode(b => {
                b.Add(new ResponseData { MyString = "test string", MyEnum = 1, Flag = "TRUE", FlagInt = 1, FlagFalse = "False", FlagIntFalse = 0 });
                b.ReturnValue = 0;
            });
        }
        public void TestTransaction_EnableMockMode()
        {
            Endpoints.TestTransaction.EnableMockMode(b => {
                b.ReturnValue = 10;
            });
        }
        public void TestNoData_EnableMockMode()
        {
            Endpoints.TestNoData.EnableMockMode(b => {
                b.ReturnValue = 0;
            });
        }
        public void TestMultipleDataSets_EnableMockMode()
        {
            Endpoints.TestMultipleDataSets.EnableMockMode(b => {
                b.Add(new ResponseDataMulti1 { Name = "Test User", Id = 1 }, new ResponseDataMulti1 { Name = "Other User", Id = 2 });
                b.Add(new ResponseDataMulti2 { Name = "Role One", Value = "R1" }, new ResponseDataMulti2 { Name = "Role Two", Value = "R2" });
                b.ReturnValue = 0;
            });
        }
        public void TestException_EnableMockMode()
        {
            Endpoints.TestException.EnableMockMode(b => {
                b.ReturnValue = 0;
            });
        }
        public void TestParameters_EnableMockMode()
        {
            Endpoints.TestParameters.EnableMockMode(b => {
                b.OutputValues["AnsiString_OUT"] = "AnsiString";
                b.OutputValues["AnsiStringFixedLength_OUT"] = "AnsiStringFixedLength";
                b.OutputValues["StringFixedLength_OUT"] = "StringFixedLength";
                b.OutputValues["Currency_OUT"] = 123.34m;
                b.OutputValues["Null_OUT"] = null;

                b.OutputValues["AnsiString_IN_OUT"] = "AnsiString";
                b.OutputValues["AnsiStringFixedLength_IN_OUT"] = "AnsiStringFixedLength";
                b.OutputValues["StringFixedLength_IN_OUT"] = "StringFixedLength";
                b.OutputValues["Currency_IN_OUT"] = 123.34m;
                b.ReturnValue = 0;
            });
        }
        public void TestValidation_EnableMockMode()
        {
            Endpoints.TestValidation.EnableMockMode(b => {
                b.ReturnValue = 0;
            });
        }
        public void TestValidationFail_EnableMockMode()
        {
            Endpoints.TestValidationFail.EnableMockMode(b => {
                b.ReturnValue = 0;
            });
        }
        public void TestParametersMismatch_EnableMockMode()
        {
            Endpoints.TestParametersMismatch2.EnableMockMode(b => {
                b.ReturnValue = 0;
            });
        }
        public void TestTableDirect_EnableMockMode()
        {
            Endpoints.TestTableDirect.EnableMockMode(b => {
                b.Add(
                    new ResponseData { MyString = "test string", MyEnum = 1, Integer = 5 }, 
                    new ResponseData { MyString = "test string 2", MyEnum = 0, Integer = 10 },
                    new ResponseData { MyString = "test string 3", MyEnum = 1, Integer = 15 }
                    );
            });
        }
        public void TestSqlCredendials_EnableMockMode()
        {
            Endpoints.TestSqlCredendials.EnableMockMode(b => {
                b.ReturnValue = 10;
            });
        }
        public void TestXml_EnableMockMode()
        {
            Endpoints.TestXml.EnableMockMode(b => {
                b.Add(
                    new ResponseData { MyString = "test string", MyEnum = 1, Integer = 5 },
                    new ResponseData { MyString = "test string 2", MyEnum = 0, Integer = 10 },
                    new ResponseData { MyString = "test string 3", MyEnum = 1, Integer = 15 }
                    );
            });
        }
        public void TestScalar_EnableMockMode()
        {
            Endpoints.TestScalar.EnableMockMode(b => {
                b.ScalarReturnValue = "MyTestScalarReturnValue";
            });
        }


        public void TestMockException_ExecuteQuery_EnableMockMode(Action<MockResponseSettings> builder)
        {
            Endpoints.TestMockException_ExecuteQuery.EnableMockMode(b => {
                builder(b.Settings);
            });
        }
        public void TestMockException_ExecuteNonQuery_EnableMockMode(Action<MockResponseSettings> builder)
        {
            Endpoints.TestMockException_ExecuteNonQuery.EnableMockMode(b => {
                builder(b.Settings);
            });
        }
        public void TestMockException_ExecuteScalar_EnableMockMode(Action<MockResponseSettings> builder)
        {
            Endpoints.TestMockException_ExecuteScalar.EnableMockMode(b => {
                builder(b.Settings);
            });
        }
        public void TestMockException_ExecuteXml_EnableMockMode(Action<MockResponseSettings> builder)
        {
            Endpoints.TestMockException_ExecuteXml.EnableMockMode(b => {
                builder(b.Settings);
            });
        }
        public void TestMockException_Transaction_EnableMockMode(Action<MockResponseSettings> builder)
        {
            Endpoints.TestMockException_Transaction.EnableMockMode(b => {
                builder(b.Settings);
            });
        }
        public void TestQuery_EnableMockMode()
        {
            Endpoints.TestQuery.EnableMockMode(b => {
                b.Add(new ResponseData { MyString = "test string", MyEnum = 1, Integer = 5 });
                b.ReturnValue = 11;
            });
        }
        public void TestNonQuery_EnableMockMode()
        {
            Endpoints.TestNonQuery.EnableMockMode(b => {
                b.ReturnValue = 1;
            });
        }
    }
}
