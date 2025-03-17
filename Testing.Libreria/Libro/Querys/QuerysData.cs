using Libreria.Application.DTOs;
using Libreria.Application.Responses;

namespace Testing.Libreria.Libro.Querys;

public static class QuerysData
{
    public static List<LibroResponse> GetLibroResponse()
    {
         List<LibroResponse> responses = new();

        var response1 = new LibroResponse { Id = 1, Name = "El principito", Editorial = "Saint-Exupery", QuantityPages = 120, Price = 12541.11M };
        var response2 = new LibroResponse { Id = 2, Name = "Señor de los anillos", Editorial = "George Allen", QuantityPages = 215, Price = 21681.41M };
        var response3 = new LibroResponse { Id = 3, Name = "Harry Poter", Editorial = "Bloomsbury Publishing", QuantityPages = 198, Price = 25365.20M };

        responses.Add(response1);
        responses.Add(response2);
        responses.Add(response3);

        return responses;
    }
    public static List<LibroDTO> GetLibroDTO()
    {
        List<LibroDTO> LibrosDTO = new();

        var libroDTO1 = new LibroDTO { Id = 1, Name = "El principito", Editorial = "Saint-Exupery", QuantityPages = 120, Price = 12541.11M };
        var libroDTO2 = new LibroDTO { Id = 2, Name = "Señor de los anillos", Editorial = "George Allen", QuantityPages = 215, Price = 21681.41M };
        var libroDTO3 = new LibroDTO { Id = 3, Name = "Harry Poter", Editorial = "Bloomsbury Publishing", QuantityPages = 198, Price = 25365.20M };

        LibrosDTO.Add(libroDTO1);
        LibrosDTO.Add(libroDTO2);
        LibrosDTO.Add(libroDTO3);

        return LibrosDTO;
    }
    public static List<LibroDTO> GetLibrosDTONotFound()
    {
        var LibrosDTO = new List<LibroDTO>();
        LibrosDTO = null;

        return LibrosDTO;
    }
    public static List<LibroResponse> GetLibrosResponseNotFound()
    {
        var response = new List<LibroResponse>();

        return response;
    }
}
