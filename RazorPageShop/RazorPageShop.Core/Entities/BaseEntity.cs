using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RazorPageShop.Core.Entities
{
    [DataContract]
    public class BaseEntity
    {
        [DataMember]
        public int Id { get; protected set; }
    }
}
