using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V123.Debugger;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SeleniumExtras.WaitHelpers;
using System.Windows.Media.Converters;
using System.Threading;

namespace Analyzer.Models
{
    public class Searcher
    {
        private ParsedItem? handledItem { get; set; }

        
        public ParsedItem SearchItems(string url, string nameItem, IWebDriver driver,bool isFirstLaunch) {
            switch (url) {
                case "https://market.csgo.com/en/":
                    try
                    {
                        SearchItemOnMarketCsGo(driver, url, nameItem, isFirstLaunch);
                    }
                    catch (NoSuchElementException)
                    {
                        handledItem = new ParsedItem() {
                            NameItem = nameItem,
                            Price = 0,
                            Source = "MarketCsGo",
                            Link = null,
                            Profit = 0
                        };
                    }
                    break;
                case "https://shadowpay.com/csgo-items":
                    try
                    {
                        SearchItemOnShadowpay(driver, url, nameItem, isFirstLaunch);

                    }
                    catch (NoSuchElementException)
                    {
                        handledItem = new ParsedItem()
                        {
                            NameItem = nameItem,
                            Price = 0,
                            Source = "Shadowpay",
                            Link = null,
                            Profit = 0
                        };
                    }
                    catch (TimeoutException) {
                        handledItem = null;
                    }
                    break;
                case "https://dmarket.com/ingame-items/item-list/csgo-skins":
                    SearchItemOnDmarket(driver, url, nameItem, isFirstLaunch);
                    break;
            }
            return handledItem;
        }
        private void SearchItemOnMarketCsGo(IWebDriver driver,string url,string nameItem,bool isFirstLaunch) {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            if (isFirstLaunch) {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                IWebElement sortPanel = driver.FindElement(By.TagName("app-sort-order"));
                sortPanel.Click();
                Thread.Sleep(1000);
                IWebElement sortParameter = driver.FindElement(By.ClassName("mat-mdc-menu-content"));
                sortParameter = wait.Until((driver) =>
                {
                    try
                    {
                        IWebElement sortItem = sortParameter.FindElement(By.XPath("//button[4]"));
                        return sortItem;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });
                sortParameter.Click();
            }

            Thread.Sleep(1000);
            IWebElement input = driver.FindElement(By.XPath("//div[@class='input-container']//input[@id='undefined']"));
            input.SendKeys(nameItem);
            IWebElement btnSearch = driver.FindElement(By.ClassName("input-button"));
            Thread.Sleep(1000);
            btnSearch.Click();

            Thread.Sleep(2000);
            IEnumerable<IWebElement> notFoundElements = driver.FindElements(By.XPath("//app-virtual-pagination/*"));
            if (notFoundElements.Count() !=1)
            {
                ClearFilterCsMarket(driver);
                throw new NoSuchElementException();
            }
            IWebElement item = wait.Until((driver) =>
            {
                IWebElement item;
                try
                {
                    item = driver.FindElement(By.XPath("//div[@class='cdk-virtual-scroll-content-wrapper']/a"));
                    if (!string.IsNullOrWhiteSpace(item.GetAttribute("innerText")))
                    {
                        return item;
                    }
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });
            handledItem = PrimaryDataHandler.HandlePrimaryDataFromMarketCsGo(item, nameItem);
            ClearFilterCsMarket(driver);
            

        }
        private void SearchItemOnShadowpay(IWebDriver driver, string url, string nameItem,bool isFirstLaunch) {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            if (isFirstLaunch)
            {
                driver.Navigate().GoToUrl(url);
                driver.Navigate().Refresh();
                Thread.Sleep(2000);
                IWebElement dialogWindow = driver.FindElement(By.Id("onesignal-slidedown-cancel-button"));
                dialogWindow.Click(); //close dialog window
            }
            IWebElement input = driver.FindElement(By.XPath("//input[@id='searchInput']"));
            Thread.Sleep(2000);
            input.SendKeys(nameItem);
            
            IWebElement refreshBtn = driver.FindElement(By.ClassName("refresh-button"));
            Thread.Sleep(1000);
            refreshBtn.Click();
            Thread.Sleep(2000);
            IEnumerable<IWebElement> notFoundElements = driver.FindElements(By.XPath("//div[@class='list-wrap']/div"));
            if (notFoundElements.Count() !=1 )
            {
                ClearFilterShadowpay(driver);
                throw new NoSuchElementException();
            }
            if (isFirstLaunch) {
                Thread.Sleep(2000);
                IWebElement currensyPanel = wait.Until((driver) =>
                {
                    try
                    {
                        IWebElement item = driver.FindElement(By.XPath("//div[@class='header-panel']//div[2]"));
                        return item;
                    }
                    catch (ElementClickInterceptedException)
                    {
                        return null;
                    }
                });
                currensyPanel.Click();
                currensyPanel = wait.Until((driver) =>
                {
                    try
                    {
                        IWebElement item = driver.FindElement(By.XPath("//div[@class='language-currency-settings__content']/div[2]/div"));
                        return item;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }

                });
                currensyPanel.Click();
                IWebElement selectedCurrensy = wait.Until((driver) =>
                {
                    try
                    {
                        IWebElement item = driver.FindElement(By.XPath("//ul[@class='dropdown__menu']/li[1]"));
                        return item;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });
                selectedCurrensy.Click();
                IWebElement closeCurrensyPanel = driver.FindElement(By.XPath("//div[@class='header-panel']//div[2]"));
                closeCurrensyPanel.Click();
                Thread.Sleep(1000);
                IWebElement sortPanel = driver.FindElement(By.XPath("//div[@class='marketplace-block__sort-selected-item']"));
                sortPanel.Click();
                IWebElement sortParameter = wait.Until((driver) =>
                {
                    try
                    {
                        IWebElement item = driver.FindElement(By.XPath("//ul[@class='dropdown__menu']//li[@class='dropdown__outer-item'][1]"));
                        return item;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });
                sortParameter.Click();
            } //I moved it from the top down to wait for the message blocking access to the currency selection panel to disappear
            IWebElement item = wait.Until((driver) => {
                try
                {
                    IWebElement item = driver.FindElement(By.XPath("//div[@class='marketplace-inventory__main-list']/div[@class='item-buy-card']/a[1]"));
                    return item;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }

            });

            handledItem = PrimaryDataHandler.HandlePrimaryDataFromShadowpay(item, nameItem);
            ClearFilterShadowpay(driver);
        }
        private void SearchItemOnDmarket(IWebDriver driver, string url, string nameItem, bool isFirstLaunch) {
            if (isFirstLaunch)
            {
                driver.Navigate().GoToUrl(url);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                IWebElement sortPanel = driver.FindElement(By.TagName("sort-items"));
                sortPanel.Click();
                IWebElement sortParameter = wait.Until((driver) =>
                {
                    try
                    {
                        IWebElement item = driver.FindElement(By.XPath("//div[@class='cdk-overlay-pane']/div/div//button[6]"));
                        return item;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });
                sortParameter.Click();
                IWebElement dialogWindow = driver.FindElement(By.XPath("//div[@class='cdk-overlay-pane']//div//div/mat-icon"));
                dialogWindow.Click();
            }
            
            IWebElement input = driver.FindElement(By.ClassName("o-filter__searchInput"));
            input.SendKeys(nameItem);
            //automatic search
            Thread.Sleep(3000); // wait for load all items
            
            IEnumerable<IWebElement> noFoundItem = driver.FindElements(By.XPath("//market-inventory/*"));
            if (noFoundItem.Count() !=3)
            {
                throw new NoSuchElementException();
            }
            IWebElement item = driver.FindElement(By.XPath("//div[@class='c-assets__container']/asset-card[1]"));
            handledItem = PrimaryDataHandler.HandlePrimaryDataFromDmarket(item, nameItem);
            input.Click();
            IWebElement btnClearFilter = driver.FindElement(By.ClassName("o-filter__searchClear"));
            Thread.Sleep(2000);
            btnClearFilter.Click();
        }



        private void ClearFilterShadowpay(IWebDriver driver) {
            IWebElement btnClearFilter = driver.FindElement(By.ClassName("search-field__clear"));
            btnClearFilter.Click();
        }
        private void ClearFilterCsMarket(IWebDriver driver) {
            IWebElement btnClearFilter = driver.FindElement(By.ClassName("transparent-button"));
            btnClearFilter.Click();
        }
       
    }
}
