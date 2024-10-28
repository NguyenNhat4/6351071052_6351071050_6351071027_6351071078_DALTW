using Foody.DataAccess.Data;
using Foody.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICompanyRepository Company { get; private set; }
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }

        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db) 
        {

            _db = db;
            Company = new CompanyRepository(db);
            Category = new CategoryRepository(db);
            Product = new ProductRepository(db);
            ShoppingCart = new ShoppingCartRepository(db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
