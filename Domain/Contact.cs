namespace Domain;
public class Contact : IEntity
{
    public Contact(
        Customer customer,
        string firstName,
        string lastName,
        string email,
        string phone) : this()
    {
        Customer = customer;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
    }

    protected Contact()
    {
        CreateDate = DateTime.Now;
    }

    public Guid Id { get; set; }
    public Customer Customer { get; set; } = new Customer();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
