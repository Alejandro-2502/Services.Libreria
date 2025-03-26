namespace Libreria.Application.Configuration;

public class TTLCacheRedis
{
    public int TTLLibroAbsoluteExpire { get; set; }
    public int TTLLibroSlidingExpire { get; set; }
}
