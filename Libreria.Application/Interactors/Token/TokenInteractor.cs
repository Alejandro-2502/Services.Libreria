using AutoMapper;
using Libreria.Application.Configuration;
using Libreria.Application.DTOs;
using Libreria.Application.Generics;
using Libreria.Application.Interfaces.ICommon;
using Libreria.Application.Interfaces.Token;
using Libreria.Application.Responses;
using Libreria.Application.Responses.Common;
using Libreria.Application.Responses.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace Libreria.Application.Interactors.Token;

public class TokenInteractor : ITokenInteractor
{
    private readonly ILogServicesInteractor _logServicesInteractor;
    private readonly IMapper _mapper;

    public TokenInteractor(ILogServicesInteractor logServicesInteractor, IMapper mapper)
    {
        _logServicesInteractor = logServicesInteractor;
        _mapper = mapper;
    }

    public async Task<Responses<TokenResponse>> Generate(UsuarioResponse usuarioResponse)
    {
        try
        {
            var userDTO = _mapper.Map<UserDTO>(usuarioResponse);
            
            var claims = Helpers.Token.TokenHelper.GetClaim(userDTO);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigHelper.ConfigJwt.Key));

            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var jwtSecurity = Helpers.Token.TokenHelper.GetJwtSecurityToken(claims, singIn);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurity);

            var responseToken = Mappers.Token.TokenResponseMapper.ToTokenResponse(token);

            return await Response.Ok(HttpStatusCode.OK, string.Empty, responseToken);
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(TokenInteractor) + nameof(Generate) + ex.Message);
            return await Response.Error<TokenResponse>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
