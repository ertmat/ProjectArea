using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectArea.Entities
{
    public class User : IdentityUser
    {
        public static object Identity { get; internal set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}