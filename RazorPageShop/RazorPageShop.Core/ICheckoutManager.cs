using RazorPageShop.Core.DTOs;
using System.Collections.Generic;

namespace RazorPageShop.Core
{
    public interface ICheckoutManager
    {
        CartDTO GetCart();
        void SaveCart(CartItemDTO modifiedItem);
        CartDTO GetCart(List<CartItemDTO> cartItems);
        CheckoutSummaryDTO GetCheckoutSummary();
        List<ProductDTO> GetProducts();
        void InitializeDB();
    }
}