using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class Potion : IItem
    {
        public int HP { get; set; }
        public string Name { get; set; }
        public int OriginalValue { get; set; }
        public int ResaleValue { get; set; }

        public Potion() { }

        public Potion(int hp, string name)
        {
            HP = hp;
            Name = name;
        }

        public Potion(int hp, string name, int originalValue, int resaleValue)
        {
            HP = hp;
            Name = name;
            OriginalValue = originalValue;
            ResaleValue = resaleValue;
        }
    }
}
