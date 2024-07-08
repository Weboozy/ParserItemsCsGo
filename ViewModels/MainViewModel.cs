using Analyzer.Models;
using Analyzer.Models.Enums;
using Analyzer.Models.StaticClasses;
using AngleSharp;
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
        [ObservableProperty]
        public int amountTypesItems;
        public List<TypesItems> ListTypesItems { get; set; } = new List<TypesItems>();
        public List<RarityStats> ListRarityStats { get; set; } = new List<RarityStats>();
        public ObservableCollection<PresentedItem> ListPresentedItems { get; set; } = new ObservableCollection<PresentedItem>();


        //private IWebDriver dirver = new ChromeDriver();



        public List<string> SearchingList = new List<string>();

        private List<TypesItems> SortingParameterByTypeItem = new List<TypesItems>()
        {
            TypesItems.rifles,
            TypesItems.pistols,
            TypesItems.submachineGuns,
            TypesItems.heavyWeapon
        };
        private List<RarityStats> SortingParameterByRarityItem = new List<RarityStats>() {
            RarityStats.industrialGrade
        };
        private List<string> SrotingParameterByNameItem = new List<string>() {
            ""
        };

        //private IWebDriver driver = new ChromeDriver();
        private Searcher searchService;
        public MainViewModel()
        {
            searchService = new Searcher();
            FillListItems();
            AmountTypesItems = ListTypesItems.Count();
            FormSearchingList(SortingParameterByTypeItem);
            //searchService.SearchItems(UrlSiteParsing.Dmarket, "Mac 10 fade", dirver, true);
            Task task = new Task(StartParsing);
            task.Start();


        }
        private void FormSearchingList(List<TypesItems> typesItems) {
            foreach (var item in typesItems)
            {
                switch (item)
                {
                    case TypesItems.heavyWeapon:
                        FillSearchingList(HeavyWeapons.DictionarySkinsNovaShotgun,"Nova");
                        FillSearchingList(HeavyWeapons.DictionarySkinsXM1014Shotgun, "XM1014");
                        FillSearchingList(HeavyWeapons.DictionarySkinsMAG7Shotgun,"MAG-7");
                        FillSearchingList(HeavyWeapons.DictionarySkinsSawedOffShotgun,"Sawed-Off");
                        FillSearchingList(HeavyWeapons.DictionarySkinsM249MachineGun, "M249");
                        FillSearchingList(HeavyWeapons.DictionarySkinsNegevMachineGun, "Negev");
                        break;
                    case TypesItems.pistols:
                        FillSearchingList(Pistols.ListSkinsCZ75Auto, "CZ75-Auto");
                        FillSearchingList(Pistols.ListSkinsP2000, "P2000");
                        FillSearchingList(Pistols.ListSkinsP250, "P250");
                        FillSearchingList(Pistols.ListSkinsFiveSeveN, "Five-SeveN");
                        FillSearchingList(Pistols.ListSkinsUSPS, "USP-S");
                        FillSearchingList(Pistols.ListSkinsGlock18, "Glock-18");
                        FillSearchingList(Pistols.ListSkinsDesertEagle, "Desert Eagle");
                        FillSearchingList(Pistols.ListSkinsTec9, "Tec-9");
                        FillSearchingList(Pistols.ListSkinsDualBerettas, "Dual Berettas");
                        FillSearchingList(Pistols.ListSkinsRevolver, "Revolver");
                        break;
                    case TypesItems.rifles:
                        FillSearchingList(Rifles.ListSkinsFAMAS, "FAMAS");
                        FillSearchingList(Rifles.ListSkinsGalilAR, "Galil AR");
                        FillSearchingList(Rifles.ListSkinsM4A1, "M4A1");
                        FillSearchingList(Rifles.ListSkinsM4A1S, "M4A1-S");
                        FillSearchingList(Rifles.ListSkinsAK47, "AK-47");
                        FillSearchingList(Rifles.ListSkinsAUG, "AUG");
                        FillSearchingList(Rifles.ListSkinsG3SG1, "G3SG1");
                        FillSearchingList(Rifles.ListSkinsSG553, "SG 553");
                        FillSearchingList(Rifles.ListSkinsAWP, "AWP");
                        FillSearchingList(Rifles.ListSkinsSCAR20, "SCAR-20");
                        FillSearchingList(Rifles.ListSkinsSSG08, "SSG 08");
                        break;
                    case TypesItems.submachineGuns:
                        FillSearchingList(SubmachineGuns.ListSkinsMAC10, "MAC-10");
                        FillSearchingList(SubmachineGuns.ListSkinsMP9, "MP9");
                        FillSearchingList(SubmachineGuns.ListSkinsMP7, "MP7");
                        FillSearchingList(SubmachineGuns.ListSkinsMP5SD, "MP5-SD");
                        FillSearchingList(SubmachineGuns.ListSkinsUMP45, "UMP-45");
                        FillSearchingList(SubmachineGuns.ListSkinsP90, "P90");
                        FillSearchingList(SubmachineGuns.ListSkinsPP19Bison, "PP19 Bison");
                        break;
                    default:
                        break;
                }
            }
        }
        private void FillSearchingList(Dictionary<string,RarityStats> nameWeaponSkin,string nameWeapon) {
            foreach (var skin in nameWeaponSkin)
            {
                SearchingList.Add(nameWeapon + " " + skin.Key);
            }
        }

        private void StartParsing() {
            bool isFirst = true;
            IWebDriver chromeDriverForCsMarket = new ChromeDriver();
            IWebDriver chromeDriverForShadowPay = new ChromeDriver();
            //IWebDriver chromeDriverForDmarket = new ChromeDriver();


            foreach (var item in SearchingList)
            {
                Task<ParsedItem> task1 = new Task<ParsedItem>(() => { return searchService.SearchItems(UrlSiteParsing.CsGoMarket, item, chromeDriverForCsMarket, isFirst); });
                Task<ParsedItem> task2 = new Task<ParsedItem>(() => { return searchService.SearchItems(UrlSiteParsing.ShadowPay, item, chromeDriverForShadowPay, isFirst); });
                //Task<ParsedItem> task3 = new Task<ParsedItem>(() => { return searchService.SearchItems(UrlSiteParsing.Dmarket, item, chromeDriverForDmarket, isFirst); });
                task1.Start();
                task2.Start();
                //task3.Start();
                Task.WaitAll( task1,task2);

                object obj = new object();
                lock (obj) {
                    PresentedItem presentedItem = new PresentedItem()
                    {
                        NameItem = item,
                        ListSources = new List<string> { task1.Result.Source, task2.Result.Source },
                        ListPrices = new List<decimal> { task1.Result.Price, task2.Result.Price },
                        Profit = 0
                    };
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ListPresentedItems.Add(presentedItem);
                    });
                }
                
                Thread.Sleep(2000);
                isFirst = false;
            }
        }

        private void FillListItems() {
            foreach (TypesItems item in Enum.GetValues(typeof(TypesItems)))
            {
                ListTypesItems.Add(item);
            }
            foreach (RarityStats item in Enum.GetValues(typeof(RarityStats)))
            {
                ListRarityStats.Add(item);
            }
        }


      

        
    }
}
