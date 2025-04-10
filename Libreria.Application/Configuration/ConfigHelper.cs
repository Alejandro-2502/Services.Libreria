namespace Libreria.Application.Configuration;

public class ConfigHelper
{
    public static ConfigSqlServer? ConfigSqlServer { get; set; }
    public static ConfigJwt ConfigJwt { get; set; }
    public static ServerRedis? ServerRedis { get; set; }
    public static TTLCacheRedis? TTLCaches { get; set; }
    public static Pollys? Pollys { get; set; }
}
