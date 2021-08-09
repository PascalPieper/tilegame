using System.Collections.Generic;
using System.Numerics;

namespace TileGame.Pathfinding
{
    public class Grid
    {
        public Vector2 GridWorldSize;
        public float nodeRadius;
        Node[,] grid;

        public float NodeDiameter { get; private set; }
        public int GridSizeX { get; private set; }
        public int GridSizeY { get; private set; }


        public Grid(int gridSizeX)
        {
            GridSizeX = gridSizeX;
            NodeDiameter = nodeRadius * 2;
        }

        void CreateGrid()
        {
            grid = new Node[GridSizeX,GridSizeY];
            // Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;
            //
            // for (int x = 0; x < GridSizeX; x ++) {
            // 	for (int y = 0; y < GridSizeY; y ++) {
            // 		Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * NodeDiameter + nodeRadius) + Vector3.forward * (y * NodeDiameter + nodeRadius);
            // 		bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unwalkableMask));
            // 		grid[x,y] = new Node(walkable,worldPoint, x,y);
            // 	}
            // }
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();
            int checkX = 0;
            int checkY = 0;
            for (int x = -1; x < 1; x++)
            {
                if (x != 0)
                {
                    checkX = node.GridX + x;
                    checkY = node.GridY + 0;
                    if (checkX >= 0 && checkX < GridSizeX && checkY >= 0 && checkY < GridSizeY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }

            for (int y = -1; y < 1; y++)
            {
                if (y != 0)
                {
                    checkX = node.GridX + 0;
                    checkY = node.GridY + y;
                    if (checkX >= 0 && checkX < GridSizeX && checkY >= 0 && checkY < GridSizeY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }
        
        public List<Node> Path;


        public Node NodeFromWorldPoint(Vector3 startPos)
        {
            // float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
            // float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
            // percentX = Mathf.Clamp01(percentX);
            // percentY = Mathf.Clamp01(percentY);
            //
            // int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            // int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
            // return grid[x, y];
            throw new System.NotImplementedException();
        }
    }
}