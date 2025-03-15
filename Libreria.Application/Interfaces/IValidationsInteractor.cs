using Libreria.Application.Request;

namespace Libreria.Application.Interfaces
{
    public interface IValidationsInteractor
    {
        Task<List<string>> Validator(LibroRequest libroRequest);
    }
}
