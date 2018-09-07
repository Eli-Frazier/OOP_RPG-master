using System;
using System.Collections.Generic;

namespace OOP_RPG
{
    public class Monster
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int OriginalHP { get; set; }
        public int CurrentHP { get; set; }
        public int Gold { get; set; }
        public int Speed { get; set; }

        public Monster() { }

        public Monster(string name, int stength, int defense, int originalHP, int speed)
        {
            this.Name = name;
            this.Strength = Strength;
            this.Defense = defense;
            this.OriginalHP = originalHP;
            this.CurrentHP = originalHP;
            Speed = speed;
            Gold = 5;
        }
    }
}