using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.utility
{
   public  class SD
    {

        public const string Role_Customer = "Customer";
        public const string Role_Company = "Company";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";


        public const string statusPending = "Pending";
        public const string statusApproved = "Approved";
        public const string statusProcessing = "Processing";
        public const string statusShipped = "Shipped";
        public const string statusDelivered = "Delivered";
        public const string statusCancelled = "Cancelled";
        public const string statusRefunded = "Refunded";

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayed = "Delayed";
        public const string PaymentStatusRejected = "Rejected";

        public const string PaymentMethodCash = "Cash";
        public const string PaymentMethodCard = "Card";

    }
}
