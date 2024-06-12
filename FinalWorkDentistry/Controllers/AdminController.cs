using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Domains;
using FinalWorkDentistry.Models;
using FinalWorkDentistry.UsersRoles;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalWorkDentistry.Controllers
{

    [Authorize(Roles = AppRoles.Administrator)]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Doctor> _repositoryDoctors;
        private readonly IRepository<Request> _repositoryRequest;

        public AdminController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, IRepository<Doctor> repositoryDoctors, IRepository<Request> repositoryRequest)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _repositoryDoctors = repositoryDoctors;
            _repositoryRequest = repositoryRequest;
        }



        public IActionResult ListRequest()
        {

          var query = _repositoryRequest.GetList();

         var model = query.OrderBy(x => x.RequestId)
                                   .Select(e => e.Adapt<RequestModel>());

            return View(model);
        }

        public IActionResult DeleteRequest(long id)
        {
            var entity = _repositoryRequest.GetList();
            if (entity != null)
            {
                _repositoryRequest.Delete(id);
            }
            return RedirectToAction("ListRequest", "Admin");
        }

        //удаление врачей

        //public IActionResult DeleteDoctors(long id)
        //{
        //    var entity = _repositoryDoctors.Read(id);
        //    if (entity != null)
        //    {
        //        _repositoryDoctors.Delete(id);
        //    }
        //    return Redirect("/Doctor/ListView");
        //}

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult IndexRole()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if(role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() => View(_userManager.Users.ToList());
        public async  Task<IActionResult> Edit(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);
                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);
                return RedirectToAction("UserList");
            }
            return NotFound();
        }
    }
}
