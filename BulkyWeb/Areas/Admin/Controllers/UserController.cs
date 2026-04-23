using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area(SD.Role_Admin)]
    [Authorize]
    public class UserController(IUnitOfWork _unitOfWork,
                                RoleManager<IdentityRole> _roleManager,
                                UserManager<IdentityUser> _userManager) : Controller
    {
        public IActionResult Index()
        {
            var users = _unitOfWork.ApplicationUser.GetAll().ToList();
            foreach(var user in users) 
            {
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
                if(user.CompanyId != null && user.CompanyId != 0)
                {
                    user.Company = _unitOfWork.Company.Get(c => c.Id == user.CompanyId);
                }
            }
            return View(users);
        }

       [HttpPost]
        public IActionResult LockUnlock(string id)
        {
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                // User is currently locked, so we unlock them
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                // User is currently unlocked, so we lock them for 100 years
                user.LockoutEnd = DateTime.Now.AddYears(100);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RoleManagment(string id)
        {
            var RoleManagmentVM = new RoleManagmentVM()
            {
                ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == id),
                RoleList = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                }),
                CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            RoleManagmentVM.ApplicationUser.Role = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == id))
                    .GetAwaiter().GetResult().FirstOrDefault();

            return View(RoleManagmentVM);

        }

        [HttpPost]
        public IActionResult RoleManagment(RoleManagmentVM roleManagmentVM)
        {
            var userFromDb = _unitOfWork.ApplicationUser.Get(u => u.Id == roleManagmentVM.ApplicationUser.Id);
            if (userFromDb != null)
            {
                userFromDb.Name = roleManagmentVM.ApplicationUser.Name;
                userFromDb.CompanyId = roleManagmentVM.ApplicationUser.CompanyId;
                var userRole = _userManager.GetRolesAsync(userFromDb).GetAwaiter().GetResult().FirstOrDefault();
                if (userRole != null)
                {
                    _userManager.RemoveFromRoleAsync(userFromDb, userRole).GetAwaiter().GetResult();
                }
                _userManager.AddToRoleAsync(userFromDb, _roleManager.FindByIdAsync(roleManagmentVM.ApplicationUser.Role).GetAwaiter().GetResult().Name).GetAwaiter().GetResult();
                _unitOfWork.Save();
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
