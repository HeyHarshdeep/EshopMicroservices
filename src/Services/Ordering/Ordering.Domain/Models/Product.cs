namespace Ordering.Domain.Models;

public class Product : Entitiy<ProductId>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
}
