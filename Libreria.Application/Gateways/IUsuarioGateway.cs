using Libreria.Application.DTOs;

namespace Libreria.Application.Gateways
{
    public interface IUsuarioGateway
    {
        Task<UserDTO> GetUser(string user, string password);
    }
}
