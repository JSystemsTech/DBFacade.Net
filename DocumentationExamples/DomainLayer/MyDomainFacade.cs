using DBFacade.DataLayer.Models;
using DBFacade.Exceptions;
using DocumentationExamples.Models.Data;
using System;

namespace DocumentationExamples.DomainLayer
{
    internal partial class MyDomainFacade : MyDomainFacadeBase, IMyDomainFacade
    {
        public MyDomainFacade(string connectionString, string connectionProvider) : base(connectionString, connectionProvider) { }
       
         /*Define Calls */

        public IDbResponse<MyDataModel> GetData()
        {
            try
            {
                return GetDataHandler.Execute();
            }
            catch(SQLExecutionException e)
            {
                /* handle Sql errors here */
                return null;
            }
            catch (Exception e)
            {
                /* handle all other errors here */
                return null;
            }
        }
        public IDbResponse<MyDataModel> GetDataWithOutputParameters()
        {
            try
            {
                return GetDataWithOutputParametersHandler.Execute();
            }
            catch (SQLExecutionException e)
            {
                /* handle Sql errors here */
                return null;
            }
            catch (Exception e)
            {
                /* handle all other errors here */
                return null;
            }
        }
        public IDbResponse<MyDataModel> GetDataWithParameters(string myString, int myInt)
        {
            try
            {
                return GetDataWithParametersHandler.Execute(myString, myInt);
            }
            catch (ValidationException<DbParamsModel<string, int>> e)
            {
                /* handle validation errors here */
                return null;
            }
            catch (SQLExecutionException e)
            {
                /* handle Sql errors here */
                return null;
            }
            catch (Exception e)
            {
                /* handle all other errors here */
                return null;
            }
        }
        public IDbResponse MyTransaction(string myString)
        {
            try
            {
                return MyTransactionHandler.Execute(myString);
            }
            catch (ValidationException<DbParamsModel<string>> e)
            {
                /* handle validation errors here */
                return null;
            }
            catch (SQLExecutionException e)
            {
                /* handle Sql errors here */
                return null;
            }
            catch (Exception e)
            {
                /* handle all other errors here */
                return null;
            }
        }
    }
}
