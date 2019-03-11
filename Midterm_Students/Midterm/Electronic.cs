using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
    public enum Genre { Action, Shooter, Puzzle, Story }

    class Electronic
    {
        public bool BatteriesIncluded { get; private set; }
        public Genre Genre { get; private set; }
        public string Name { get; private set; }
        public float Price { get; private set; }
        public int Quantity { get; private set; }
        public int AgeRequirement { get; private set; }

        public Electronic(string name, float price, int quantity, int ageRequirement, bool batteriesIncluded, Genre genre)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            BatteriesIncluded = batteriesIncluded;
            Genre = genre;
            AgeRequirement = ageRequirement;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
