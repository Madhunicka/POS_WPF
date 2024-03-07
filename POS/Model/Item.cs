using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    public class Item
    {
        public int Id { get; set; }
        public String ProName { get; set; }
        public int Quantity { get; set; }
        public int PriceQ { get; set; }
        public int Discount { get; set; }
        public string CategoryName { get; internal set; }
    }
}
