using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectArea.Entities
{
    public class ProjectDbContext : IdentityDbContext<User>
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
