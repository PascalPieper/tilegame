using System;
using System.Numerics;
using TileGame.Tiles.Behavior;
using SFML.Graphics;
using SFML.System;
using TileGame.Interfaces;
using TileGame.Level;
using TileGame.Pathfinding;
using Char = TileGame.Character.Char;

namespace TileGame.Tiles
{
    public abstract class Tile : ITick
    {
        public uint Identifier => 0;

        public string Name { get; protected set; }

        public Node Node { get; set; }

        protected string BaseTextureName { get; set; }

        public RectangleShape TileRect { get; set; }

        public RectangleShape HighlightRect { get; set; }
        public TreasureChest TreasureChest { get; set; } = null;

        public CharacterEffectBehavior Behavior { get; protected set; }

        protected Tile(string name, CharacterEffectBehavior behavior, Color rectColor)
        {
            Name = name;
            TileRect = new RectangleShape();
            Behavior = behavior;
            TileRect.FillColor = rectColor;
            Node = new Node(true, new Vector2f(0, 0), new Vector2i(0, 0));
        }

        protected Tile()
        {
            Name = "Default Tile";
            HighlightRect = new RectangleShape();
            TileRect = new RectangleShape();
            HighlightRect.FillColor = new Color(0, 0, 0, 0);
            Behavior = null;
            Node = new Node(true, new Vector2f(0, 0), new Vector2i(0, 0));
        }

        public virtual void Tick()
        {
            Console.Write("Ticking");
        }
    }
}