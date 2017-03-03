using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using ProjectArea.Entities;
using ProjectArea.Services;
using ProjectArea.ViewModels;

namespace ProjectArea.Controllers
{
    public class HomeController : Controller
    {
        private IUserManagerData _userManager;
        private IProjectManagerData _projectManager;

        public HomeController(IProjectManagerData projectManager, IUserManagerData userManager)
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

            return View(model);
        }

        public IActionResult YourProjects(ProjectViewModel model)
        {
            var userId = _userManager.GetLoggedUserId();
            model.Projects = _projectManager.GetForUser(userId);

            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
