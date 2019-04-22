using DBFacade.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerSandbox1.Facades.SampleFunctionalTestFacade.MockDbDataModels
{
    public class MockTestModel : IMockDbTableRow
    {
        public int id { get; set; }
        public Guid guid { get; set; }
        public DateTime CreateDate { get; set; }
        public int count { get; set; }
        public string comment { get; set; }

        public MockTestModel(int id, Guid guid, DateTime createDate, int count, string comment)
        {
            this.id = id;
            this.guid = guid;
            this.CreateDate = createDate;
            this.count = count;
            this.comment = comment;
        }
    }
}
