using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double  OrderTotal { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public string? PaymentStatus { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? Phonenumber { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? Name { get; set; }



    }
}
