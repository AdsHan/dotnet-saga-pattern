using NServiceBus;

namespace SagaPattern.API.Application.Messages.Sagas.CreateProduct;

public class CreateProductSagaData : ContainSagaData
{
    // Identificador da saga
    public Guid SagaId { get; set; }

    // Campos compartilhados
    public bool OrderAccepted { get; set; }

    // Dados do produti
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}
