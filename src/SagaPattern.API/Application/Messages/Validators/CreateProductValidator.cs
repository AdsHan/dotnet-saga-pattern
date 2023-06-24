using FluentValidation;
using SagaPattern.API.Application.Messages.Sagas.CreateProduct;

namespace SagaPattern.API.Application.Messages.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductSagaData>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("O título não foi informado!");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("O descrição não foi informado!");

        RuleFor(x => x.Price)
            .NotEmpty()
                .WithMessage("O preço não foi informado!");

        RuleFor(x => x.Quantity)
            .NotEmpty()
                .WithMessage("A quantidade não foi informado!");
    }
}
