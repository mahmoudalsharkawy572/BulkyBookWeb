using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

            public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null , bool tracked = false);
    
            public void Add(T entity);
    
            public void Remove(T entity);
    
            public void RemoveRange(IEnumerable<T> entities);
    }
}
