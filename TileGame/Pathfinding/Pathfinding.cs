using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.System;
using TileGame.Tiles;

namespace TileGame.Pathfinding
{
    public class Pathfinding
    {
        private Tile[,] TilesMatrix { get; set; }
        public List<Node> Path = new List<Node>();

        public Pathfinding(Tile[,] tilesMatrix)
        {
            TilesMatrix = tilesMatrix;
        }

        public void FindPath(Vector2i startPos, Vector2i targetPos)
        {
            Node startNode = NodeFromWorldPoint(startPos);
            Node targetNode = NodeFromWorldPoint(targetPos);

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node node = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost < node.FCost || openSet[i].FCost == node.FCost)
                    {
                        if (openSet[i].HCost < node.HCost)
                            node = openSet[i];
                    }
                }

                openSet.Remove(node);
                closedSet.Add(node);

                if (node == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return;
                }

                foreach (Node neighbour in GetNeighbours(node, TilesMatrix))
                {
                    if (!neighbour.Walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newCostToNeighbour = node.GCost + GetDistance(node, neighbour);
                    if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetNode);
                        neighbour.Parent = node;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }

        void RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            path.Reverse();

            Path = path;
        }

        int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Math.Abs(nodeA.MatrixPosition.X - nodeB.MatrixPosition.X);
            int dstY = Math.Abs(nodeA.MatrixPosition.Y - nodeB.MatrixPosition.Y);

            return dstX + dstY;
        }

        // public float NodeDiameter { get; private set; }
        // public int GridSizeX { get; private set; }
        // public int GridSizeY { get; private set; }


        public List<Node> GetNeighbours(Node node, Tile[,] tiles)
        {
            List<Node> neighbours = new List<Node>();
            int checkX = 0;
            int checkY = 0;
            for (int x = -1; x <= 1; x++)
            {
                if (x != 0)
                {
                    checkX = node.MatrixPosition.X + x;
                    checkY = node.MatrixPosition.Y;
                    if (checkX >= 0 && checkX < tiles.GetLength(0) && checkY >= 0 && checkY < tiles.GetLength(1))
                    {
                        neighbours.Add(tiles[checkX, checkY].Node);
                    }
                }
            }

            for (int y = -1; y <= 1; y++)
            {
                if (y != 0)
                {
                    checkX = node.MatrixPosition.X + 0;
                    checkY = node.MatrixPosition.Y + y;
                    if (checkX >= 0 && checkX < tiles.GetLength(0) && checkY >= 0 && checkY < tiles.GetLength(1))
                    {
                        neighbours.Add(TilesMatrix[checkX, checkY].Node);
                    }
                }
            }

            return neighbours;
        }


        public Node NodeFromWorldPoint(Vector2i startPos)
        {
            return TilesMatrix[startPos.X, startPos.Y].Node;
        }
    }
}