using System.Threading.Tasks;
using DBFacade.DataLayer.CommandConfig;

namespace DBFacade.DataLayer.Manifest
{
    public interface IDbManifestMethod
    {
        IDbCommandConfig Config { get; }
        Task<IDbCommandConfig> GetConfigAsync();
    }
}