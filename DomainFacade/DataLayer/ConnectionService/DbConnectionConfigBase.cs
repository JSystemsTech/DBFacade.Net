using System.Data;
using static DomainFacade.DataLayer.ConnectionService.DbConnectionService;
using DomainFacade.DataLayer.ConnectionService.SQL;

namespace DomainFacade.DataLayer.ConnectionService
{
    public class DbConnectionConfigBase
    {
        public virtual IDbConnection GetDbConnection() { return null; }
        public virtual bool CheckStoredProcAvailability() { return true; }
        public virtual void SetAvailableStoredProcs(DbConnectionStoredProcedure[] storedProcs) { }
        public virtual string GetAvailableStoredProcCommandText() { return null; }
        public virtual CommandType GetAvailableStoredProcCommandType() { return new CommandType(); }
        internal string GetDefaultAvailableStoredProcCommandText()
        {
            return QueryLoader.GetSprocMetaQuery();
        }
    }
}
