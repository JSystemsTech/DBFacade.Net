using System.Data;
using static DomainFacade.DataLayer.ConnectionService.DbConnectionService;
using DomainFacade.DataLayer.ConnectionService.SQL;

namespace DomainFacade.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    public class DbConnectionConfigBase
    {
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <returns></returns>
        public virtual IDbConnection GetDbConnection() { return null; }
        /// <summary>
        /// Checks the stored proc availability.
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckStoredProcAvailability() { return true; }
        /// <summary>
        /// Sets the available stored procs.
        /// </summary>
        /// <param name="storedProcs">The stored procs.</param>
        public virtual void SetAvailableStoredProcs(DbConnectionStoredProcedure[] storedProcs) { }
        /// <summary>
        /// Gets the available stored proc command text.
        /// </summary>
        /// <returns></returns>
        public virtual string GetAvailableStoredProcCommandText() { return null; }
        /// <summary>
        /// Gets the type of the available stored proc command.
        /// </summary>
        /// <returns></returns>
        public virtual CommandType GetAvailableStoredProcCommandType() { return new CommandType(); }
        /// <summary>
        /// Gets the default available stored proc command text.
        /// </summary>
        /// <returns></returns>
        internal string GetDefaultAvailableStoredProcCommandText()
        {
            return QueryLoader.GetSprocMetaQuery();
        }
    }
}
