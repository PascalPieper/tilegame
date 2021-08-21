using System;
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
            DisplayEquipSlot("Weapon", WeaponSlot);
            DisplayEquipSlot("Ring", RingSlot);
            
            for (int i = 0; i < MaxSlots; i++)
            {
                if (ImGui.Button("Equip Item " + i))
                    {
                        Console.WriteLine("test" + i);
                    }
                    if (ImGui.Button("Destroy Item " + i))
                    {
                        Console.WriteLine("test" + i);
                    }
            }
        }

        public void DisplayEquipSlot(string slotName, ItemBase item)
        {
            ImGui.Begin("Equipment");
            if (ArmorSlot != null)
            {
                ImGui.LabelText(item.Name, item.Description);
            }
            
        }
    }
}