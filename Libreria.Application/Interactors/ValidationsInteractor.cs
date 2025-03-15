using Libreria.Application.Interfaces;
using Libreria.Application.Interfaces.ICommon;
using Libreria.Application.Request;
using Libreria.Application.Validations;

namespace Libreria.Application.Interactors;

public class ValidationsInteractor : IValidationsInteractor
{
    private readonly LibroValidations _libroValidations;
    private readonly ILogServicesInteractor _logServicesInteractor;

    public ValidationsInteractor(LibroValidations libroValidations, ILogServicesInteractor logServicesInteractor)
    {
        _libroValidations = libroValidations;
        _logServicesInteractor = logServicesInteractor;
    }

    public async Task<List<string>> Validator(LibroRequest libroRequest)
    {
        try
        {
            List<string> Messages = new();

            var result = _libroValidations.Validate(libroRequest);

            if (!result.IsValid)
            {
                var errors = result.Errors
                           .Select(e => Task.Run(() => e.ErrorMessage))
                           .ToArray();

                var resultMessages = await Task.WhenAll(errors);
                Messages.AddRange(resultMessages);
            }
            return Messages;
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(ValidationsInteractor) + nameof(Validator) + ex.Message);
            throw;
        }
    }
}
