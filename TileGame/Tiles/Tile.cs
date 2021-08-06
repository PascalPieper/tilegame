using System;
using System.Numerics;
using TileGame.Tiles.Behavior;
using SFML.Graphics;
using SFML.System;
using TileGame.Interfaces;
using Char = TileGame.Character.Char;

namespace TileGame.Tiles
{
    public interface IOccupied
    {
        IOccupyTile OccupyingEntity { get; }
    }

    public abstract class Tile : ITick
    {
        public string Name { get; protected set; }

        public RectangleShape TileRect { get; set; }

        public Vector<uint> CurrentMapPosition { get; set; }

        public AdjacentTiles AdjacentTiles { get; set; } = null;
        public uint Identifier => 0;

        public CharacterEffectBehavior Behavior { get; protected set; }

        protected Tile(string name, CharacterEffectBehavior behavior, Color rectColor)
        {
            Name = name;
            TileRect = new RectangleShape();
            Behavior = behavior;
            TileRect.FillColor = rectColor;
        }

        protected Tile()
        {
            Name = "Default Tile";
            TileRect = new RectangleShape();
            Behavior = null;
        }

        public virtual void Tick()
        {
            Console.Write("Ticking");
        }
    }
}