using System;
namespace back_end_plante.Configurations;

public class ConnectionStringConfiguration
{
    public string MongoPlanteUri => Environment.GetEnvironmentVariable("MONGODB_URI");
}