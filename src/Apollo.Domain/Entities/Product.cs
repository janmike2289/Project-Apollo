namespace Apollo.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }

    private Product()
    {
    }

    public Product(string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegative(price);

        Id = Guid.NewGuid();
        Name = name.Trim();
        Price = price;
        IsActive = true;
    }

    public void Update(string name, decimal price, bool isActive)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegative(price);

        Name = name.Trim();
        Price = price;
        IsActive = isActive;
    }
}
