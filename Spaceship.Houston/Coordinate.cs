namespace Autopilot
{
    public class Coordinate
    {
        public static readonly Coordinate Center = new Coordinate(0, 0, "N/A");

        public readonly int X;
        public readonly int Y;
        public string Identifier { get; set; }
        //public int Distance { get; set; }

        public Coordinate(int x, int y, string identifier)
        {
            X = x;
            Y = y;
            this.Identifier = identifier;
        }

        public override string ToString()
        {
            // return $"({X},{Y})";
            return "({X},{Y})";
        }

        protected bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Coordinate)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return !Equals(left, right);
        }
    }
}