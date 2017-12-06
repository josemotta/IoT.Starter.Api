using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RazorPageShop.Core.Entities
{
    public class CartItem : BaseEntity
    {
        [DataMember]
        [Required]
        public virtual Product Product { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }
}