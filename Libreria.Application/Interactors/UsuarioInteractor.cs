using AutoMapper;
using Libreria.Application.Gateways;
using Libreria.Application.Generics;
using Libreria.Application.Interfaces;
using Libreria.Application.Interfaces.ICommon;
using Libreria.Application.Messages;
using Libreria.Application.Request.Token;
using Libreria.Application.Responses;
using Libreria.Application.Responses.Common;
using System.Net;

namespace Libreria.Application.Interactors;

public class UsuarioInteractor : IUsuarioInteractor
{
    private readonly IUsuarioGateway _usuarioGateway;
    private readonly ILogServicesInteractor _logServicesInteractor;
    private readonly IMapper _mapper;

    public UsuarioInteractor(IUsuarioGateway usuarioGateway, ILogServicesInteractor logServicesInteractor, IMapper mapper)
    {
        _usuarioGateway = usuarioGateway;
        _logServicesInteractor = logServicesInteractor;
        _mapper = mapper;
    }

    public async Task<Responses<UsuarioResponse>> GetUser(UsarioTokenRequest  usarioTokenRequest)
    {
        try
        {
            var result = await _usuarioGateway.GetUser(usarioTokenRequest.User, usarioTokenRequest.Password);

            if (result is null)
                return await Response.Error<UsuarioResponse>(HttpStatusCode.Unauthorized, MessageLibreria.UsuarioNoAutorizado);

            var response = _mapper.Map<UsuarioResponse>(result);

            return await Response.Ok(HttpStatusCode.OK, string.Empty, response);

        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(UsuarioInteractor) + nameof(GetUser) + ex.Message);
            return await Response.Error<UsuarioResponse>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
