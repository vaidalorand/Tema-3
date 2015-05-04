using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniStore.Models
{
    public class CartView
    {
        public IEnumerable<Cart> CartItems { get; set; }
        public int CartTotal { get; set; }
    }

    public class CartRemoveView
    {
        public int ItemId { get; set; }
        public int ItemCount { get; set; }
        public decimal CartTotal { get; set; }
    }
}