using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class Shop
    {
        public List<Weapon> Weapons { get; set; }
        public List<Armor> Armor { get; set; }
        public List<Potion> Potions { get; set; } 

        private Dictionary<string, object> ItemCatalog { get; set; }

        public Game Game { get; set; }

        public Shop(Game game)
        {
            Weapons = new List<Weapon>();
            Armor = new List<Armor>();
            Potions = new List<Potion>();
            ItemCatalog = new Dictionary<string, object>();
            Game = game;

            StockShop();

        }
        private void StockShop()
        {
            // add a bunch of shit to the cooresponding lists

            for (var loop = 1; loop <= 5; loop++)
            {
                #region Armor           
                //var leatherArmor = new Armor("Leather Armor", 3, 10, 5);
                //Armor.Add(leatherArmor);
                Armor.Add(new Armor("Leather Armor", 3, 10, 5));
                Armor.Add(new Armor("Wooden Armor", 3, 10, 2));
                Armor.Add(new Armor("Metal Armor", 7, 20, 7));
                #endregion

                #region Weapons
                Weapons.Add(new Weapon("Sword", 3, 10, 2));
                Weapons.Add(new Weapon("Axe", 4, 12, 3));
                Weapons.Add(new Weapon("Longsword", 7, 20, 5));
                #endregion

                #region Potions
                Potions.Add(new Potion(5, "Light Healing Potion", 3, 1));
                Potions.Add(new Potion(7, "Medium Healing Potion", 5, 2));
                Potions.Add(new Potion(10, "Heavy Healing Potion", 7, 3));
                Potions.Add(new Potion(20, "Extreme Healing Potion", 10, 5));
                #endregion
            }
        }

        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Shop O' Shit! Would you like to look at my shit or sell your own shit?");
            Console.WriteLine("1. Browse the shit");
            Console.WriteLine("2. Sell your shit");
            Console.WriteLine("3. Return to main menu");

            Console.Write("Enter your selection: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowInventory();
                    Sell();
                    break;
                case "2":
                    BuyFromUser();
                    break;
                case "3":                                       
                default:
                    this.Game.Main();
                    break;
            }
        }

        #region Buying stuff
        public void ShowInventory()
        {
            ItemCatalog.Clear();

            Console.Clear();
            Console.WriteLine("Here's all the shit I have for sale.");
            ShowArmor();
            ShowWeapons();
            ShowPotions();
        }
        
        private void ShowArmor()
        {
            Console.WriteLine("");
            Console.WriteLine("*****Armor*****");
            var count = 1;
            foreach (var a in Armor.OrderBy(x => x.Name))
            {
                Console.WriteLine($"a{count}: {a.Name} with {a.Defense} Defense for {a.OriginalValue} gold");

                ItemCatalog.Add($"a{count}", a);
                count++;
            }
        }

        private void ShowWeapons()
        {
            Console.WriteLine("");
            Console.WriteLine("*****Weapons*****");
            var count = 1;
            foreach (var w in this.Weapons.OrderBy(x => x.Name))
            {
                Console.WriteLine($"w{count}: {w.Name} with {w.Strength} Strength for {w.OriginalValue} gold");

                ItemCatalog.Add($"w{count}", w);
                count++;
            }
        }

        private void ShowPotions()
        {
            Console.WriteLine("");
            Console.WriteLine("*****Potions*****");
            var count = 1;
            foreach (var p in Potions.OrderBy(x => x.Name))
            {
                Console.WriteLine($"p{count}: {p.Name} that grants you {p.HP} hit points for {p.OriginalValue} gold");

                ItemCatalog.Add($"p{count}", p);
                count++;
            }
        }

        public void Sell()
        {
            //var selection = "";
            
            Console.WriteLine("");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Buy something");
            Console.WriteLine("2. Go back");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    BuySomething();
                    break;
                case "2":
                    Menu();
                    break;
                default:
                    Menu();
                    break;
            }
            

            
            
        }

        public void BuySomething()
        {
            var selection = "";
            while (string.IsNullOrEmpty(selection))
            {
                Console.WriteLine("Please make a selection for what you would like to buy");
                selection = Console.ReadLine();
            }
        
            if (!ItemCatalog.ContainsKey(selection))
            {
                Console.WriteLine("");
                Console.WriteLine("Please enter a valid item number.");
                Sell();
            }

            if (VerifyGold(selection))
            {
                FinalizeSale(selection);
            }
            else
            {
                Console.WriteLine("You don't have enough gold! come back when you've made more money, peasant! (press any key to continue)");
                Console.ReadKey();
                Menu();
            }
        }

        public bool VerifyGold(string selection)
        {
            //if hero.gold >= item.originalvalue give them the item and subtract originalvalue from hero.gold
            //else say funds are insuffecient, come back when youve made some money
            
            switch (selection.Substring(0,1))
            {
                case "w":
                    var weapon = (Weapon)ItemCatalog[selection];
                    return Game.Hero.Gold >= weapon.OriginalValue;
                case "a":
                    var armor = (Armor)ItemCatalog[selection];
                    return Game.Hero.Gold >= armor.OriginalValue;
                case "p":
                    var potion = (Potion)ItemCatalog[selection];
                    return Game.Hero.Gold >= potion.OriginalValue;
                default:
                    return false;
            }
        }

        private void FinalizeSale(string selection)
        {
            switch (selection.Substring(0, 1))
            {
                case "w":
                    var weapon = (Weapon)ItemCatalog[selection];
                    Game.Hero.Gold -= weapon.OriginalValue;
                    Game.Hero.WeaponsBag.Add(weapon);
                    Weapons.Remove(weapon);
                    Menu();
                    break;
                case "a":
                    var armor = (Armor)ItemCatalog[selection];
                    Game.Hero.Gold -= armor.OriginalValue;
                    Game.Hero.ArmorsBag.Add(armor);
                    Armor.Remove(armor);
                    Menu();
                    break;
                case "p":
                    var potion = (Potion)ItemCatalog[selection];
                    Game.Hero.Gold -= potion.OriginalValue;
                    Game.Hero.PotionsBag.Add(potion);
                    Potions.Remove(potion);
                    Menu();
                    break;
                default:
                    Menu();
                    break;
            }
        }
        #endregion

        public void BuyFromUser()
        {
            Console.Clear();
            Game.Hero.ItemsToSell();
            Console.WriteLine("");
            Console.WriteLine("Here is everything you can sell. What would you like to do?");
            Console.WriteLine("1. Sell Something");
            Console.WriteLine("2. Return to Shop menu");
            Console.WriteLine("3. Return to main menu");

            Console.Write("Enter your selection: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Game.Hero.SellYourStuff();
                    break;
                case "2":
                    Menu();
                    break;
                case "3":
                default:
                    this.Game.Main();
                    break;
            }
        }
    }

    
}

//Console.WriteLine("");
//            Console.WriteLine("*************************");
//            Console.WriteLine("Press any key to continue");
//            Console.ReadKey();
//            Menu();