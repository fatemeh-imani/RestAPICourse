using Microsoft.AspNetCore.Identity;

namespace Movies.Applications.DataBaces.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Rating> Ratings { get; set; } = [];
    }
}
