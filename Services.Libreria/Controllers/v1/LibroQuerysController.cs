using Libreria.Application.Generics;
using Libreria.Application.Interfaces;
using Libreria.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Services.Libreria.API.Controllers.v1;

//[Authorize]
[ApiController]
public class LibroQuerysController : ControllerBase
{
    private readonly ResponseHttp _responseHttp;
    private readonly ILibroQuerysInteractor _libreriaQuerysInteractor;

    public LibroQuerysController(ILibroQuerysInteractor libreriaQuerysInteractor, ResponseHttp responseHttp)
    {
        _libreriaQuerysInteractor = libreriaQuerysInteractor;
        _responseHttp = responseHttp;
    }

    [HttpGet("All")]
    [ProducesResponseType(typeof(Responses<LibroResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses<LibroResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var response = await _libreriaQuerysInteractor.GetAll();
        return await _responseHttp.GetResponseHttp(response);
    }

    [HttpGet("ById")]
    [ProducesResponseType(typeof(Responses<LibroResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses<LibroResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _libreriaQuerysInteractor.GetById(id);
        return await _responseHttp.GetResponseHttp(response);
    }

    [HttpGet("GetByPriceGreaterThan")]
    [ProducesResponseType(typeof(Responses<LibroResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses<LibroResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByPriceGreaterThan(decimal price)
    {
        var response = await _libreriaQuerysInteractor.GetByPriceGreaterThan(price);
        return await _responseHttp.GetResponseHttp(response);
    }
}
