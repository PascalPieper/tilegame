namespace Project.Tiles
{
    public struct TileAssembly
    {
        public TileAssembly(string[] traversableTiles, string[] blockadeTiles)
        {
            TraversableTiles = traversableTiles;
            BlockadeTiles = blockadeTiles;
        }

        public string[] TraversableTiles { get; private set; }
        public string[] BlockadeTiles { get; private set; }
    }
}