using FluentValidation;
using Libreria.Application.Messages;
using Libreria.Application.Request;

namespace Libreria.Application.Validations;

public class LibroValidations : AbstractValidator<LibroRequest>
{
    public LibroValidations()
    {
        RuleFor(lib => lib.Name).NotNull().NotEmpty().WithMessage(MessagesValidationsLibro.ValidationsLibroNulo)
             .MaximumLength(15).WithMessage(MessagesValidationsLibro.ValidationsLibroNameMax15Caracteres);
        RuleFor(lib => lib.Editorial).NotNull().NotEmpty().WithMessage(MessagesValidationsLibro.ValidationsLibroNulo)
            .MaximumLength(20).WithMessage(MessagesValidationsLibro.ValidationsLibroEditorialMax20Caracteres);
        RuleFor(lib => lib.QuantityPages).GreaterThan(0).WithMessage(MessagesValidationsLibro.ValidationsQuantityPagesMayor0);
        RuleFor(lib => lib.Price).GreaterThan(0).WithMessage(MessagesValidationsLibro.ValidationsLibroPriceMayor0);
    }
}
