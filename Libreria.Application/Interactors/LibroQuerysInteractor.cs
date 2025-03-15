using AutoMapper;
using Libreria.Application.Gateways;
using Libreria.Application.Generics;
using Libreria.Application.Interfaces;
using Libreria.Application.Interfaces.ICommon;
using Libreria.Application.Responses;
using Libreria.Application.Responses.Common;
using System.Net;

namespace Libreria.Application.Interactors;

public class LibroQuerysInteractor : ILibroQuerysInteractor
{
    private readonly ILogServicesInteractor _logServicesInteractor;
    private readonly ILibroQuerysGateway _libroQuerysGateway;
    private readonly IMapper _mapper;

    public LibroQuerysInteractor(ILogServicesInteractor logServicesInteractor, ILibroQuerysGateway libroQuerysGateway, 
                                 IMapper mapper)
    {
        _logServicesInteractor = logServicesInteractor;
        _libroQuerysGateway = libroQuerysGateway;
        _mapper = mapper;
    }
    public async Task<Responses<List<LibroResponse>>> GetAll()
    {
        try
        {
            var result = await _libroQuerysGateway.GetAll();
           
            if (result is null) 
                return await Response.Error<List<LibroResponse>>(HttpStatusCode.NotFound, Messages.MessageLibreria.NotFound);

            var response = _mapper.Map<List<LibroResponse>>(result);

           return await Response.Ok(HttpStatusCode.OK, string.Empty,response);
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(LibroQuerysInteractor) + nameof(GetAll) + ex.Message);
            return await Response.Error<List<LibroResponse>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Responses<LibroResponse>> GetById(int id)
    {
        try 
        {
            var result = await _libroQuerysGateway.GetById(id);

            if (result is null)
                return await Response.Error<LibroResponse>(HttpStatusCode.NotFound, Messages.MessageLibreria.NotFound);

            var response = _mapper.Map<LibroResponse>(result);
            
           return await Response.Ok(HttpStatusCode.OK, string.Empty, response);
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(LibroQuerysInteractor) + nameof(GetById) + ex.Message);
            return await Response.Error<LibroResponse>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Responses<List<LibroResponse>>> GetByPriceGreaterThan(decimal price)
    {
        try
        {
            var result = await _libroQuerysGateway.GetByPriceGreaterThan(price);

            if (result is null)
                return await Response.Error<List<LibroResponse>>(HttpStatusCode.NotFound, Messages.MessageLibreria.NotFound);

            var response = _mapper.Map<List<LibroResponse>>(result);
            
           return await Response.Ok(HttpStatusCode.OK, string.Empty, response);
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(LibroQuerysInteractor) + nameof(GetByPriceGreaterThan) + ex.Message);
            return await Response.Error<List<LibroResponse>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
