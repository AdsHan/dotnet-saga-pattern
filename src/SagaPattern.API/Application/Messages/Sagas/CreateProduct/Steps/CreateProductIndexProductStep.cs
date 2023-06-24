using NServiceBus;

namespace SagaPattern.API.Application.Messages.Sagas.CreateProduct.Steps;

public record CreateProductIndexProductStep(Guid SagaId) : IMessage;
