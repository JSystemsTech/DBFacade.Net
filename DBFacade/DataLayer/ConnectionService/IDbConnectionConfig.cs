using System.Data;
using static DBFacade.DataLayer.ConnectionService.DbConnectionService;

namespace DBFacade.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbConnectionConfig
    {
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <returns></returns>
        IDbConnection GetDbConnection();
        /// <summary>
        /// Checks the stored proc availability.
        /// </summary>
        /// <returns></returns>
        bool CheckStoredProcAvailability();
        /// <summary>
        /// Sets the available stored procs.
        /// </summary>
        /// <param name="storedProcs">The stored procs.</param>
        void SetAvailableStoredProcs(DbConnectionStoredProcedure[] storedProcs);
        /// <summary>
        /// Gets the available stored proc command text.
        /// </summary>
        /// <returns></returns>
        string GetAvailableStoredProcCommandText();
        /// <summary>
        /// Gets the type of the available stored proc command.
        /// </summary>
        /// <returns></returns>
        CommandType GetAvailableStoredProcCommandType();
        /// <summary>
        /// Availables the stored procs.
        /// </summary>
        /// <returns></returns>
        DbConnectionStoredProcedure[] AvailableStoredProcs();
    }
}
