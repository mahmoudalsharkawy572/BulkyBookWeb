using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ApplicationUserRepository(ApplicationDbContext _db) : Repository<ApplicationUser>(_db), IApplicationUserRepository
    {
        public void Update(ApplicationUser appUser)
        {
            _db.ApplicationUsers.Update(appUser);
        }
    }
}
