using NServiceBus;

namespace SagaPattern.API.Common;

public abstract class Saga : IMessage
{
    protected Saga()
    {
        SagaId = Guid.NewGuid();
    }

    public Guid SagaId { get; set; }
}
