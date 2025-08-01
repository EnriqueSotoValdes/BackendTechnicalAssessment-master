using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.Dtos;

public class ClientDtoValidator : AbstractValidator<ClientDto>
{
    public ClientDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(default(int))
            .WithMessage("El Id del cliente es necesario.");

        RuleFor(x => x.DocType)
            .NotEmpty()
            .WithMessage("El tipo de documento es necesario.")
            .MaximumLength(25)
            .WithMessage("El tipo de documento tiene una longitud máxima de 25 caracteres.");

        RuleFor(x => x.DocNum)
            .NotEmpty()
            .WithMessage("El número de documento es necesario.")
            .MaximumLength(12)
            .WithMessage("El número de documento tiene una longitud máxima de 12 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("El email es necesario.")
            .EmailAddress()
            .WithMessage("El email no cumple el formato adecuado.");

        // ... More validations (It is not necessary to create them)
        When(x => x.DocType.ToLower() == "nif", () =>
        {
            RuleFor(x => x.DocNum)
                .Matches(@"^\d{8}[A-Za-z]$").WithMessage("El formato del NIF no es válido. Debe tener 8 números y una letra.");
        });
    }
}
