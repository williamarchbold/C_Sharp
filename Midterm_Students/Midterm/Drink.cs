using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
    class Drink
    {
        public int SizeInOunces { get; private set; }
        public string Name { get; private set; }
        public float Price { get; private set; }
        public int Quantity { get; private set; }
        public int CalorieCount { get; private set; }
        public bool IsVegetarian { get; private set; }

        public Drink(string name, float price, int quantity, int calorieCount, int sizeInOunces, bool isVegetarian)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            SizeInOunces = sizeInOunces;
            CalorieCount = calorieCount;
            IsVegetarian = isVegetarian;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
