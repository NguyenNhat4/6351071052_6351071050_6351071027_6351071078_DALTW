using Foody.DataAccess.Data;
using Foody.DataAccess.Repository.IRepository;
using Foody.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Foody.DataAccess.Repository
{

    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
    
        public void Remove(Category obj)
        {
            base.Remove(obj);
        }

        public void Update(Category obj)
        {            _db.Categories.Update(obj);

        }
    }
}
