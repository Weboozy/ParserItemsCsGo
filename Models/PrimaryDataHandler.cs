using Analyzer.Models;
using AngleSharp.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V124.CSS;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Analyzer
{
    public static class PrimaryDataHandler
    {
        public static ParsedItem HandlePrimaryDataFromMarketCsGo(IWebElement data,string nameItem) {
            string pattern = @"\${1}\d+.?\d*";
            string item = data.GetAttribute("innerText");
            //item = item.Substring(item.IndexOf('$'), item.Length - item.IndexOf('$'));
            //string quality = item.Substring(0, 2);
            Match match = Regex.Match(item, pattern);
            ParsedItem itemCard = new ParsedItem()
            {
                Price = Decimal.Parse(match.Value.Replace('.', ',').TrimStart('$')),
                NameItem = nameItem,
                Source = "MarketCsGo",
                Link = data.GetAttribute("href")
            };

            return itemCard;
        }
        public static ParsedItem HandlePrimaryDataFromShadowpay(IWebElement data,string nameItem) {
            string item = data.GetAttribute("innerText");
            string pattern = @"\d+.?\d*";
            Match selectPriceMatch = Regex.Match(item, pattern);
            ParsedItem itemCard = new ParsedItem();
            try
            {
                itemCard.Price = Decimal.Parse(selectPriceMatch.Value.Replace('.', ','));
                itemCard.NameItem = nameItem;
                itemCard.Source = "Shadowpay";
                itemCard.Link = data.GetAttribute("href");
            }
            catch (Exception)
            {
                itemCard.Price = 0.0m;
                itemCard.NameItem = nameItem;
                itemCard.Source = "Shadowpay";
                itemCard.Link = data.GetAttribute("href");
            }
            return itemCard;

        }
        public static ParsedItem HandlePrimaryDataFromDmarket(IWebElement data, string nameItem) {
            string item = data.GetAttribute("innerText");
            string pattern = @"\d+.?\d*";
            string datas = data.GetAttribute("innerText");
            Match selectPriceMatch = Regex.Match(item, pattern);
            ParsedItem itemCard = new ParsedItem();
            try
            {
                itemCard.Price = Decimal.Parse(selectPriceMatch.Value.Replace('.', ','));
                itemCard.NameItem = nameItem;
                itemCard.Source = "Dmarket";
                itemCard.Link = data.GetAttribute("href");
            }
            catch (Exception)
            {

                throw;
            }
            return itemCard;
        }
    }
}
