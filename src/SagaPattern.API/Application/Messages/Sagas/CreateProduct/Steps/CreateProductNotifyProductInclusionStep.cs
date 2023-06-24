using NServiceBus;

namespace SagaPattern.API.Application.Messages.Sagas.CreateProduct.Steps;

public record CreateProductNotifyProductInclusionStep(Guid SagaId) : IMessage;
