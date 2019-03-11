using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
    class NonElectronic
    {
        public bool ChokingHazard { get; private set; }
        public string Name { get; private set; }
        public float Price { get; private set; }
        public int Quantity { get; private set; }
        public int AgeRequirement { get; private set; }

        public NonElectronic(string name, float price, int quantity, int ageRequirement, bool chokingHazard)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            ChokingHazard = chokingHazard;
            AgeRequirement = ageRequirement;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
