using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Hero
    {
        // These are the Properties of our Class.
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int OriginalHP { get; set; }
        public int CurrentHP { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }
        public int Gold { get; set; }
        public int Speed { get; set; }

        public Dictionary<string, object> UserItemCatalog { get; set; }

        public List<Armor> ArmorsBag { get; set; }
        public List<Weapon> WeaponsBag { get; set; }
        public List<Potion> PotionsBag { get; set; }

        public Shop Shop { get; set; }


        /*This is a Constructor. (it has the same name as the class)
        When we create a new object from our Hero class, the instance of this class, our hero, has:
        an empty List that has to contain instances of the Armor class,
        an empty List that has to contain instance of the Weapon class,
        stats of the "int" data type, including an intial strength and defense,
        original hitpoints that are going to be the same as the current hitpoints.
        */
        public Hero(Shop shop)
        {
            this.ArmorsBag = new List<Armor>();
            this.WeaponsBag = new List<Weapon>();
            PotionsBag = new List<Potion>();
            this.Strength = 10;
            this.Defense = 10;
            this.OriginalHP = 30;
            this.CurrentHP = 30;
            Speed = 9;
            Gold = 0;
            UserItemCatalog = new Dictionary<string, object>();
            Shop = shop;
        }

        //These are the Methods of our Class.
        public void ShowStats()
        {
            Console.WriteLine("*****" + this.Name + "*****");
            Console.WriteLine("Strength: " + this.Strength);
            Console.WriteLine("Defense: " + this.Defense);
            Console.WriteLine("Hitpoints: " + this.CurrentHP + "/" + this.OriginalHP);
        }

        public void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine("*****  INVENTORY ******");
            Console.WriteLine("**Weapons**");
            foreach (var w in this.WeaponsBag)
            {
                Console.WriteLine(w.Name + " of " + w.Strength + " Strength");
            }

            Console.WriteLine("");
            Console.WriteLine("**Armor**");
            foreach (var a in this.ArmorsBag)
            {
                Console.WriteLine(a.Name + " of " + a.Defense + " Defense");
            }

            Console.WriteLine("");
            Console.WriteLine("**Potions**");
            foreach (var a in this.PotionsBag)
            {
                Console.WriteLine($"{a.Name} that will grant {a.HP} Hitpoints");
            }

            Console.WriteLine("");
            Console.WriteLine("**Equipped**");
            if(EquippedArmor != null)
            {
                Console.WriteLine($"Armor: {EquippedArmor.Name}");
            }
            else
            {
                Console.WriteLine($"Armor:");
            }

            if(EquippedWeapon != null)
            {
                Console.WriteLine($"Weapon: {EquippedWeapon.Name}");
            }
            else
            {
                Console.WriteLine($"Weapon:");
            }

            Console.WriteLine("");
            Console.WriteLine("Gold: " + Gold);

            Console.WriteLine("");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Drink a potion");
            Console.WriteLine("2. Equip a weapon");
            Console.WriteLine("3. Equip an armor");
            Console.WriteLine("4. Unequip a weapon");
            Console.WriteLine("5. Unequip an armor");
            Console.WriteLine("6. Return to main menu");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    DrinkPotion();
                    break;
                case "2":
                    EquipWeapon();
                    break;
                case "3":
                    EquipArmor();
                    break;
                case "4":
                    UnequipWeapon();
                    break;
                case "5":
                    UnequipArmor();
                    break;
                case "6":
                    Shop.Game.Main();
                    break;
                default:
                    Shop.Game.Main();
                    break;
            }
        }

        #region Equip and Unequip armor and weapons
        public void EquipWeapon()
        {
            if(EquippedWeapon != null)
            {
                Console.WriteLine("You're already weilding a weapon");
                Console.ReadKey();
                ShowInventory();
            }

            UserItemCatalog.Clear();
            Console.WriteLine("");
            Console.WriteLine("*****Weapons*****");
            var count = 1;
            foreach (var w in this.WeaponsBag)
            {
                Console.WriteLine($"{count}: {w.Name} with {w.Strength} strength");

                UserItemCatalog.Add($"{count}", w);
                count++;
            }
            
            if(UserItemCatalog.Count == 0)
            {
                Console.WriteLine("You have no weapons to equip (press any button to continue)");
                Console.ReadKey();
                ShowInventory();
            }

            var selection = Console.ReadLine();

            var intselection = Convert.ToInt32(selection);
            if (intselection < 1 || intselection > UserItemCatalog.Count)
            {
                Console.WriteLine("Please make a valid selection");
                EquipWeapon();
                Console.ReadKey();
            }
            else
            {
                var weapon = (Weapon)UserItemCatalog[selection];
                WeaponsBag.Remove(weapon);
                EquippedWeapon = weapon;
                Strength += weapon.Strength;
                ShowInventory();
            }


            //Shop.Game.Main();

            //if (WeaponsBag.Any())
            //{
            //    this.EquippedWeapon = this.WeaponsBag[0];
            //}
        }

        public void EquipArmor()
        {
            if (EquippedArmor != null)
            {
                Console.WriteLine("You're already wearing armor (press any button to continue)");
                Console.ReadKey();
                ShowInventory();
            }

            UserItemCatalog.Clear();
            Console.WriteLine("");
            Console.WriteLine("*****Armor*****");
            var count = 1;
            foreach (var a in this.ArmorsBag)
            {
                Console.WriteLine($"{count}: {a.Name} with {a.Defense} defense");

                UserItemCatalog.Add($"{count}", a);
                count++;
            }

            if (UserItemCatalog.Count == 0)
            {
                Console.WriteLine("You have no armor to equip");
                Console.ReadKey();
                ShowInventory();
            }

            var selection = Console.ReadLine();

            var intselection = Convert.ToInt32(selection);
            if (intselection < 1 || intselection > UserItemCatalog.Count)
            {
                Console.WriteLine("Please make a valid selection");
                EquipArmor();
                Console.ReadKey();
            }
            else
            {
                var armor = (Armor)UserItemCatalog[selection];
                ArmorsBag.Remove(armor);
                EquippedArmor = armor;
                Defense += armor.Defense;
                ShowInventory();
            }

            //if (ArmorsBag.Any())
            //{
            //    this.EquippedArmor = this.ArmorsBag[0];
            //}
        }

        public void UnequipWeapon()
        {
            if (EquippedWeapon == null)
            {
                Console.WriteLine("You have nothing equipped to unequip");
                Console.ReadKey();
                ShowInventory();
            }
            Strength -= EquippedWeapon.Strength;
            WeaponsBag.Add(EquippedWeapon);
            EquippedWeapon = null;
            ShowInventory();
        }

        public void UnequipArmor()
        {
            if (EquippedArmor == null)
            {
                Console.WriteLine("You have nothing equipped to unequip");
                Console.ReadKey();
                ShowInventory();
            }
            Defense -= EquippedArmor.Defense;
            ArmorsBag.Add(EquippedArmor);
            EquippedArmor = null;
            ShowInventory();
        }
        #endregion

        public void DrinkPotion()
        {
            UserItemCatalog.Clear();
            Console.WriteLine("");
            Console.WriteLine("*****Potions*****");
            var count = 1;
            foreach (var p in this.PotionsBag)
            {
                Console.WriteLine($"{count}: {p.Name} that will give you {p.HP} HP");

                UserItemCatalog.Add($"{count}", p);
                count++;
            }

            if (UserItemCatalog.Count == 0)
            {
                Console.WriteLine("You have no potions to drink");
                Console.ReadKey();
                ShowInventory();
            }

            var selection = Console.ReadLine();

            var intselection = Convert.ToInt32(selection);
            if (intselection < 1 || intselection > UserItemCatalog.Count)
            {
                Console.WriteLine("Please make a valid selection");
                DrinkPotion();
                Console.ReadKey();
            }
            else
            {
                var potion = (Potion)UserItemCatalog[selection];
                PotionsBag.Remove(potion);
                CurrentHP += potion.HP;
                ShowInventory();
            }
        }

        #region sell your stuff
        public void ItemsToSell()
        {
            UserItemCatalog.Clear();

            ShowUserWeapons();
            ShowUserArmor();
            ShowUserPotions();
        }

        public void ShowUserArmor()
        {
            Console.WriteLine("");
            Console.WriteLine("*****Armor*****");
            var count = 1;
            foreach (var a in this.ArmorsBag)
            {
                Console.WriteLine($"a{count}: Sell {a.Name}  for {a.ResaleValue} gold");

                UserItemCatalog.Add($"a{count}", a);
                count++;
            }
        }

        public void ShowUserWeapons()
        {
            Console.WriteLine("*****Weapons*****");
            var count = 1;
            foreach (var w in this.WeaponsBag)
            {
                Console.WriteLine($"w{count}: Sell {w.Name} for {w.ResaleValue} gold");

                UserItemCatalog.Add($"w{count}", w);
                count++;
            }
        }

        public void ShowUserPotions()
        {
            Console.WriteLine("");
            Console.WriteLine("*****Potions*****");
            var count = 1;
            foreach (var p in this.PotionsBag)
            {
                Console.WriteLine($"p{count}:Sell {p.Name} for {p.ResaleValue} gold");

                UserItemCatalog.Add($"p{count}", p);
                count++;
            }
        }

        public void SellYourStuff()
        {
            if (UserItemCatalog.Count == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("You have nothing to sell, come back when you can give me something! (Press any key to continue)");
                Console.ReadKey();
                Shop.Menu();
            }

            var selection = "";
            while (string.IsNullOrEmpty(selection))
            {
                Console.WriteLine("");
                Console.Write("What would you like to sell? ");

                selection = Console.ReadLine();
            }

            if (!UserItemCatalog.ContainsKey(selection))
            {
                Console.WriteLine("");
                Console.WriteLine("Please enter a valid item number.");
                SellYourStuff();
            }

            switch (selection.Substring(0, 1))
            {
                case "w":
                    var weapon = (Weapon)UserItemCatalog[selection];
                    Gold += weapon.ResaleValue;
                    WeaponsBag.Remove(weapon);
                    Shop.Weapons.Add(weapon);
                    Shop.Menu();
                    break;
                case "a":
                    var armor = (Armor)UserItemCatalog[selection];
                    Gold += armor.ResaleValue;
                    ArmorsBag.Remove(armor);
                    Shop.Armor.Add(armor);
                    Shop.Menu();
                    break;
                case "p":
                    var potion = (Potion)UserItemCatalog[selection];
                    Gold += potion.ResaleValue;
                    PotionsBag.Remove(potion);
                    Shop.Potions.Add(potion);
                    Shop.Menu();
                    break;
                default:
                    Shop.Menu();
                    break;
            }
        }
        #endregion
    }
}
