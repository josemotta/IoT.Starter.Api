using RazorPageShop.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPageShop.Core
{
    public class DiscountManager
    {
        public List<DiscountRuleDTO> Discounts { get; private set; }

        static DiscountManager instance;
        public static DiscountManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new DiscountManager();
                return instance;
            }
        }

        public DiscountManager()
        {
            Discounts = new List<DiscountRuleDTO>();
            Discounts.Add(new DiscountRuleDTO(0, 499.99M, 0));
            Discounts.Add(new DiscountRuleDTO(500M, 599.99M, .05M));
            Discounts.Add(new DiscountRuleDTO(600M, 699.99M, .10M));
            Discounts.Add(new DiscountRuleDTO(700M, decimal.MaxValue, .15M));
        }

        public DiscountRuleDTO GetDiscount(decimal subtotal)
        {
            return Discounts.Where(d => d.CheckRange(subtotal)).FirstOrDefault();
        }
    }
}
