using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBugTracker.Models;

namespace PBugTracker.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private BugTrackerDbContext _context;
        public ProjectController(BugTrackerDbContext context)
        {
            _context = context;
        }

        #region List

        public IActionResult Index()
        {
            List<Project> projects = _context.Project.Where(o => o.IsDeleted == false).ToList();

            return View(projects);
        }

        public IActionResult Detail(int id)
        {

            Project projects = _context.Project.First(item => item.Id == id && item.IsDeleted == false);

            return View(projects);
        }

        #endregion

        #region Add

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Project projects)
        {
            _context.Project.Add(projects);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion

        #region Update

        [HttpGet]
        public IActionResult Update(int id)
        {
            var projects = _context.Project.First(item => item.Id == id);

            return View(projects);
        }

        [HttpPost]
        public IActionResult Update(Project projects)
        {
            _context.Project.Update(projects);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var projects = _context.Project.First(x => x.Id == id);

            projects.IsDeleted = true;
            _context.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}
