namespace Ordering.Domain.Models;

/// <summary>
/// Aggregate root that represents a customer's order.
/// In DDD an Order is an aggregate root that controls its OrderItems and raises domain events.
/// Keep behavior (methods) on the aggregate to maintain invariants — this simple example focuses on structure.
/// </summary>
public class Order : Aggregate<OrderId>
{
    // Internal mutable list of order items. It's kept private so the aggregate
    // controls changes (adding/removing items) and preserves invariants.
    private readonly List<OrderItem> _orderItems = new();

    // Expose order items as a read-only list to callers so they can't modify internal state directly.
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    /// <summary>
    /// Id of the customer who made the order. The setter is private to ensure only the aggregate
    /// or factory code can assign it, preventing external code from changing ownership.
    /// </summary>
    public CustomerId CustomerId { get; private set; } = default!;

    /// <summary>
    /// A human friendly name for the order (could be used in UIs).
    /// Kept simple here; in real apps you may store more metadata.
    /// </summary>
    public OrderName OrderName { get; private set; } = default!;

    /// <summary>
    /// Shipping address for the order. The type is a value object (Address) in the domain.
    /// Value objects are immutable and represent a description rather than identity.
    /// </summary>
    public Address ShippingAddress { get; private set; } = default!;

    /// <summary>
    /// Billing address for the order. Often the same as ShippingAddress but kept separate for clarity.
    /// </summary>
    public Address BillingAddress { get; private set; } = default!;

    /// <summary>
    /// Payment details for the order. This might also be a value object or reference to a payment entity.
    /// </summary>
    public Payment Payment { get; private set; } = default!;

    /// <summary>
    /// Current status of the order. Initialized to Pending by default.
    /// Use methods on the aggregate to change status so you can validate transitions.
    /// </summary>
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    /// <summary>
    /// Computed total price of the order. This is calculated from the sum of each
    /// order item's price multiplied by its quantity. Because it's derived, it has no setter.
    /// </summary>
    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }

    // Note: For learning purposes this class focuses on structure and comments.
    // In a production DDD model you would add constructors, factories and methods
    // such as AddItem, RemoveItem, ChangeQuantity, PlaceOrder, CancelOrder, etc.

    public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
    {
        var order = new Order
        {
            Id = id,

            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending
        };

        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = OrderStatus.Pending;

        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var orderItem = new OrderItem(Id, productId, quantity, price);
        _orderItems.Add(orderItem);
    }

    public void remove(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
        }

    }
}