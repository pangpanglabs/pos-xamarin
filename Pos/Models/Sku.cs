using System;

namespace Pos.Models
{
    public class Sku
    {
		public long Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public decimal ListPrice { get; set; }
		public decimal SalePrice { get; set; }
    }
}

