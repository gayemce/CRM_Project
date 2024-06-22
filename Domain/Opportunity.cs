namespace Domain;

public class Opportunity : IEntity
{
    public Opportunity(
        Customer customer,
        string description,
        decimal value) : this()
    {
        Customer = customer;
        Description = description;
        Value = value;
    }

    protected Opportunity()
    {
        CreateDate = DateTime.Now;
    }

    public Guid Id { get; set; }
    public Customer Customer { get; set; } = new Customer();
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public DateTime CloseDate { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}