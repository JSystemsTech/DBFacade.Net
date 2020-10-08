using System.Threading.Tasks;
using DbFacade.DataLayer.CommandConfig;

namespace DbFacade.DataLayer.Manifest
{
    public interface IDbManifestMethod
    {
        IDbCommandConfig GetConfig();
        Task<IDbCommandConfig> GetConfigAsync();
    }
}