using NServiceBus;
using SagaPattern.API.Application.Messages.Sagas.CreateProduct.Steps;
using SagaPattern.API.Application.Messages.Validators;
using SagaPattern.API.Data.Entities;

namespace SagaPattern.API.Application.Messages.Sagas.CreateProduct;

public class CreateProductSaga : Saga<CreateProductSagaData>,
    IAmStartedByMessages<CreateProductStartStep>,
    IHandleMessages<CreateProductValidateProductStep>,
    IHandleMessages<CreateProductSaveProductStep>,
    IHandleMessages<CreateProductCheckInventoryStep>,
    IHandleMessages<CreateProductNotifyProductInclusionStep>,
    IHandleMessages<CreateProductIndexProductStep>,
    IHandleMessages<CreateProductCompleteStep>
{

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CreateProductSagaData> mapper)
    {
        // Mapear a propriedade que identifica a Saga
        mapper.MapSaga(saga => saga.SagaId).ToMessage<CreateProductStartStep>(msg => msg.SagaId);
        mapper.MapSaga(saga => saga.SagaId).ToMessage<CreateProductValidateProductStep>(msg => msg.SagaId);
        mapper.MapSaga(saga => saga.SagaId).ToMessage<CreateProductSaveProductStep>(msg => msg.SagaId);
        mapper.MapSaga(saga => saga.SagaId).ToMessage<CreateProductCheckInventoryStep>(msg => msg.SagaId);
        mapper.MapSaga(saga => saga.SagaId).ToMessage<CreateProductNotifyProductInclusionStep>(msg => msg.SagaId);
        mapper.MapSaga(saga => saga.SagaId).ToMessage<CreateProductIndexProductStep>(msg => msg.SagaId);
        mapper.MapSaga(saga => saga.SagaId).ToMessage<CreateProductCompleteStep>(msg => msg.SagaId);
    }

    public async Task Handle(CreateProductStartStep message, IMessageHandlerContext context)
    {
        // Lógica de inicialização da Saga
        Data.Title = message.Title;
        Data.Description = message.Description;
        Data.Price = message.Price;
        Data.Quantity = message.Quantity;

        await context.SendLocal(new CreateProductValidateProductStep(message.SagaId));
    }

    public async Task Handle(CreateProductValidateProductStep message, IMessageHandlerContext context)
    {
        var validationResult = new CreateProductValidator().Validate(Data);

        await context.SendLocal(validationResult.IsValid ? new CreateProductSaveProductStep(message.SagaId) : new CreateProductCompleteStep(message.SagaId));
    }

    public async Task Handle(CreateProductSaveProductStep message, IMessageHandlerContext context)
    {

        var product = new ProductModel()
        {
            Title = Data.Title,
            Description = Data.Description,
            Price = Data.Price,
            Quantity = Data.Quantity
        };

        // _dbContext.Add(product);

        //await _dbContext.SaveChangesAsync();

        await context.SendLocal(new CreateProductCheckInventoryStep(message.SagaId));
    }

    public async Task Handle(CreateProductCheckInventoryStep message, IMessageHandlerContext context)
    {

        // Lógica para verificar o estoque

        await context.SendLocal(new CreateProductNotifyProductInclusionStep(message.SagaId));
    }

    public async Task Handle(CreateProductNotifyProductInclusionStep message, IMessageHandlerContext context)
    {

        // Lógica para notificar os interessados sobre a inclusão do produto

        await context.SendLocal(new CreateProductIndexProductStep(message.SagaId));
    }

    public async Task Handle(CreateProductIndexProductStep message, IMessageHandlerContext context)
    {

        // Lógica para indexar o produto em um mecanismo de busca

        await context.SendLocal(new CreateProductCompleteStep(message.SagaId));
    }

    public Task Handle(CreateProductCompleteStep message, IMessageHandlerContext context)
    {
        // Lógica para finalizar o processo de inclusão do produto

        MarkAsComplete();

        return Task.CompletedTask;
    }

}
