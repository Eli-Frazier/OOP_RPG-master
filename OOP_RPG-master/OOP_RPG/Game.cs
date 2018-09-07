using System;
using System.Collections.Generic;

namespace OOP_RPG
{
    public class Game
    {
        public Hero Hero { get; set; }
        public Shop Shop { get; set; }
        
        
        public Game() {
            Shop = new Shop(this);
            this.Hero = new Hero(Shop);
        }

        public void Start() {
            Console.WriteLine("Welcome hero!");
            Console.Write("Please enter your name: ");
            this.Hero.Name = Console.ReadLine();

            //Console.WriteLine("Hello " + hero.Name);
            //Console.WriteLine(string.Format("Hello {0}", hero.Name));
            //Console.WriteLine($"Hello {hero.Name}");

            /*this.*/Main();
        }
        
        public void Main() {
            Console.Clear();
            Console.WriteLine($"Hello {Hero.Name}");
            Console.WriteLine("Please choose an option by entering a number.");
            Console.WriteLine("1. View Stats");
            Console.WriteLine("2. View Inventory");
            Console.WriteLine("3. Fight Monster");
            Console.WriteLine("4. Enter Shop");
            Console.WriteLine("5. Quit Game");

            Console.Write("Enter your selection: ");
            var input = Console.ReadLine();

            #region old if else if else 
            //if (input == "1") {
            //    this.Stats();
            //}
            //else if (input == "2") {
            //    this.Inventory();
            //}
            //else if (input == "3") {
            //    this.Fight();
            //}
            //else {
            //    return;
            //}
            #endregion

            switch (input)
            {
                case "1":
                    Stats();
                    break;
                case "2":
                    Inventory();
                    break;
                case "3":
                    Fight();
                    break;
                case "4":
                    Shop.Menu();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Main();
                    break;
            }

        
        }
        public void Stats() {
            Console.Clear();
            Hero.ShowStats();
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey();
            Main();
        }
        
        public void Inventory(){
            
            Hero.ShowInventory();
            
        }
        
        public void Fight(){
            var fight = new Fight(Hero, this);
            fight.Start();
        }
        
     

    }
}