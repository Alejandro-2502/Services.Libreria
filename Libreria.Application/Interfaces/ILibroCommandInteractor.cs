using Libreria.Application.DTOs;
using Libreria.Application.Generics;
using Libreria.Application.Request;

namespace Libreria.Application.Interfaces
{
    public interface ILibroCommandInteractor
    {
        Task<Responses<bool>> Add(LibroRequest libreriaDTO);
        Task<Responses<bool>> Update(LibroRequest libreriaDTO);
        Task<Responses<bool>> Delete(int id);
    }
}
