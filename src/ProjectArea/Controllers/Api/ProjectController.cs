using Microsoft.AspNetCore.Mvc;
using ProjectArea.Entities;
using ProjectArea.Services;
using ProjectArea.ViewModels;
using System;

namespace ProjectArea.Controllers.Api
{
    [Route("Project/api")]
    public class ProjectController : Controller
    {
        private IUserManagerData _userManager;
        private IProjectManagerData _projectManager;

        public ProjectController(IProjectManagerData projectManager, IUserManagerData userManager)
        {
            _projectManager = projectManager;
            _userManager = userManager;
        }

        [HttpPost("addproject")]
        public IActionResult AddProject(ProjectViewModel model)
        {
            var project = new Project();

            project.Name = model.Name;
            project.Description = model.Description;

            var userId = _userManager.GetLoggedUserId();
            project.OwnerId = userId;

            _projectManager.Add(project);
            _projectManager.Commit();

            var results = project;
            return Ok(results);
        }

        [HttpGet("project/{id}")]
        public IActionResult Project(int id)
        {
            var model = new ProjectViewModel();
            var project = _projectManager.Get(id);

            model.Name = project.Name;
            model.Description = project.Description;
            model.Id = id;
            model.Members = _projectManager.GetAllMembers(id);

            return Ok(model);
        }

        [HttpGet("projects")]
        public IActionResult YourProjects(ProjectViewModel model)
        {
            var userId = _userManager.GetLoggedUserId();
            model.Projects = _projectManager.GetForUser(userId);

            return Ok(model);
        }

        [HttpPost("join")]
        public IActionResult Join(ProjectViewModel model, int id)
        {
            var member = new Member();
            var userId = _userManager.GetLoggedUserId();
            DateTime joinDate = DateTime.Now;

            member.MemberId = userId;
            member.ProjectId = id;
            member.Name = User.Identity.Name;
            member.JoinDate = joinDate;

            member.Role = 1;

            _projectManager.AddMember(member);
            _projectManager.Commit();

            return Ok();
        }
    }
}
