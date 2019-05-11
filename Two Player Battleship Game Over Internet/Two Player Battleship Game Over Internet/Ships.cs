namespace Two_Player_Battleship_Game_Over_Internet
{
    public abstract class Ships
    {
        public string name { get; protected set; }
        public int length { get; protected set; }
        protected int hits;
        public bool isSunk { get { return hits >= length; } }
        protected VALUE value;



    }

    public class Submarine : Ships
    {
        public Submarine()
        {
            this.name = "Submarine";
            this.length = 3;
            this.hits = 0;
            this.value = VALUE.Submarine;
        }

    }

    public class Battleship : Ships
    {
        public Battleship()
        {
            this.name = "Battleship";
            this.length = 4;
            this.hits = 0;
            this.value = VALUE.Battleship;
        }
    }

    public class Destroyer : Ships
    {
        public Destroyer()
        {
            this.name = "Destroyer";
            this.length = 2;
            this.hits = 0;
            this.value = VALUE.Destroyer;
        }
    }

    public class Cruiser : Ships
    {
        public Cruiser()
        {
            this.name = "Cruiser";
            this.length = 3;
            this.hits = 0;
            this.value = VALUE.Cruiser;
        }

    }

    public class Carrier : Ships
    {
        public Carrier()
        {
            this.name = "Carrier";
            this.length = 5;
            this.hits = 0;
            this.value = VALUE.Carrier;
        }

    }

}