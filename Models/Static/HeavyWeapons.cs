using AngleSharp.Dom;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using Analyzer.Models.Enums;

namespace Analyzer.Models.Static
{
    public static class HeavyWeapons
    {
        public static Dictionary<string, string> ListHeavyWeapons = new Dictionary<string, string> {
            { "Nova Shotgun","DictionarySkinsNovaShotgun" },
            { "XM1014 Shotgun","DictionarySkinsXM1014Shotgun" },
            { "MAG-7 Shotgun","DictionarySkinsMAG7Shotgun" },
            { "Sawed-Off Shotgun", "DictionarySkinsSawedOffShotgun" },
            { "M249 machine gun", "DictionarySkinsM249MachineGun" },
            { "Negev machine gun","DictionarySkinsNegevMachineGun" }
        };
        public static Dictionary<string, RarityStats> DictionarySkinsNovaShotgun = new Dictionary<string, RarityStats>
        {
            {"Modern Hunter",RarityStats.milSpec},
            {"Baroque Orange",RarityStats.restricted},
            {"Bloomstick",RarityStats.classified},
            {"Blaze Orange",RarityStats.milSpec},
            {"Hyper Beast",RarityStats.classified},
            {"Quick Sand",RarityStats.milSpec},
            {"Green Apple",RarityStats.industrialGrade},
            {"Walnut",RarityStats.consumerGrade},
            {"Tempest",RarityStats.milSpec},
            {"Interlock",RarityStats.milSpec},
            {"Antique",RarityStats.classified},
            {"Sobek's Bite",RarityStats.restricted},
            {"Graphite",RarityStats.restricted},
            {"Red Quartz",RarityStats.restricted},
            {"Rising Skull",RarityStats.restricted},
            {"Clear Polymer",RarityStats.restricted},
            {"Moon in Libra",RarityStats.consumerGrade},
            {"Forest Leaves",RarityStats.consumerGrade},
            {"Toy Soldier",RarityStats.restricted},
            {"Rust Coat",RarityStats.industrialGrade},
            {"Ghost Camo",RarityStats.milSpec},
            {"Koi",RarityStats.restricted},
            {"Plume",RarityStats.milSpec},
            {"Gila",RarityStats.restricted},
            {"Wild Six",RarityStats.restricted},
            {"Army Sheen",RarityStats.consumerGrade},
            {"Ranger",RarityStats.milSpec},
            {"Windblown",RarityStats.milSpec},
            {"Exo",RarityStats.milSpec},
            {"Wood Fired",RarityStats.milSpec},
            {"Caged Steel",RarityStats.industrialGrade},
            {"Candy Apple",RarityStats.industrialGrade},
            {"Sand Dune",RarityStats.consumerGrade},
            {"Polar Mesh",RarityStats.consumerGrade},
            {"Predator",RarityStats.consumerGrade},
            {"Mandrel",RarityStats.consumerGrade}

        };
        public static Dictionary<string, RarityStats> DictionarySkinsXM1014Shotgun = new Dictionary<string, RarityStats>()
        {
            {"Frost Borre",RarityStats.milSpec},
            {"Elegant Vines",RarityStats.restricted},
            {"Ancient Lore",RarityStats.restricted},
            {"Red Leather",RarityStats.milSpec},
            {"Blaze Orange",RarityStats.milSpec},
            {"Banana Leaf",RarityStats.industrialGrade},
            {"Tranquility",RarityStats.classified},
            {"VariCamo Blue",RarityStats.milSpec},
            {"Heaven Guard",RarityStats.restricted},
            {"Incinegator",RarityStats.classified},
            {"Bone Machine",RarityStats.milSpec},
            {"XOXO",RarityStats.classified},
            {"Entombed",RarityStats.classified},
            {"Fallout Warning",RarityStats.industrialGrade},
            {"Jungle",RarityStats.consumerGrade},
            {"Watchdog",RarityStats.milSpec},
            {"Grassland",RarityStats.consumerGrade},
            {"Seasons",RarityStats.restricted},
            {"Urban Perforated",RarityStats.consumerGrade},
            {"Teclu Burner",RarityStats.restricted},
            {"Red Python",RarityStats.milSpec},
            {"Black Tie",RarityStats.restricted},
            {"Ziggy",RarityStats.restricted},
            {"Blue Tire",RarityStats.consumerGrade},
            {"Scumbria",RarityStats.milSpec},
            {"Zombie Offensive",RarityStats.restricted},
            {"Charter",RarityStats.consumerGrade},
            {"Quicksilver",RarityStats.milSpec},
            {"Slipstream",RarityStats.milSpec},
            {"Oxide Blaze",RarityStats.milSpec},
            {"Blue Spruce",RarityStats.consumerGrade},
            {"Blue Steel",RarityStats.industrialGrade},
            {"Hieroglyph",RarityStats.consumerGrade},
            {"CaliCamo",RarityStats.industrialGrade}

        };
        public static Dictionary<string, RarityStats> DictionarySkinsMAG7Shotgun = new Dictionary<string, RarityStats>()
        {
            {"Cinquedea",RarityStats.classified},
            {"Bulldozer",RarityStats.restricted},
            {"Counter Terrace",RarityStats.milSpec},
            {"Prism Terrace",RarityStats.restricted},
            {"Chainmail",RarityStats.industrialGrade},
            {"Silver",RarityStats.industrialGrade},
            {"Hazard",RarityStats.milSpec},
            {"Justice",RarityStats.classified},
            {"Memento",RarityStats.milSpec},
            {"Sand Dune",RarityStats.consumerGrade},
            {"Carbon Fiber",RarityStats.industrialGrade},
            {"Seabird",RarityStats.consumerGrade},
            {"Core Breach",RarityStats.restricted},
            {"Praetorian",RarityStats.restricted},
            {"Irradiated Alert",RarityStats.consumerGrade},
            {"Hard Water",RarityStats.milSpec},
            {"Firestarter",RarityStats.milSpec},
            {"Heat",RarityStats.restricted},
            {"BI83 Spectrum",RarityStats.restricted},
            {"Copper Coated",RarityStats.milSpec},
            {"Storm",RarityStats.consumerGrade},
            {"Monster Call",RarityStats.restricted},
            {"Petroglyph",RarityStats.restricted},
            {"SWAG-7",RarityStats.restricted},
            {"Popdog",RarityStats.milSpec},
            {"Navy Sheen",RarityStats.consumerGrade},
            {"Heaven Guard",RarityStats.milSpec},
            {"Cobalt Core",RarityStats.milSpec},
            {"Sonar",RarityStats.milSpec},
            {"Insomnia",RarityStats.milSpec},
            {"Foresight",RarityStats.milSpec},
            {"Metallic DDPAT",RarityStats.industrialGrade},
            {"Rust Coat",RarityStats.consumerGrade}
        };
        public static Dictionary<string, RarityStats> DictionarySkinsSawedOffShotgun = new Dictionary<string, RarityStats>()
        {
            {"Copper",RarityStats.milSpec},
            {"First Class",RarityStats.milSpec},
            {"Orange DDPAT",RarityStats.restricted},
            {"Rust Coat",RarityStats.industrialGrade},
            {"Devourer",RarityStats.classified},
            {"Kiss♥Love",RarityStats.classified},
            {"Wasteland Princess",RarityStats.classified},
            {"The Kraken",RarityStats.covert},
            {"Jungle Thicket",RarityStats.consumerGrade},
            {"Bamboo Shadow",RarityStats.consumerGrade},
            {"Mosaico",RarityStats.industrialGrade},
            {"Irradiated Alert",RarityStats.consumerGrade},
            {"Serenity",RarityStats.restricted},
            {"Highwayman",RarityStats.restricted},
            {"Limelight",RarityStats.restricted},
            {"Apocalypto",RarityStats.restricted},
            {"Sage Spray",RarityStats.consumerGrade},
            {"Brake Light",RarityStats.milSpec},
            {"Parched",RarityStats.consumerGrade},
            {"Clay Ambush",RarityStats.consumerGrade},
            {"Zander",RarityStats.milSpec},
            {"Amber Fade",RarityStats.milSpec},
            {"Full Stop",RarityStats.milSpec},
            {"Yorick",RarityStats.milSpec},
            {"Morris",RarityStats.milSpec},
            {"Origami",RarityStats.milSpec},
            {"Black Sand",RarityStats.milSpec},
            {"Spirit Board",RarityStats.milSpec},
            {"Snake Camo",RarityStats.industrialGrade},
            {"Fubar",RarityStats.milSpec},
            {"Forest DDPAT",RarityStats.consumerGrade}


        };
        public static Dictionary<string, RarityStats> DictionarySkinsM249MachineGun = new Dictionary<string, RarityStats>() {
            {"Shipping Forecast",RarityStats.industrialGrade},
            {"Humidor",RarityStats.milSpec},
            {"Blizzard Marbleized",RarityStats.industrialGrade},
            {"Jungle",RarityStats.consumerGrade},
            {"Midnight Palm",RarityStats.industrialGrade},
            {"Impact Drill",RarityStats.consumerGrade},
            {"Nebula Crusader",RarityStats.restricted},
            {"Emerald Poison Dart",RarityStats.restricted},
            {"Jungle DDPAT",RarityStats.consumerGrade},
            {"Aztec",RarityStats.restricted},
            {"Magma",RarityStats.milSpec},
            {"Contrast Spray",RarityStats.consumerGrade},
            {"Downtown",RarityStats.restricted},
            {"Predator",RarityStats.consumerGrade},
            {"Warbird",RarityStats.milSpec},
            {"Deep Relief",RarityStats.milSpec},
            {"System Lock",RarityStats.milSpec},
            {"O.S.I.P.R.",RarityStats.milSpec},
            {"Spectre",RarityStats.milSpec},
            {"Submerged",RarityStats.consumerGrade},
            {"Gator Mesh",RarityStats.industrialGrade},

        };
        public static Dictionary<string, RarityStats> DictionarySkinsNegevMachineGun = new Dictionary<string, RarityStats>()
        {
            {"Mjölnir",RarityStats.classified},
            {"Anodized Navy",RarityStats.milSpec},
            {"Infrastructure",RarityStats.milSpec},
            {"Phoenix Stencil",RarityStats.milSpec},
            {"CaliCamo",RarityStats.industrialGrade},
            {"Bratatat",RarityStats.milSpec},
            {"Power Loader",RarityStats.restricted},
            {"Loudmouth",RarityStats.restricted},
            {"Palm",RarityStats.industrialGrade},
            {"Boroque Sand",RarityStats.consumerGrade},
            {"Nuclear Waste",RarityStats.industrialGrade},
            {"dev_texture",RarityStats.restricted},
            {"Lionfish",RarityStats.restricted},
            {"Dazzle",RarityStats.milSpec},
            {"Terrain",RarityStats.milSpec},
            {"Prototype",RarityStats.milSpec},
            {"Drop Me",RarityStats.milSpec},
            {"Desert-Strike",RarityStats.milSpec},
            {"Ultralight",RarityStats.milSpec},
            {"Man-o'-war",RarityStats.industrialGrade},
            {"Bulkhead",RarityStats.industrialGrade},
            {"Army Sheen",RarityStats.consumerGrade},
        };
    }
}
