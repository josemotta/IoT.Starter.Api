using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RazorPageShop.Core.DTOs
{
    public class DiscountRuleDTO
    {
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal Rate { get; set; }
        public decimal CalculatedDiscount { get; set; }

        public DiscountRuleDTO(decimal minValue, decimal maxValue, decimal rate)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Rate = rate;
        }

        public bool CheckRange(decimal subtotal)
        {
            var result = subtotal >= MinValue && subtotal <= MaxValue;
            if (result)
                CalculatedDiscount = Math.Round(subtotal * Rate, 2);
            return result;
        }
    }
}