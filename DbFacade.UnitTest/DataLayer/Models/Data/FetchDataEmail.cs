using DbFacade.DataLayer.Models;
using DbFacade.Extensions;
using System.Net.Mail;

namespace DbFacade.UnitTest.DataLayer.Models.Data
{
    internal class FetchDataEmail : IDbDataModel
    {
        public MailAddress Email { get; internal set; }
        public IEnumerable<MailAddress> EmailList { get; internal set; }
        public void Init(IDataCollection collection)
        {
            Email = new MailAddress(collection.GetValue<string>("Email"));
            EmailList = collection.ToEnumerable<string>("EmailList").Select(m => new MailAddress(m));
        }
    }
}
