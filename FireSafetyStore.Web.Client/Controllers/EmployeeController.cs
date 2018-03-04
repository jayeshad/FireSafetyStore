using FireSafetyStore.Web.Client.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FireSafetyStore.Web.Client.Infrastructure.Security;
using FireSafetyStore.Web.Client.Infrastructure.Common;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;

namespace IdentitySample.Controllers
{

    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        public EmployeeController()
        {
        }

        public EmployeeController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Users/
        //public async Task<ActionResult> Index()
        //{
        //    return View(await UserManager.Users.ToListAsync());
        //}


        public async Task<ActionResult> Index()
        {
            var employeeRole = await RoleManager.Roles.FirstOrDefaultAsync(x => x.Name == FireSafetyAppConstants.EmployeeRoleName);
            return View(await UserManager.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(employeeRole.Id)).ToListAsync());
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            var vm = PopulateEmployeeRole();
           return View(vm);
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel)
        {
            var vm = PopulateEmployeeRole();
            if (ModelState.IsValid)
            {
                var user = MapUserModel(userViewModel);
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);
                var selectedRole = RoleManager.Roles.Where(x => x.Name == FireSafetyAppConstants.EmployeeRoleName).Select(x => x.Name).ToArray();
                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRole != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRole);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());                    
                    return View(vm);
                }
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        private ApplicationUser MapUserModel(RegisterViewModel vm)
        {
            return new ApplicationUser
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Address = vm.Address,
                City = vm.City,
                State =vm.State,
                PostalCode = vm.PostalCode,
                UserName = vm.Email,
                Email = vm.Email,
                PhoneNumber = vm.PhoneNumber
            };
        }

        private RegisterViewModel PopulateEmployeeRole()
        {
            return new RegisterViewModel
            {
                RolesList = RoleManager.Roles.Where(x => x.Name == FireSafetyAppConstants.EmployeeRoleName).ToList().Select(x => new SelectListItem()
                {
                    Selected = true,
                    Text = x.Name,
                    Value = x.Name
                }),
                GenderItems = PopulateGenderItems()
            };
        }

        private IEnumerable<SelectListItem> PopulateGenderItems()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "Male", Text = "Male", Selected = true });
            list.Add(new SelectListItem { Value = "Female", Text = "Female" });
            return list;
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                PhoneNumber = user.PhoneNumber,
                RolesList = RoleManager.Roles.Where(x=>x.Name == FireSafetyAppConstants.EmployeeRoleName).ToList().Select(x => new SelectListItem()
                {
                    Selected = true,
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FirstName,LastName,Address,City,State,PostalCode,Email,Id,RolesList")] EditUserViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.Email;
                user.Email = editUser.Email;
                user.FirstName = editUser.FirstName;
                user.LastName = editUser.LastName;
                user.Gender = editUser.Gender;
                user.DateOfBirth = editUser.DateOfBirth;
                user.Address = editUser.Address;
                user.City = editUser.City;
                user.State = editUser.State;
                user.PostalCode = editUser.PostalCode;
                user.PhoneNumber = editUser.PhoneNumber;
                var userRoles = await UserManager.GetRolesAsync(user.Id);
                var selectedRole = userRoles.ToArray();
                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
