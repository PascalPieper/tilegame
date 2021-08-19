namespace TileGame.Items
{
    public struct ItemAssembly
    {
        public ItemAssembly(string[] spawnableItems)
        {
            SpawnableItems = spawnableItems;
        }

        public string[] SpawnableItems { get; private set; }
    }
}