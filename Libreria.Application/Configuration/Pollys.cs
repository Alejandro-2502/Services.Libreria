namespace Libreria.Application.Configuration
{
    public class Pollys
    {
        public Command Commands { get; set; }
        public Querys Querys { get; set; }
    }

    public class Command
    {
        public int Retry { get; set; }
        public int FromSeconds { get; set; }
    }

    public class Querys
    {
        public int Retry { get; set; }
        public int FromSeconds { get; set; }
    }
}
