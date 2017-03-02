using Microsoft.AspNetCore.Http;
using ProjectArea.Entities;
using System.Linq;
using System.Security.Claims;

namespace ProjectArea.Services
{
    public interface IUserManagerData
    {
        User Get(string id);
        string GetLoggedUserId();
    }

    public class SqlUserData : IUserManagerData
    {
        private ProjectDbContext _context;
        private IHttpContextAccessor _httpContextAccessor;

        public SqlUserData(ProjectDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public User Get(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public string GetLoggedUserId()
        {
            var id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return id;
        }
    }
}
