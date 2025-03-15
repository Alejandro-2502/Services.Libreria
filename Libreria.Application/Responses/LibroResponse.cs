namespace Libreria.Application.Responses
{
    public class LibroResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Editorial { get; set; }
        public int QuantityPages { get; set; }
        public decimal Price { get; set; }
    }
}
