using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RazorPageShop.Core.DTOs
{
    [DataContract]
    public class CartItemDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SKU { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string SmallImagePath { get; set; }
        [DataMember]
        public string LargeImagePath { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public decimal Subtotal
        {
            get
            {
                return Quantity * Price;
            }
        }
    }
}