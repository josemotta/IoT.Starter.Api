using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageShop.Core;
using RazorPageShop.Core.DTOs;

namespace RazorPageShop.Pages
{
    public class IndexModel : PageModel
    {
        public IList<ProductDTO> Products { get; set; }
        readonly ICheckoutManager _checkoutManager;

        public IndexModel(ICheckoutManager checkoutManager)
        {
            this._checkoutManager = checkoutManager;
        }

        public void OnGet()
        {
            Products = this._checkoutManager.GetProducts();
        }

        public async Task<IActionResult> OnPostAddAsync(string SKU)
        {
            this._checkoutManager.SaveCart(new CartItemDTO
            {
                SKU = SKU,
                Quantity = 1
            });

            return RedirectToPage("Cart");
        }
    }
}
