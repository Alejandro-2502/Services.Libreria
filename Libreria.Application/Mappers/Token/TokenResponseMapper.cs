using Libreria.Application.Messages;
using Libreria.Application.Responses.Token;

namespace Libreria.Application.Mappers.Token;

public class TokenResponseMapper
{
    public static TokenResponse ToTokenResponse(string token)
    {
        return new TokenResponse()
        {
            Success = true,
            Messages = MessageToken.GenerateTokenOk,
            Token = token
        };

    }
}
