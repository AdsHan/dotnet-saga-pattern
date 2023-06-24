using SagaPattern.API.Common;

namespace SagaPattern.API.Application.Messages.Sagas.CreateProduct.Steps;

public class CreateProductStartStep : Saga
{
    public CreateProductStartStep(string title, string description, double price, int quantity) : base()
    {
        Title = title;
        Description = description;
        Price = price;
        Quantity = quantity;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}
