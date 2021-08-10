using System.Numerics;
using SFML.System;

namespace TileGame.Pathfinding
{
    public class Node
    {
        public bool Walkable { get; set; }
        public Vector2f WorldPosition { get; set; }
        public int GridX { get; set; }
        public int GridY { get; set; }

        public int GCost;
        public int HCost;

        public int FCost => GCost + HCost;

        public Node Parent;


        public Node(bool walkable, Vector2f worldPos, int gridX, int gridY)
        {
            Walkable = walkable;
            WorldPosition = worldPos;
            GridX = gridX;
            GridY = gridY;
        }
    }
}