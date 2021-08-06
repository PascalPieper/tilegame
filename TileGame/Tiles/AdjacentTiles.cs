namespace TileGame.Tiles
{
    /// <summary>
    /// Holds references to tiles that count as directionally adjacent
    /// North, East, South, West
    /// </summary>
    public class AdjacentTiles
    {
        public Tile North { get; }
        public Tile East { get; }
        public Tile South { get; }
        public Tile West { get; }

        public AdjacentTiles(Tile north, Tile east, Tile south, Tile west)
        {
            this.North = north;
            this.East = east;
            this.South = south;
            this.West = west;
        }
    }
}