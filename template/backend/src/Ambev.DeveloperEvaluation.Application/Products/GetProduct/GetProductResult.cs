namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductResult
{
    public Guid Id { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public DateTime CreatedAt { get; set; }
}
