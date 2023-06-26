using back_end_plante.Clients;
using back_end_plante.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace back_end_plante.Repository;

public class MsprPlanteRepositoryBase
{
    protected readonly ConnectionStringConfiguration _connectionStringConfiguration;
    private readonly string _mongoPocProviderDb;
    protected string MongoPocProviderDbCnx => _mongoPocProviderDb;
    protected readonly string MongoPocProviderName = "Arosa-je";

    public MsprPlanteRepositoryBase(IOptions<ConnectionStringConfiguration> connectionStringConfiguration)
    {
        _connectionStringConfiguration = connectionStringConfiguration.Value;
        _mongoPocProviderDb = _connectionStringConfiguration.MongoPlanteUri;
    }

    internal MongoClient GetClient()
    {
        return MongoDbClient.GetClient(MongoPocProviderDbCnx);
    }
}