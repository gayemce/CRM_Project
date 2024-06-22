using Microsoft.AspNetCore.Identity;

namespace Domain;
public sealed class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        Id = Guid.NewGuid();
        SecurityStamp = Guid.NewGuid().ToString();
    }

    public ApplicationUser(string userName) : this()
    {
        UserName = userName;
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string DisplayName => (FirstName + " " + LastName).Trim();
}
