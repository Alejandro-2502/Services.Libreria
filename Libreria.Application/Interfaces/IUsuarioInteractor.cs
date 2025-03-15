using Libreria.Application.Generics;
using Libreria.Application.Request.Token;
using Libreria.Application.Responses;

namespace Libreria.Application.Interfaces;

public interface IUsuarioInteractor
{
    Task<Responses<UsuarioResponse>> GetUser(UsarioTokenRequest id);
}
