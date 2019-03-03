using System.Data;
using static DomainFacade.DataLayer.ConnectionService.DbConnectionService;

namespace DomainFacade.DataLayer.ConnectionService
{
    public interface IDbConnectionConfig {
        IDbConnection GetDbConnection();
        bool CheckStoredProcAvailability();
        void SetAvailableStoredProcs(DbConnectionStoredProcedure[] storedProcs);
        string GetAvailableStoredProcCommandText();
        CommandType GetAvailableStoredProcCommandType();
        DbConnectionStoredProcedure[] AvailableStoredProcs();
    }
}
