using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniStore.Models
{

    public partial class ShoppingCart
    {
        CartDBContext storeDB = new CartDBContext();

        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public string CartId { get; set; }




        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }



        
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product product)
        {
            
            var cartItem = storeDB.Carts.SingleOrDefault(c => c.CartId == ShoppingCartId&& c.ProductId==product.ProductID);

            if (cartItem == null)
            {
      
                cartItem = new Cart
                {
                    CartId = ShoppingCartId,
                    ProductId=product.ProductID,
                    Quantity=1,
                };

                storeDB.Carts.Add(cartItem);
            }
            else
            {
    
                cartItem.Quantity++;
            }
          
            storeDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {

            var cartItem = storeDB.Carts.SingleOrDefault(c => c.CartId == ShoppingCartId && c.CartProductId==id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    itemCount = cartItem.Quantity;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);
                }
          
                storeDB.SaveChanges();
            }
            return itemCount;
        }


        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }
           
            storeDB.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }

        public decimal GetTotal()
        {

            int total = (from Carts in storeDB.Carts
                         where Carts.CartId == CartId
                         select (int)Carts.Quantity * Carts.Product.Price).Sum();

            return total;
        }

        
        
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        

    }

}