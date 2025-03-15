namespace Libreria.Application.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Editorial { get; set; }
        public int QuantityPages { get; set; }
        public decimal Price { get; set; }
    }
}
