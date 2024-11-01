using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Models.ViewModels
{
    public class ShoppingCartVM
    {

        public IEnumerable<ShoppingCart> ShippingCartList { get; set; }
        public double OrderTotal { get; set; }
    }
}
