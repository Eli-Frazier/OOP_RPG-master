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

        public Monster() { }

        public Monster(string name, int stength, int defense, int originalHP)
        {
            this.Name = name;
            this.Strength = Strength;
            this.Defense = defense;
            this.OriginalHP = originalHP;
            this.CurrentHP = originalHP;
            Gold = 5;
        }
    }
}