using Libreria.Application.DTOs;

namespace Libreria.Application.Gateways;

public interface ILibroCommandGateway
{
    Task<bool> Add(LibroDTO libroDTO);
    Task<bool> Update(LibroDTO libroDTO);
    Task<bool> Delete(int id);
}
