using AutoMapper;
using Libreria.Application.DTOs;
using Libreria.Application.Gateways;
using Libreria.Application.Generics;
using Libreria.Application.Helpers.PollyRetry;
using Libreria.Application.Interfaces;
using Libreria.Application.Interfaces.ICommon;
using Libreria.Application.Request;
using Libreria.Application.Responses.Common;
using System.Net;

namespace Libreria.Application.Interactors;

public class LibroCommandInteractor : ILibroCommandInteractor
{
    private readonly ILogServicesInteractor _logServicesInteractor;
    private readonly ILibroCommandGateway _libroCommandGateway;
    private readonly IValidationsInteractor _validationsInteractor;
    private readonly IMapper _mapper;

    public LibroCommandInteractor(ILogServicesInteractor logServicesInteractor, ILibroCommandGateway libroCommandGateway, 
                                  IMapper mapper, IValidationsInteractor validationsInteractor)
    {
        _logServicesInteractor = logServicesInteractor;
        _libroCommandGateway = libroCommandGateway;
        _mapper = mapper;
        _validationsInteractor = validationsInteractor;
    }
    public async Task<Responses<bool>> Delete(int id)
    {
        try
        {
            var pollyRetry = PollyCommand.GetPollyRetry();

            var result = await pollyRetry.ExecuteAsync(async () =>
            {
                return await _libroCommandGateway.Delete(id);
            });

            if (!result)
                return await Response.Error<bool>(HttpStatusCode.UnprocessableEntity, Messages.MessageLibreria.DeleteError);

            return await Response.Ok(HttpStatusCode.OK, Messages.MessageLibreria.DeleteOk, true);
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(LibroCommandInteractor) + nameof(Delete) + ex.Message);
            return await Response.Error<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Responses<bool>> Add(LibroRequest libroRequest)
    {
        try
        {
            if (libroRequest is null)
                return await Response.Error<bool>(HttpStatusCode.BadRequest, Messages.MessageLibreria.AddConflicto);

            var resultValidator = await _validationsInteractor.Validator(libroRequest);

            if (resultValidator.Any())
                return await Response.ErrorsList<bool>(HttpStatusCode.BadRequest, resultValidator);

            var resultDTO = _mapper.Map<LibroDTO>(libroRequest);

            var pollyRetry = PollyCommand.GetPollyRetry();

            var result = await pollyRetry.ExecuteAsync(async () =>
            {
                return await _libroCommandGateway.Add(resultDTO);
            });

            if (!result)
                return await Response.Error<bool>(HttpStatusCode.UnprocessableEntity, Messages.MessageLibreria.AddError);

            return await Response.Ok(HttpStatusCode.OK, Messages.MessageLibreria.AddOk, true);
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(LibroCommandInteractor) + nameof(Add) + ex.Message);
            return await Response.Error<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Responses<bool>> Update(LibroRequest libroRequest)
    {
        try
        {
            if (libroRequest is null)
                return await Response.Error<bool>(HttpStatusCode.BadRequest, Messages.MessageLibreria.UpdateConflicto);

            var resultValidator = await _validationsInteractor.Validator(libroRequest);

            if (resultValidator.Any())
                return await Response.ErrorsList<bool>(HttpStatusCode.BadRequest, resultValidator);

            var resultDTO = _mapper.Map<LibroDTO>(libroRequest);

            var pollyRetry = PollyCommand.GetPollyRetry();

            var result = await pollyRetry.ExecuteAsync(async () =>
            {
                return await _libroCommandGateway.Update(resultDTO);
            });

            if (!result)
                return await Response.Error<bool>(HttpStatusCode.UnprocessableEntity, Messages.MessageLibreria.UpdateError, false);

            return await Response.Ok(HttpStatusCode.OK, Messages.MessageLibreria.UpdateOk, true);
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(LibroCommandInteractor) + nameof(Update) + ex.Message);
            return await Response.Error<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
