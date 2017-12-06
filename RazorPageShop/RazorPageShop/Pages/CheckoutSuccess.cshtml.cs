using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageShop.Core.DTOs;
using RazorPageShop.Core;

namespace RazorPageShop.Pages
{
    public class CheckoutSuccessModel : PageModel
    {
        private readonly ICheckoutManager _checkoutManager;

        public CheckoutSummaryDTO CheckoutSummaryDTO { get; set; }

        public CheckoutSuccessModel(ICheckoutManager checkoutManager)
        {
            this._checkoutManager = checkoutManager;
            this.CheckoutSummaryDTO = checkoutManager.GetCheckoutSummary();
        }
    }
}