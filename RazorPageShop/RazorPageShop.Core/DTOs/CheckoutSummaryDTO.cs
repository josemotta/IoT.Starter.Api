using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPageShop.Core.DTOs
{
    public class CheckoutSummaryDTO
    {
        public string OrderNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Total { get; set; }
        public int DeliveryUpToNWorkingDays { get; set; }

        public string DeliveryUpTo
        {
            get
            {
                return string.Format("{0} {1}", DeliveryUpToNWorkingDays, 
                    DeliveryUpToNWorkingDays == 1 ? " working day" : " working days");
            }
        }

        public CustomerInfoDTO CustomerInfo { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
    }
}
