using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
    class Toy
    {
        public int AgeRequirement { get; private set; }
        public string Name { get; private set; }
        public float Price { get; private set; }
        public int Quantity { get; private set; }

        public Toy(string name, float price, int quantity, int ageRequirement)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            AgeRequirement = ageRequirement;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
