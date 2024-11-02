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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(OrderDetail obj)
        {
         _db.Update(obj);
        }
    }

}
