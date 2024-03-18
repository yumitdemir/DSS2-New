using Forum.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.UI.Controllers
{
    public class UsersController : Controller
    {
        // GET: UsersContrller
        public ActionResult Index()
        {
            return View(Enumerable.Empty<UserShortViewModel>());
        }

        // GET: UsersContrller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersContrller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersContrller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersContrller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersContrller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersContrller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersContrller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
