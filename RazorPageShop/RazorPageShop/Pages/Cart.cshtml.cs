using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageShop.Core.DTOs;
using RazorPageShop.Core;
using Microsoft.AspNetCore.Mvc;

namespace RazorPageShop.Pages
{
    public class CartModel : PageModel
    {
        private readonly ICheckoutManager _checkoutManager;
        public string RequestId { get; set; }

        public CartDTO CartDTO { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public CartModel(ICheckoutManager checkoutManager)
        {
            this._checkoutManager = checkoutManager;
        }

        public void OnGet()
        {
            CartDTO = this._checkoutManager.GetCart();
        }
    }
}
