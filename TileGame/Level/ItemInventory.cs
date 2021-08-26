using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ImGuiNET;
using SFML.Graphics;
using SFML.Window;
using TileGame.Game;
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
            ImGui.Begin("Inventory Utility");
            ImGui.Columns(3);
            if (ImGui.Button("Sort by weight"))
            {
                var sortedItems = SortByWeight();
                Items.Clear();
                foreach (var item in sortedItems)
                {
                    this.Items.Add(item);
                }
            }

            ImGui.NextColumn();
            if (ImGui.Button("Sort by name"))
            {
                var sortedItems = SortByName();
                Items.Clear();
                foreach (var item in sortedItems)
                {
                    this.Items.Add(item);
                }
            }

            ImGui.NextColumn();
            if (ImGui.Button("Sort by price"))
            {
                var sortedItems = SortByPrice();
                Items.Clear();
                foreach (var item in sortedItems)
                {
                    this.Items.Add(item);
                }
            }

            ImGui.End();


            DisplayEquipSlot("Trousers", ArmorSlot);

            DisplayEquipSlot("Weapon", WeaponSlot);

            DisplayEquipSlot("Ring", RingSlot);

            ImGui.Begin("Inventory");
            ImGui.Columns(2);
            for (int i = 0; i < this.Items.Count; i++)
            {
                ImGui.Text(Items[i].Name);
                ImGui.Text("Worth: " + Items[i].Price + "$");
                ImGui.Text("Weight: " + Math.Round(Items[i].Weight, 2));
                
                ImGui.NextColumn();
                int nameNumber = i + 1;
                if (ImGui.Button("Equip Item " + nameNumber))
                {
                    Console.WriteLine("test" + nameNumber);
                }

                if (ImGui.Button("Destroy Item " + nameNumber))
                {
                    Items.Remove(Items[i]);
                }

                ImGui.NewLine();
                ImGui.NextColumn();
            }

            ImGui.End();
        }

        public List<ItemBase> SortByWeight()
        {
            List<ItemBase> sortedItems = Items.OrderBy(itemBase => itemBase.Weight).ToList();
            return sortedItems;
        }

        public List<ItemBase> SortByName()
        {
            List<ItemBase> sortedItems = Items.OrderBy(itemBase => itemBase.Name).ToList();
            return sortedItems;
        }

        public List<ItemBase> SortByPrice()
        {
            List<ItemBase> sortedItems = Items.OrderBy(itemBase => itemBase.Price).ToList();
            return sortedItems;
        }

        public void DisplayEquipSlot(string slotName, ItemBase item)
        {
            ImGui.Begin("Equipment");

            if (ArmorSlot != null)
            {
                ImGui.LabelText(item.Name, item.Description);
            }

            ImGui.End();
        }
    }
}