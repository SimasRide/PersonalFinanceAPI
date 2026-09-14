using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models;

public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
}
