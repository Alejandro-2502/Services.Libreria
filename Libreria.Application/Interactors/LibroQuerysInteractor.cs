using AutoMapper;
using Libreria.Application.Gateways;
using Libreria.Application.Generics;
using Libreria.Application.Interfaces;
using Libreria.Application.Interfaces.ICommon;
using Libreria.Application.Interfaces.Redis;
using Libreria.Application.Responses;
using Libreria.Application.Responses.Common;
using System.Net;

namespace Libreria.Application.Interactors;

public class LibroQuerysInteractor : ILibroQuerysInteractor
{
    private readonly ILogServicesInteractor _logServicesInteractor;
    private readonly ILibroQuerysGateway _libroQuerysGateway;
    private readonly IMapper _mapper;
    private readonly IDistributedRedisCacheInteractor _distributedRedisCacheInteractor;

    public LibroQuerysInteractor(ILogServicesInteractor logServicesInteractor, ILibroQuerysGateway libroQuerysGateway,
                                 IMapper mapper, IDistributedRedisCacheInteractor distributedRedisCacheInteractor)
    {
        _logServicesInteractor = logServicesInteractor;
        _libroQuerysGateway = libroQuerysGateway;
        _mapper = mapper;
        _distributedRedisCacheInteractor = distributedRedisCacheInteractor;
    }
    public async Task<Responses<List<LibroResponse>>> GetAll()
    {
        try
        {
            var cacheKey = $"Libros";

            var result = await _distributedRedisCacheInteractor.GetOrSetAsync(cacheKey,
                async () =>
                {return await _libroQuerysGateway.GetAll();},
                Caches.Libros)!;
           
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
            var cacheKey = $"Libros_{id}";

            var result = await _distributedRedisCacheInteractor.GetOrSetAsync(cacheKey,
                async () =>
                {return await _libroQuerysGateway.GetById(id); },
                Caches.Libro_Id)!;

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
            
            var cacheKey = $"Libros_{price}";

            var result = await _distributedRedisCacheInteractor.GetOrSetAsync(cacheKey,
                async () =>
                {return await _libroQuerysGateway.GetByPriceGreaterThan(price);},
                Caches.Libro_Price)!;

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
