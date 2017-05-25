using Model.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Model.Registers
{
    public class Product
    {
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public long? CategoryID { get; set; }
        public long? SupplierId { get; set; }
       
        [DisplayName("Registered Categories")]
        public Category Category { get; set; }
        [DisplayName("Registered Suppliers")]
        public Supplier Supplier { get; set; }
    }
}