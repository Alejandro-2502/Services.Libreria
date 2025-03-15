using Libreria.Application.Generics;
using Libreria.Application.Responses;

namespace Libreria.Application.Interfaces;

public interface ILibroQuerysInteractor
{
    Task<Responses<List<LibroResponse>>> GetAll();
    Task<Responses<LibroResponse>> GetById(int id);
    Task<Responses<List<LibroResponse>>> GetByPriceGreaterThan(decimal price);
}
