using System.Net.Sockets;
using MongoDB.Driver;

namespace back_end_plante.Clients
{
    public static class MongoDbClient
    {
        private static MongoClient _client { get; set; }

        public static MongoClient GetClient(string connectionString)
        {
            if (_client == null)
            {
                CreateClient(connectionString);
            }

            return _client;
        }

        private static void CreateClient(string connectionString)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            settings.MaxConnectionLifeTime = new System.TimeSpan(6, 0, 0);
            settings.ClusterConfigurator = b => b.ConfigureTcp(
                tcp => tcp.With(
                    socketConfigurator:
                    (Action<Socket>)(
                        s => s.SetSocketOption(
                            SocketOptionLevel.Socket,
                            SocketOptionName.KeepAlive,
                            true))));

            settings.MaxConnectionPoolSize = 1000;
            settings.ReadPreference = ReadPreference.Secondary;
            settings.ApplicationName = "plante";
            _client = new MongoClient(settings);
        }
    }
}