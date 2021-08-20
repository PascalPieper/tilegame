using ImGuiNET;
using TileGame.Interfaces;
using TileGame.Items;

namespace TileGame.Level
{
    public class ItemInventory : Inventory<ItemBase>, IUpdate
    {
        public ItemInventory(int maxSlots) : base(maxSlots)
        {
        }

        public ItemBase ArmorSlot { get; set; } = null;
        public ItemBase WeaponSlot { get; set; } = null;
        public ItemBase RingSlot { get; set; } = null;
        public void Update()
        {
            DisplayEquipSlot("Trousers", ArmorSlot);
            DisplayEquipSlot("Weapon", ArmorSlot);
            DisplayEquipSlot("Ring", RingSlot);
        }

        public void DisplayEquipSlot(string slotName, ItemBase item)
        {
            ImGui.Begin("Equipment");
            ImGui.LabelText("slot", slotName);
        }
    }
}