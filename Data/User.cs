using Microsoft.AspNetCore.Identity;

namespace PBugTracker.Data
{
    public class User : IdentityUser
    {
        public string? FirstNameLastName { get; set; }
    }
}
