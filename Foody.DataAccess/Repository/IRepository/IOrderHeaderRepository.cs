using Foody.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository: IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);   
        void UpdateStatus(int orderHeaderId, string orderStatus, string? paymentStatus = null);
        void UpdateStripePaymentID(int id ,string  sessionId, string paymentIntentId);
    }
}
