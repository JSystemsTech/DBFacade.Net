using DBFacade.DataLayer.CommandConfig;
using System.Threading.Tasks;

namespace DBFacade.DataLayer.Manifest
{
    public interface IDbManifestMethod
    {
        IDbCommandConfig Config{ get; }  
        Task<IDbCommandConfig> GetConfigAsync();
    }  

}
