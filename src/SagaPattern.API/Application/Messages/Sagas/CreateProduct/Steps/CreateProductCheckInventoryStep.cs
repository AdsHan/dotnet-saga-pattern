using NServiceBus;

namespace SagaPattern.API.Application.Messages.Sagas.CreateProduct.Steps;

public record CreateProductCheckInventoryStep(Guid SagaId) : IMessage;
