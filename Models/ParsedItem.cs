using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Models
{
    public class ParsedItem
    {
        public string NameItem { get; set; }
        public decimal Price { get; set; }
        public string Source { get; set; }
        public string? Link { get; set; }
        public decimal Profit { get; set; }
    }
}
