using System;
using System.Numerics;
using TileGame.Tiles.Behavior;
using SFML.Graphics;
using SFML.System;
using TileGame.Interfaces;
using TileGame.Pathfinding;
using Char = TileGame.Character.Char;

namespace TileGame.Tiles
{
    public abstract class Tile : ITick
    {
        public string Name { get; protected set; }

        public Node Node { get; set; }

        public RectangleShape TileRect { get; set; }

        public uint Identifier => 0;

        public CharacterEffectBehavior Behavior { get; protected set; }

        protected Tile(string name, CharacterEffectBehavior behavior, Color rectColor)
        {
            Name = name;
            TileRect = new RectangleShape();
            Behavior = behavior;
            TileRect.FillColor = rectColor;
            Node = new Node(true, new Vector2f(0,0), new Vector2i(0,0));
        }

        protected Tile()
        {
            Name = "Default Tile";
            TileRect = new RectangleShape();
            Behavior = null;
            Node = new Node(true, new Vector2f(0,0), new Vector2i(0,0));
            Node.GCost = 15;
        }

        public virtual void Tick()
        {
            Console.Write("Ticking");
        }
    }
}