using DBFacade.DataLayer.Models;
using DocumentationExamples.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationExamples.DomainLayer
{
    public interface IMyDomainFacade
    {
        IDbResponse<MyDataModel> GetData();
        IDbResponse<MyDataModel> GetDataWithOutputParameters();
        IDbResponse<MyDataModel> GetDataWithParameters(string myString, int myInt);
        IDbResponse MyTransaction(string myString);
    }
}
