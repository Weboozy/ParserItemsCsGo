using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer.Models.Urls;

namespace Analyzer.Models
{
    public class Parser
    {
        public void ParseSkinsFromCsGoMarket(IWebDriver webDriver, string item, bool isFirstLoad)
        {
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            WebDriverWait explicitWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(2));
            if (isFirstLoad)
            {
                webDriver.Navigate().GoToUrl(UrlSiteParsing.CsGoMarket);
                ExecuteSortFilter(webDriver, explicitWait, "//app-filter-above-inventory//app-sort-order//button");
                ExecuteSortFilterParameter(webDriver, explicitWait, "//div[@class='cdk-overlay-container']//div[@class='cdk-overlay-pane']//button[4]");
            }
            ExecuteSearchFilter(webDriver, explicitWait, "//app-filter-above-inventory//app-search//form//app-input//input", item);
            ExecuteButtonSearchFilter(webDriver, explicitWait, "//app-filter-above-inventory//app-search//app-input//div[@class='actions']/div[2]");
            Thread.Sleep(1000); // we are waiting for the content to load
            bool isFound = ExecuteSearchNotFoundPage(webDriver, explicitWait, "//mat-sidenav-container//mat-sidenav-content//app-virtual-pagination//cdk-virtual-scroll-viewport//div/*", 0);
            if (isFound)
            {
                ExecuteClearFilter(webDriver, explicitWait, "//app-filter-above-inventory//app-tags-search");
                return;
            }
            IWebElement parseElement = SelectElement(webDriver, explicitWait, "//mat-sidenav-container//mat-sidenav-content//app-virtual-pagination//cdk-virtual-scroll-viewport//div/a");
            string data = parseElement.GetAttribute("innerText");
            ExecuteClearFilter(webDriver, explicitWait, "//app-filter-above-inventory//app-tags-search");
        }
        public void ParseSkinsFromShadowpay(IWebDriver webDriver, string item, bool isFirstLoad)
        {
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            WebDriverWait explicitWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(2));
            if (isFirstLoad)
            {
                webDriver.Navigate().GoToUrl(UrlSiteParsing.ShadowPay);
                ExecuteSortFilter(webDriver, explicitWait, "//main[@class='main-block__container']//div[@class='market__breadcrumbs-search-block']//div[@class='marketplace-inventory-configs__block']//div[@class='vue-portal-target']//div[@class='dropdown']");
                ExecuteSortFilterParameter(webDriver, explicitWait, "//main[@class='main-block__container']//div[@class='market__breadcrumbs-search-block']//div[@class='marketplace-inventory-configs__block']//div[@class='vue-portal-target']//div[@class='dropdown']//ul[@class='dropdown__menu']//li[1]");
            }
            ExecuteSearchFilter(webDriver, explicitWait, "//main[@class='main-block__container']//div[@class='market__breadcrumbs-search-block']//div[@class='marketplace-inventory-configs__block']//div[@class='marketplace-inventory-search']//input[@class='search-field__search-input']", item);
            ExecuteButtonSearchFilter(webDriver, explicitWait, "//main[@class='main-block__container']//div[@class='market__breadcrumbs-search-block']//div[@class='marketplace-inventory-configs__block']//div[@class='vue-portal-target']/button");
            Thread.Sleep(1000);// We are waiting for the elements to load
            bool isFound = ExecuteSearchNotFoundPage(webDriver, explicitWait, "//div[@class='content']//div[@class='marketplace-inventory__main-list']/div", 0);
            if (isFound)
            {
                ExecuteClearFilter(webDriver, explicitWait, "//main[@class='main-block__container']//div[@class='market__breadcrumbs-search-block']//div[@class='marketplace-inventory-configs__block']//div[@class='marketplace-inventory-search']//img[@class='search-field__clear']");
                return;
            }
            IWebElement parseElement = SelectElement(webDriver, explicitWait, "//div[@class='content']//div[@class='marketplace-inventory__main-list']/div");
            string text = parseElement.GetAttribute("innerText");
            ExecuteClearFilter(webDriver, explicitWait, "//main[@class='main-block__container']//div[@class='market__breadcrumbs-search-block']//div[@class='marketplace-inventory-configs__block']//div[@class='marketplace-inventory-search']//img[@class='search-field__clear']");
        }
        public void ParseSkinsFromDmarket(IWebDriver webDriver, string item, bool isFirstLoad)
        {
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10); //5
            WebDriverWait explicitWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(2));
            if (isFirstLoad)
            {
                webDriver.Navigate().GoToUrl(UrlSiteParsing.Dmarket);
                ExecuteSortFilter(webDriver, explicitWait, "//div[@class='c-exchange__container']//filters[@class='c-exchange__filters']//sort-items");
                ExecuteSortFilterParameter(webDriver, explicitWait, "//div[@class='cdk-overlay-container']//div[@class='cdk-overlay-pane']//button[6]");
                ExecuteActionWithDialogWindow(webDriver, explicitWait, "//div[@class='cdk-overlay-container']//div[@class='cdk-overlay-pane']//mat-icon");
            }
            ExecuteSearchFilter(webDriver, explicitWait, "//div[@class='c-exchange__container']//filters[@class='c-exchange__filters']//input", item);
            ExecuteButtonSearchFilter(webDriver, explicitWait, "//div[@class='c-exchange__container']//filters[@class='c-exchange__filters']//div[3]//button");
            Thread.Sleep(1000);
            bool isFound = ExecuteSearchNotFoundPage(webDriver, explicitWait, "//market-side//market-inventory//assets-card-scroll//div[@class='c-assets__container']//asset-card", 0);
            if (isFound)
            {
                ExecuteClearFilter(webDriver, explicitWait, "//div[@class='c-exchange__container']//filters[@class='c-exchange__filters']//button[@class='o-filter__searchClear']");
                return;
            }
            IWebElement parseElement = SelectElement(webDriver, explicitWait, "//market-side//market-inventory//assets-card-scroll//div[@class='c-assets__container']//asset-card");
            string content = parseElement.GetAttribute("innerText");
            ExecuteClearFilter(webDriver, explicitWait, "//div[@class='c-exchange__container']//filters[@class='c-exchange__filters']//button[@class='o-filter__searchClear']");
        }
        public void ParseSkinsFromTradeit(IWebDriver webDriver, string item, bool isFirstLoad)
        {
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);//5
            webDriver.Manage().Window.Maximize();
            WebDriverWait explicitWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(2));
            webDriver.Navigate().GoToUrl(UrlSiteParsing.Tradeit);
            ExecuteSortFilter(webDriver, explicitWait, "//main//div[contains(@class,'site-inventory')]//div[contains(@class,'filter-wrapper')]" +
                "//div[contains(@class,'filter-control-wrapper')]//div[contains(@class,'sort-selector-wrapper')]//div[contains(@class,'dropdown-wrapper')]//div");
            ExecuteSortFilterParameter(webDriver, explicitWait, "//div[@class='v-overlay-container']//div[@class='v-overlay__content']/div/div[4]");//for 3 and 5 buttons it works for 4 it doesn't
        }

        public void ExecuteSortFilter(IWebDriver webDriver, WebDriverWait explicitWait, string pattern)
        {
            IWebElement sortFilter = explicitWait.Until((webDriver) =>
            {
                try
                {
                    IWebElement element = webDriver.FindElement(By.XPath(pattern));
                    return element;
                }
                catch (NoSuchElementException exception)
                {
                    return null;
                }
            });
            sortFilter.Click();
        }
        public void ExecuteSortFilterParameter(IWebDriver webDriver, WebDriverWait explicitWait, string pattern)
        {
            IWebElement sortFilterParameter = explicitWait.Until((webDriver) =>
            {
                try
                {
                    IWebElement element = webDriver.FindElement(By.XPath(pattern));
                    return element;
                }
                catch (NoSuchElementException exception)
                {
                    return null;
                }
            });
            sortFilterParameter.Click();
        }
        public void ExecuteSearchFilter(IWebDriver webDriver, WebDriverWait explicitWait, string pattern, string item)
        {
            IWebElement searchFilter = explicitWait.Until((webDriver) =>
            {
                try
                {
                    IWebElement element = webDriver.FindElement(By.XPath(pattern));
                    return element;
                }
                catch (Exception)
                {
                    return null;
                }

            });
            searchFilter.Click();
            searchFilter.SendKeys(item);
        }
        public void ExecuteClearFilter(IWebDriver webDriver, WebDriverWait explicitWait, string pattern)
        {
            IWebElement buttonClearFilter = explicitWait.Until((webDriver) =>
            {
                try
                {
                    IWebElement element = webDriver.FindElement(By.XPath(pattern));
                    return element;
                }
                catch (Exception)
                {
                    return null;
                }
            });
            buttonClearFilter.Click();
        }
        public void ExecuteButtonSearchFilter(IWebDriver webDriver, WebDriverWait explicitWait, string pattern)
        {
            IWebElement buttonSearchFilter = explicitWait.Until((webDriver) =>
            {
                try
                {
                    IWebElement element = webDriver.FindElement(By.XPath(pattern));
                    return element;
                }
                catch (NoSuchElementException exception)
                {
                    return null;
                }
            });
            buttonSearchFilter.Click();
        }
        public bool ExecuteSearchNotFoundPage(IWebDriver webDriver, WebDriverWait explicitWait, string pattern, int boundry)
        {
            IEnumerable<IWebElement> elementsCollection = explicitWait.Until((webDriver) =>
            {
                try
                {
                    IEnumerable<IWebElement> elements = webDriver.FindElements(By.XPath(pattern));
                    return elements;
                }
                catch (Exception)
                {
                    return null;
                }
            });
            if (elementsCollection.Count() == boundry)
                return true;
            return false;
        }
        public IWebElement SelectElement(IWebDriver webDriver, WebDriverWait explicitWait, string pattern)
        {
            IWebElement selectedElement = explicitWait.Until((webDriver) =>
            {
                try
                {
                    IWebElement element = webDriver.FindElement(By.XPath(pattern));
                    return element;
                }
                catch (NoSuchElementException exception)
                {
                    return null; ;
                }
            });
            return selectedElement;
        }
        public void ExecuteActionWithDialogWindow(IWebDriver webDriver, WebDriverWait explicitWait, string pattern)
        {
            IWebElement button = explicitWait.Until((webDriver) =>
            {
                try
                {
                    IWebElement element = webDriver.FindElement(By.XPath(pattern));
                    return element;
                }
                catch (NoSuchElementException exception)
                {
                    return null;
                }
            });
            button.Click();
        }
    }
}
