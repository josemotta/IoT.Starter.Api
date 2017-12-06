using Microsoft.AspNetCore.Mvc;
using RazorPageShop.Core;
using RazorPageShop.Core.DTOs;
using System.Linq;
using System.Web.Http;

namespace RazorPageShop.WebApi
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class CartController : ApiController
    {
        readonly ICheckoutManager checkoutManager;

        public CartController(ICheckoutManager checkoutManager)
        {
            this.checkoutManager = checkoutManager;
        }

        [ResponseCache(NoStore = true)]
        [ValidateAntiForgeryToken]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public CartDTO Post([Microsoft.AspNetCore.Mvc.FromBody]CartItemDTO value)
        {
            var cart = checkoutManager.GetCart();
            var cartItem = cart.CartItems.Where(i => i.SKU == value.SKU).SingleOrDefault();
            if (cartItem != null)
            {
                cartItem.Quantity = value.Quantity;
                var recalculatedCart = checkoutManager.GetCart(cart.CartItems);

                checkoutManager.SaveCart(cartItem);
                return recalculatedCart;
            }
            else
            {
                return cart;
            }
        }
    }
}
