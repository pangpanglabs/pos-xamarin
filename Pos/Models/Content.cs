using System;
using System.Collections.Generic;

namespace Pos.Models
{
    public class Content
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal ListPrice { get; set; }
        public decimal SalePrice { get; set; }
        public IEnumerable<Sku> Skus { get; set; }
    }
}

