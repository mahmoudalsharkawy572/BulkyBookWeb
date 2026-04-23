using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    public class CategoryRepository(ApplicationDbContext _db) : Repository<Category>(_db), ICategoryRepository
    {
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
