using Microsoft.AspNetCore.Identity;

namespace Domain;
public sealed class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole()
    {
        Id = Guid.NewGuid();
    }

    public ApplicationRole(string roleName) : this()
    {
        Name = roleName;
    }
}

public sealed class ApplicationRoleClaim : IdentityRoleClaim<Guid> { }

public sealed class ApplicationUserRole : IdentityUserRole<Guid> { }
