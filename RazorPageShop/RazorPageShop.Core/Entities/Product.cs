using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RazorPageShop.Core.Entities
{
    public class Product : BaseEntity
    {
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
    }
}