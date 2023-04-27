using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBugTracker.Data;
using PBugTracker.Models;
using System.Dynamic;

namespace PBugTracker.Controllers
{
    [Authorize]
    public class BugController : Controller
    {
        private BugTrackerDbContext _context;
        public BugController(BugTrackerDbContext context)
        {
            _context = context;
        }

        #region List

        public IActionResult Index()
        {
            List<Bug> bugs = _context.Bug.Where(o => o.IsDeleted == false).ToList();

            return View(bugs);
        }

        public IActionResult Detail(int id)
        {

            Bug bugs = _context.Bug.First(item => item.Id == id && item.IsDeleted == false);
            List<Comment> comments = _context.Comment.Where(o => o.IsDeleted == false && o.BugID == id).ToList();

            dynamic dy = new ExpandoObject();
            dy.bugs = bugs;
            dy.comments = comments;

            return View(dy);
        }

        #endregion

        #region Add

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Bug bugs)
        {
            _context.Bug.Add(bugs);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion

        #region Update

        [HttpGet]
        public IActionResult Update(int id)
        {
            var bugs = _context.Bug.First(item => item.Id == id);

            return View(bugs);
        }

        [HttpPost]
        public IActionResult Update(Bug bugs)
        {
            _context.Bug.Update(bugs);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion

        #region Comment

        [HttpGet]
        public IActionResult Comment(int id)
        {
            var bugs = _context.Bug.First(item => item.Id == id);

            return View(bugs);
        }

        [HttpPost]
        public IActionResult Comment(Bug bugs,IFormCollection form)
        {

            Comment comment = new Comment
            {
                BugID = bugs.Id,
                Commentor = User.Identity.Name,
                CommentDescription = form["commentInput"]

            };

            _context.Comment.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var bugs = _context.Bug.First(x => x.Id == id);

            bugs.IsDeleted = true;
            _context.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}
