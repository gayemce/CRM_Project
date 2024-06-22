namespace Domain;
public class Customer : IEntity
{
    public Customer(
    string name,
    string email,
    string phone,
    string address) : this()
    {
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
    }

    public Customer()
    {
        CreateDate = DateTime.Now;
        Contacts = new List<Contact>();
        Opportunities = new List<Opportunity>();
        Activities = new List<Activity>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public ICollection<Contact> Contacts { get; set; }
    public ICollection<Opportunity> Opportunities { get; set; }
    public ICollection<Activity> Activities { get; set; }
}
