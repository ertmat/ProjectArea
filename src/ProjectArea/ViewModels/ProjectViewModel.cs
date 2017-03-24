using ProjectArea.Entities;
using System.Collections.Generic;

namespace ProjectArea.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public int UserRole { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Member> Members { get; set; }
    }
}