namespace Domain;
public class Activity : IEntity
{
    public Activity(
        Customer customer,
        string description
    ) : this()
    {
        Customer = customer;
        Description = description;
    }

    protected Activity()
    {
        CreateDate = DateTime.Now;
    }
    public Guid Id { get; set; }
    public Customer Customer { get; set; } = new Customer();
    public string Description { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}