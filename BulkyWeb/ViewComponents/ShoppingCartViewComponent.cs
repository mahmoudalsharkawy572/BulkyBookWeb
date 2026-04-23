using Bulky.DataAccess.Repository.IRepository;
using Bulky.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyWeb.ViewComponents {
    public class ShoppingCartViewComponent(IUnitOfWork _unitOfWork) : ViewComponent 
    {
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            int count = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null) 
            {
                var userId = claim.Value;
                count = _unitOfWork.ShoppingCart
                                   .GetAll(u => u.ApplicationUserId == userId)
                                   .Sum(u => u.Count);
            }

            HttpContext.Session.SetInt32(SD.SessionCart, count);
            return View(count);
        }

    }
}
