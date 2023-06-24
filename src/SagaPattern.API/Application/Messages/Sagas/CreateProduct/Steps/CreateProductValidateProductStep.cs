using NServiceBus;

namespace SagaPattern.API.Application.Messages.Sagas.CreateProduct.Steps;

public record CreateProductValidateProductStep(Guid SagaId) : IMessage;