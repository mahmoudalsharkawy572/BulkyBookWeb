using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork(ApplicationDbContext _db,
                            ICategoryRepository _categoryRepository,
                            IProductRepository _productRepository,
                            ICompanyRepository _companyRepository,
                            IShoppingCartRepository _shoppingCartRepository,
                            IApplicationUserRepository _applicationUserRepository,
                            IOrderDetailRepository _orderDetailRepository,
                            IOrderHeaderRepository _orderHeaderRepository) : IUnitOfWork
    {
        
        public ICategoryRepository Category => _categoryRepository;

        public IProductRepository Product => _productRepository;

        public ICompanyRepository Company => _companyRepository;

        public IShoppingCartRepository ShoppingCart => _shoppingCartRepository;

        public IApplicationUserRepository ApplicationUser => _applicationUserRepository;

        public IOrderDetailRepository OrderDetail => _orderDetailRepository;

        public IOrderHeaderRepository OrderHeader => _orderHeaderRepository;

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
