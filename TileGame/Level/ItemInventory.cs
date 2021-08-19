using TileGame.Interfaces;
using TileGame.Items;

namespace TileGame.Level
{
    class ItemInventory : Inventory<ItemBase>, IUpdate
    {
        public ItemInventory(int maxSlots) : base(maxSlots)
        {
        }

        public ItemBase ArmorSlot { get; set; }
        public ItemBase WeaponSlot { get; set; }
        public ItemBase RingSlot { get; set; }
        public void Update()
        {
        }
    }
}