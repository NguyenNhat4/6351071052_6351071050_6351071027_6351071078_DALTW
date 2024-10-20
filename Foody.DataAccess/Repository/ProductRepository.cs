using Foody.DataAccess.Data;
using Foody.DataAccess.Repository.IRepository;
using Foody.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        } 



        public void Update(Product obj)
        {
           var objFromDb = _db.Products.FirstOrDefault(s => s.Id == obj.Id);
            if (objFromDb != null)
            {
                if (obj.imageUrl != null)
                {
                    objFromDb.imageUrl = obj.imageUrl;
                }
                objFromDb.Name = obj.Name;
                objFromDb.Price = obj.Price;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Description = obj.Description;
            }
        }
    }
}

