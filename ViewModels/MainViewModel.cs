using Analyzer.Models;
using Analyzer.Models.Enums;
using Analyzer.Models.Static;
using Analyzer.Models.StaticClasses;
using AngleSharp;
using AngleSharp.Browser;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace Analyzer.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private List<string> ListSkins;
        private IWebDriver driver;
        public MainViewModel()
        {
            FormListSkins();
            Parser parser = new Parser();
            driver = new ChromeDriver();
            bool isFirst = true;
            //foreach (var item in ListSkins)
            //{
            //    string[] arr = item.Split('#');
            //    parser.ParseSkinsFromCsGoMarket(driver, arr[0]+" " + arr[1],isFirst);
            //    isFirst = false;
            //    Thread.Sleep(2000);
            //}
            parser.ParseSkinsFromTradeit(driver, "asdf", isFirst);


        }
        private List<string> FormSkin(Type sourceType, string nameProperty)
        {
            List<string> outputData = new List<string>();
            FieldInfo propertyInfo = sourceType.GetField(nameProperty);
            Dictionary<string, string> propertyValue = (Dictionary<string, string>)propertyInfo.GetValue(null);
            foreach (var value in propertyValue)
            {
                FieldInfo propertyInfo2 = sourceType.GetField(value.Value); //get the name of the variable that contains the name of the skins for the weapon
                Dictionary<string, RarityStats> skinQuality = (Dictionary<string, RarityStats>)propertyInfo2.GetValue(null);
                foreach (var item in skinQuality)
                {
                    outputData.Add($"{value.Key}#{item.Key}#{item.Value.ToString()}");
                }
            }
            return outputData;
        }
        private void FormListSkins()
        {
            ListSkins = FormSkin(typeof(HeavyWeapons), nameof(HeavyWeapons.ListHeavyWeapons));
            ListSkins.AddRange(FormSkin(typeof(Pistols), nameof(Pistols.ListPistols)));
            ListSkins.AddRange(FormSkin(typeof(Rifles), nameof(Rifles.ListRifles)));
            ListSkins.AddRange(FormSkin(typeof(SubmachineGuns), nameof(SubmachineGuns.ListSubmachineGuns)));
        }







    }
}
