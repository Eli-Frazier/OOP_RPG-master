using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Fight
    {
        List<Monster> Monsters { get; set; }

        //public Monster Enemy { get; set;  }

        public Game game { get; set; }
        public Hero hero { get; set; }
        
        public Fight(Hero hero, Game game) {
            this.Monsters = new List<Monster>();
            //this.Enemy = Enemy;
            this.hero = hero;
            this.game = game;

            this.AddMonster("Squid", 9, 5, 16, 10, 2);
            this.AddMonster("Skeleton", 15, 1, 15, 30, 5);
            this.AddMonster("Ghost", 11, 2, 15, 10, 3);
            this.AddMonster("Wolf", 16, 8, 20, 15, 10);
        }
        
        public void AddMonster(string name, int strength, int defense, int hp, int gold, int speed) {
            var monster = new Monster();
            monster.Name = name;
            monster.Strength = strength;
            monster.Defense = defense;
            monster.OriginalHP = hp;
            monster.CurrentHP = hp;
            monster.Gold = gold;
            monster.Speed = speed;
            this.Monsters.Add(monster);
        }
        
        public void Start() {
            Console.Clear();
            #region example code
            //makes us fight first monster (default)
            //var enemy = this.Monsters[0];

            //makes us fight last monster
            //var enemy = this.Monsters[this.Monsters.Count -1];

            //fight second monster
            //var enemy = this.Monsters[1];

            //fight first enemy w/ less than 20 HP
            //var enemy = this.Monsters.FirstOrDefault( m => m.CurrentHP < 20);

            //fight enemy with stength of at least 11
            //var enemy = this.Monsters.FirstOrDefault(m => m.Strength >= 11);
            #endregion
            //fight random enemy
            var random = new Random();
            var enemy = this.Monsters[random.Next(0, this.Monsters.Count)];

            Console.WriteLine("You've encountered a " + enemy.Name + "! " + enemy.Strength + " Strength/" + enemy.Defense + " Defense/" + 
            enemy.CurrentHP + " HP. What will you do?");
            Console.WriteLine("1. Fight");
            Console.WriteLine("2. Nevermind");
            
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    HeroTurn(enemy);
                    break;
                case "2":
                    game.Main();
                    break;
                default:
                    game.Main();
                    break;
            }
            //if (input == "1") {
            //    this.HeroTurn(enemy);
            //}
            //else { 
            //    this.game.Main();
            //}
        }

        public void WhatNow(Monster monster)
        {
            var enemy = monster;
            Console.WriteLine("");
            Console.WriteLine($"{enemy.Name} is at {enemy.CurrentHP} HP. What now?");
            Console.WriteLine($"you are at {game.Hero.CurrentHP} HP");
            Console.WriteLine("1. Fight");
            Console.WriteLine("2. Flee");
            Console.WriteLine("3. Drink Potion");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    HeroTurn(enemy);
                    break;
                case "2":
                    Flee(monster);
                    break;
                case "3":
                    DrinkPotion(monster);
                    break;
                default:
                    game.Main();
                    break;
            }
            //if (input == "1")
            //{
            //    this.HeroTurn(enemy);
            //}
            //else
            //{
            //    this.game.Main();
            //}
        }
        
        public void HeroTurn(Monster monster){
           var enemy = monster;
           var compare = hero.Strength - enemy.Defense;
           int damage;
           
           if(compare <= 0) {
               damage = 1;
               enemy.CurrentHP -= damage;
           }
           else{
               damage = compare;
               enemy.CurrentHP -= damage;
           }
           Console.WriteLine("");
           Console.WriteLine("You did " + damage + " damage!");
           
           if(enemy.CurrentHP <= 0){
               this.Win(enemy);
           }
           else
           {
               this.MonsterTurn(enemy);
           }
           
        }
        
        public void MonsterTurn(Monster monster){
           var enemy = monster;
           int damage;
           var compare = enemy.Strength - hero.Defense;
           if(compare <= 0) {
               damage = 1;
               hero.CurrentHP -= damage;
           }
           else{
               damage = compare;
               hero.CurrentHP -= damage;
           }
           Console.WriteLine(enemy.Name + " does " + damage + " damage!");
           if(hero.CurrentHP <= 0){
               this.Lose();
           }
           else
           {
               this.WhatNow(monster);
           }
        }
        
        public void Win(Monster monster) {
            var enemy = monster;
            Console.WriteLine($"{enemy.Name}  has been defeated! You win the battle! You won {enemy.Gold} gold! (press any key to continue)");
            hero.Gold += enemy.Gold;
            Console.ReadKey();
            game.Main();
            
        }
        
        public void Lose() {
            Console.WriteLine("You've been defeated! :( GAME OVER.");
            Console.WriteLine($"What would you like to do?");
            Console.WriteLine($"1. Start a new game");
            Console.WriteLine($"2. Exit");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    game.Start();
                    break;
                case "2":
                    Environment.Exit(0);
                    break;
                default:
                    Environment.Exit(0);
                    break;

            }
        }

        public void DrinkPotion(Monster monster)
        {
            game.Hero.UserItemCatalog.Clear();
            Console.WriteLine("");
            Console.WriteLine("*****Potions*****");
            var count = 1;
            foreach (var p in game.Hero.PotionsBag)
            {
                Console.WriteLine($"{count}: {p.Name} that will give you {p.HP} HP");

                game.Hero.UserItemCatalog.Add($"{count}", p);
                count++;
            }

            if (game.Hero.UserItemCatalog.Count == 0)
            {
                Console.WriteLine("You have no weapons to equip");
                Console.ReadKey();
                WhatNow(monster);
            }

            var selection = Console.ReadLine();

            var intselection = Convert.ToInt32(selection);
            if (intselection < 1 || intselection > game.Hero.UserItemCatalog.Count)
            {
                Console.WriteLine("Please make a valid selection");
                DrinkPotion(monster);
                Console.ReadKey();
            }
            else
            {
                var potion = (Potion)game.Hero.UserItemCatalog[selection];
                game.Hero.PotionsBag.Remove(potion);
                game.Hero.CurrentHP += potion.HP;
                this.WhatNow(monster);
            }
        }

        public void Flee(Monster monster)
        {
            if(game.Hero.Speed >= monster.Speed)
            {
                Console.WriteLine($"You were able to escape the {monster.Name}! (press any button to continue)");
                Console.ReadKey();
                game.Main();
            }
            else
            {
                Console.WriteLine($"You were NOT able to escape the {monster.Name}! It caught you and killed you! (press any button to continue)");
                Console.ReadKey();
                Lose();
            }
        }
    }
    
}