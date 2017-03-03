using ProjectArea.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ProjectArea.Services
{
    public interface IProjectManagerData
    {
        Project Add(Project newProject);
        Project Get(int id);
        IEnumerable<Project> GetAll();
        IEnumerable<Project> GetForUser(string userId);
        void Commit();
    }

    public class SqlProjectData : IProjectManagerData
    {
        private ProjectDbContext _context;

        public SqlProjectData(ProjectDbContext context)
        {
            _context = context;
        }

        public Project Add(Project newProject)
        {
            _context.Projects.Add(newProject);
            return newProject;
        }

        public Project Get(int id)
        {
            return _context.Projects.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects;
        }

        public IEnumerable<Project> GetForUser(string userId)
        {
            return _context.Projects.Where(i => i.OwnerId == userId);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}