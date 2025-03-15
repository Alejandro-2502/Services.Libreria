using Libreria.Application.Configuration;
using Libreria.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Libreria.Application.Helpers;

public static class Token
{
    public static Claim[] GetClaim(UserDTO userDTO)
    {
        var claims = new[]
                        {new Claim("rol", userDTO.Rol.ToString()),
                         new Claim("mail", userDTO.Mail.ToString()),
                         new Claim("user", userDTO.Usuario.ToString())};

        return claims;
    }

    public static JwtSecurityToken GetJwtSecurityToken(Claim[] claim, SigningCredentials singIn)
    {
        var token = new JwtSecurityToken(ConfigHelper.ConfigJwt.Issuer,
                                         ConfigHelper.ConfigJwt.Audience,
                                         claims: claim,
                                         expires: DateTime.UtcNow.AddMinutes(40),
                                         signingCredentials: singIn);

        return token;
    }
}
