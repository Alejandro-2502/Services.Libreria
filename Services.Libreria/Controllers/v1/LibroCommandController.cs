using Libreria.Application.Generics;
using Libreria.Application.Interfaces;
using Libreria.Application.Request;
using Libreria.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Services.Libreria.API.Controllers.v1;

[Authorize]
[ApiController]
[Route("")]
public class LibroCommandController : ControllerBase
{
    private readonly ResponseHttp _responseHttp;
    private readonly ILibroCommandInteractor _libreriaCommandInteractor;

    public LibroCommandController(ResponseHttp responseHttp, ILibroCommandInteractor libreriaCommandInteractor)
    {
        _responseHttp = responseHttp;
        _libreriaCommandInteractor = libreriaCommandInteractor;
    }

    [HttpPost()]
    [ProducesResponseType(typeof(Responses<LibroResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses<LibroResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] LibroRequest libroRequest)
    {
        if (libroRequest == null)
            return BadRequest();

        var response = await _libreriaCommandInteractor.Add(libroRequest);
        return await _responseHttp.GetResponseHttp(response);
    }

    [HttpPut()]
    [ProducesResponseType(typeof(Responses<LibroResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses<LibroResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] LibroIdRequest libroRequest)
    {
        if (libroRequest == null)
            return BadRequest();

        var response = await _libreriaCommandInteractor.Update(libroRequest);
        return await _responseHttp.GetResponseHttp(response);
    }

    [HttpDelete()]
    [ProducesResponseType(typeof(Responses<LibroResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses<LibroResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest();

        var response = await _libreriaCommandInteractor.Delete(id);
        return await _responseHttp.GetResponseHttp(response);
    }
}
