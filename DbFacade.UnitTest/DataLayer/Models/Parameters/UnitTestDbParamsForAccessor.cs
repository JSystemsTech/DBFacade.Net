using DbFacade.UnitTest.DataLayer.Models.Attributes;

namespace DbFacade.UnitTest.DataLayer.Models.Parameters
{
    public class UnitTestDbParamsForAccessor
    {
        public string FirstName = "First";
        public string MiddleName = "Middle";
        public string LastName = "Last";
        [TestColumn]
        public int Age = 32;
        [TestColumn]
        public string FullName => $"{LastName},{FirstName},{MiddleName}";
        [TestColumn]
        public DateTime Updated = DateTime.Now.Date;

        public DateTime UpdatedInternal = DateTime.Now.Date;

        [TestBad("one")]
        [TestBad("two")]
        public string ColWithMutiAttr { get; set; }
    }
}
