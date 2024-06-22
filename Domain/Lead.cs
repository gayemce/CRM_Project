namespace Domain;
public class Lead : IEntity
{
    public Lead(
        string firstName,
        string lastName,
        string email,
        string phone,
        string source) : this()
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Source = source;
    }
    protected Lead()
    {
        CreateTime = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public DateTime CreateTime { get; set; }
    public DateTime? UpdateDate { get; set; }
}
