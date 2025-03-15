using Libreria.Application.DTOs;
using Libreria.Application.Generics;
using Libreria.Application.Responses;
using Libreria.Application.Responses.Token;

namespace Libreria.Application.Interfaces.Token;

public interface ITokenInteractor
{
    Task<Responses<TokenResponse>> Generate(UsuarioResponse usuarioResponse);
}
