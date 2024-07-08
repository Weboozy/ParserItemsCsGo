using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Models
{
    public  class PresentedItem
    {
        public string NameItem { get; set; }
        public List<string> ListSources { get; set; } = new List<string>();
        public List<decimal> ListPrices { get; set; } = new List<decimal>();
        public decimal Profit { get; set; }
    }
}
