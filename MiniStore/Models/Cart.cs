using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MiniStore.Models
{
    public class Cart
    {
        public int CartProductId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }

    public class CartDBContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
    }

}