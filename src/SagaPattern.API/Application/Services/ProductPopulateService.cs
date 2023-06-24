using SagaPattern.API.Data;
using SagaPattern.API.Data.Entities;
using SagaPattern.API.Data.Enums;

namespace SagaPattern.API.Application.Services;

public class ProductPopulateService
{

    private readonly CatalogDbContext _dbContext;

    public ProductPopulateService(CatalogDbContext context)
    {
        _dbContext = context;
    }

    public async Task Initialize()
    {
        if (_dbContext.Database.EnsureCreated())
        {
            var random = new Random();

            for (int i = 1; i < 100; i++)
            {
                _dbContext.Products.Add(new ProductModel()
                {
                    Title = $"Sandalia - {i}",
                    Description = $"Sandalia - {i}",
                    Price = random.NextDouble() * (100 - 1) + 1,
                    Quantity = random.Next(1, 100),
                    Status = random.Next(1, 3) == 1 ? EntityStatusEnum.Active : EntityStatusEnum.Inactive
                });
            }

            _dbContext.SaveChanges();
        };
    }
}