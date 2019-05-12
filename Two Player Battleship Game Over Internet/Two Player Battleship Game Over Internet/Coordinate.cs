using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Player_Battleship_Game_Over_Internet
{
    public enum VALUE
    {
        [Description("H")] Hit,
        [Description("M")] Miss,
        [Description("B")] Battleship,
        [Description("S")] Submarine,
        [Description("D")] Destroyer,
        [Description("C")] Cruiser,
        [Description("C")] Carrier,
        [Description("o")] Ocean,
    }

    public class Coordinate
    {
        private int row;
        private int column;
        public VALUE value { get; set; } 

        public Coordinate(int row, int column)
        {
            this.row = row;
            this.column = column;
            this.value = VALUE.Ocean;
        } 
        
        public VALUE Get_Status()
        {
            return this.value;
        }

        public bool Is_Occupied => Get_Status() == VALUE.Ocean ? false : true;


        public override string ToString()
        {
            return value.GetDescription();
        }

        internal bool Hit()
        {
            var hasBeenHit = value == VALUE.Hit || value == VALUE.Miss;
            if (!hasBeenHit)
            {
                if (Is_Occupied)
                {
                    value = VALUE.Hit;
                }
                else
                {
                    value = VALUE.Miss;
                }
            }

            return value == VALUE.Hit;
        }

        internal void WasHit()
        {
            value = VALUE.Hit;
        }

        internal void WasMiss()
        {
            value = VALUE.Miss;
        }
    }
}
