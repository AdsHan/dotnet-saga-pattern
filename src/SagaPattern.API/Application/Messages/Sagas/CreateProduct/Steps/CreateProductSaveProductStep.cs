using NServiceBus;

namespace SagaPattern.API.Application.Messages.Sagas.CreateProduct.Steps;

public record CreateProductSaveProductStep(Guid SagaId) : IMessage;
