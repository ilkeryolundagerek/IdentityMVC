using IdentityMVC.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityMVC.Controllers
{
    public class RolesController : Controller
    {
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;
        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager=roleManager;
            _userManager=userManager;
        }

        // GET: RolesController
        public ActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        // GET: RolesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RolesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Name, string Detail)
        {
            try
            {
                AppRole role = Activator.CreateInstance<AppRole>();
                role.Detail = Detail;
                role.Name = Name;
                _roleManager.CreateAsync(role).Wait();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddRoleToUser()
        {
            var model = new Tuple<IQueryable<AppUser>, IQueryable<AppRole>>(_userManager.Users, _roleManager.Roles);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddRoleToUser(string selected_userid, string selected_role)
        {
            if (!string.IsNullOrEmpty(selected_role))
            {
                var selected_user = _userManager.Users.FirstOrDefault(x => x.Id.Equals(selected_userid));
                if (selected_user!=null)
                {
                    _userManager.AddToRoleAsync(selected_user, selected_role).Wait();
                }
            }
            var model = new Tuple<IQueryable<AppUser>, IQueryable<AppRole>>(_userManager.Users, _roleManager.Roles);
            return View(model);
        }

        // GET: RolesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RolesController/Edit/5
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

        // GET: RolesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RolesController/Delete/5
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
