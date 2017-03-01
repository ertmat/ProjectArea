using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectArea.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
