namespace Ordering.Domain.Models;

public class Customer : Entitiy<CustomerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
}
