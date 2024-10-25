using System;
using System.Collections.Generic;
using System.Linq;
using Foody.Models;
using System.Linq.Expressions;
using Foody.DataAccess.Data;
using Foody.DataAccess.Repository.IRepository;
using System.Text;
using System.Threading.Tasks;

namespace Foody.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(Company obj)
        {
            _db.Update(obj);

        }
    }
}
