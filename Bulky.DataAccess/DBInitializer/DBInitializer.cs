using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.DBInitializer
{
    public class DBInitializer(RoleManager<IdentityRole> _roleManager
                            , UserManager<IdentityUser> _userManager
                            , ApplicationDbContext _dbContext) : IDBInitializer
    {
        public void Initialize()
        {
            #region Migrations if they are not applied
            try
            {
                if(_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch(Exception ex) 
            {
                //
            }
            #endregion

            #region Create Roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

                #region Create Admin User if it is not created
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "Admin Test",
                    Email = "admin@ecommerce.com",
                    Name = "Mahmoud Alsharkawy",
                    PhoneNumber = "1112223333",
                    StreetAddress = "test 123 Ave",
                    State = "IL",
                    PostalCode = "23422",
                    City = "Cairo"
                }, "Admin123*").GetAwaiter().GetResult();


                ApplicationUser user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@ecommerce.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                #endregion
            }

            #endregion


        }
    }
}
