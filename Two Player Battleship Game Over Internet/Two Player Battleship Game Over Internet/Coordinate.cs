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
        [Description("O")] Ocean,
    }

    class Coordinate
    {
        private int row;
        private int column;
        private VALUE value; 

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

        public bool Is_Occupied()
        {
            return Get_Status() == VALUE.Ocean ? false : true;
        }
    }
}
