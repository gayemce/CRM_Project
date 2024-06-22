namespace Domain;
public class Product : IEntity
{
    public Product(
        string name,
        decimal price,
        string description) : this()
    {
        Name = name;
        Price = price;
        Description = description;
    }

    protected Product()
    {
        CreateDate = DateTime.Now;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
