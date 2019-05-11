namespace Two_Player_Battleship_Game_Over_Internet
{
    public abstract class Ships
    {
        public string name { get; protected set; }
        public int length { get; protected set; }
        public int hits { get; set; }
        public bool isSunk { get { return hits >= length; } }
        public VALUE value { get; protected set; }
        public Direction direction { get; set; }
        public int row { get; set; }
        public int column { get; set; }

    }

    public class Submarine : Ships
    {
        public Submarine()
        {
            this.name = "Submarine";
            this.length = 3;
            this.hits = 0;
            this.value = VALUE.Submarine;
            this.direction = Direction.North;
            
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
            this.direction = Direction.North;
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
            this.direction = Direction.North;
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
            this.direction = Direction.North;
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
            this.direction = Direction.North;
        }

    }

}