using Libreria.Application.DTOs;

namespace Libreria.Application.Gateways;

public interface ILibroQuerysGateway
{
    Task<List<LibroDTO>> GetAll();
    Task<LibroDTO> GetById(int id);
    Task<List<LibroDTO>> GetByPriceGreaterThan(decimal price);
}
