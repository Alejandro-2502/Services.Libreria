using Libreria.Application.Interfaces;
using Libreria.Application.Interfaces.Token;
using Libreria.Application.Request.Token;
using Microsoft.AspNetCore.Mvc;

namespace Services.Libreria.API.Controllers.Token;

public class TokenController : ControllerBase
{
    private readonly IUsuarioInteractor _usuarioInteractor;
    private readonly ITokenInteractor _tokenInteractor;
    private readonly ResponseHttp _responseHttp;
    public TokenController(IUsuarioInteractor usuarioInteractor, ITokenInteractor tokenInteractor, ResponseHttp responseHttp)
    {
        _usuarioInteractor = usuarioInteractor;
        _tokenInteractor = tokenInteractor;
        _responseHttp = responseHttp;
    }

    [HttpPost("generar")]
    public async Task<IActionResult> GenerateToken([FromBody] UsarioTokenRequest request)
    {
        var response = await _usuarioInteractor.GetUser(request);

        if (response is null)
            return Unauthorized();

        var tokenResponse = await _tokenInteractor.Generate(response.Response);
        return await _responseHttp.GetResponseHttp(tokenResponse);
    }
}
