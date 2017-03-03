using Microsoft.AspNetCore.Mvc;
using ProjectArea.Services;
using ProjectArea.ViewModels;
using ProjectArea.Entities;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectArea.Controllers
{
    public class ProjectController : Controller
    {
        private IUserManagerData _userManager;
        private IProjectManagerData _projectManager;

        public ProjectController(IProjectManagerData projectManager, IUserManagerData userManager)
        {
            _projectManager = projectManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult AddProject()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddProject(ProjectViewModel model)
        {
            var project = new Project();

            project.Name = model.Name;
            project.Description = model.Description;

            var userId = _userManager.GetLoggedUserId();
            project.OwnerId = userId;

            _projectManager.Add(project);
            _projectManager.Commit();

            return RedirectToAction("Project", new { id = project.Id });
        }

        public IActionResult Project(int id)
        {
            var model = new ProjectViewModel();
            var project = _projectManager.Get(id);

            model.Name = project.Name;
            model.Description = project.Description;
            model.Id = id;
            model.Members = _projectManager.GetAllMembers(id);

            return View(model);
        }

        public IActionResult YourProjects(ProjectViewModel model)
        {
            var userId = _userManager.GetLoggedUserId();
            model.Projects = _projectManager.GetForUser(userId);

            return View(model);
        }

        public IActionResult Join(ProjectViewModel model, int id)
        {
            var member = new Member();
            var userId = _userManager.GetLoggedUserId();
            DateTime joinDate = DateTime.Now;

            member.MemberId = userId;
            member.ProjectId = id;
            member.Name = User.Identity.Name;
            member.JoinDate = joinDate;

            _projectManager.AddMember(member);
            _projectManager.Commit();

            return RedirectToAction("Project", new { id = member.ProjectId });
        }
    }
}
