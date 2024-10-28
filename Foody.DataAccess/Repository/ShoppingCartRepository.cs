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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;   

        }

        public void Update(ShoppingCart obj)
        {
            
                _db.ShoppingCarts.Update(obj);
            
        }
    }
    

    }

