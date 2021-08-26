using System;
using System.Collections.Generic;
using ImGuiNET;
using SFML.Graphics;
using SFML.System;
using TileGame.Character;
using TileGame.Game;
using TileGame.Interfaces;
using TileGame.Pathfinding;
using TileGame.Tiles;

namespace TileGame.Level
{
    public class Level : ITick, IUpdate
    {
        public Tile[,] TileMatrix { get; set; }
        public Tile SpawnTile { get; set; }
        public Tile ExitTile { get; set; }
        public List<Vector2i> EmptyTiles { get; set; }
        public Vector2i LevelSize { get; set; }

        private bool AutoPathFinding { get; set; } = false;

        public Character.Player ActivePlayer { get; set; }

        private readonly GameManager _gameManager;
        public Pathfinding.Pathfinding Pathfinding { get; set; } = null;

        public delegate void LevelTask();

        public readonly Queue<LevelTask> LevelGenerationQueue = new Queue<LevelTask>();
        private int LevelQueueCreationSpeed { get; set; }
        public uint Identifier { get; set; } = 0;


        public Level(GameManager gameManager, Vector2i levelSize)
        {
            this._gameManager = gameManager;
            LevelSize = levelSize;
            LevelQueueCreationSpeed = 10;
        }

        public Level(GameManager gameManager, Vector2i levelSize, int levelQueueCreationSpeed)
        {
            this._gameManager = gameManager;
            LevelSize = levelSize;
            LevelQueueCreationSpeed = levelQueueCreationSpeed;
        }

        public void DestroyAllTiles()
        {
            try
            {
                if (this.TileMatrix == null)
                {
                    return;
                }

                for (var i = 0; i < TileMatrix.GetLength(0); i++)
                {
                    for (var j = 0; j < TileMatrix.GetLength(1); j++)
                    {
                        TileMatrix[i, j] = null;
                    }
                }

                _gameManager.UnloadAllGameObjects();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error at [Level.cs] - The level contents are corrupted and cannot be unloaded " +
                                  e.Message);
            }
        }

        public bool CheckTilePlaced(Vector2i tilePosition)
        {
            return TileMatrix[tilePosition.X, tilePosition.Y] == null;
        }

        public List<Vector2i> FindEmptyTiles()
        {
            EmptyTiles.Clear();
            var empty = new List<Vector2i>();
            for (int i = 0; i < this.TileMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.TileMatrix.GetLength(1); j++)
                {
                    var tempPosition = new Vector2i(i, j);
                    if (CheckTilePlaced(tempPosition))
                    {
                        empty.Add(tempPosition);
                    }
                }
            }

            this.EmptyTiles = empty;
            return empty;
        }

        public void Tick()
        {
        }

        private bool TraverseCheck(Tile target)
        {
            if (target is ITraversable)
            {
                ActivePlayer.OccupiedNode = target.Node;
                var traversable = target as ITraversable;
                traversable.OnEnter();
                target.Tick();
                if (AutoPathFinding)
                {
                    VisualizePathfinder(target.Node, ExitTile.Node);
                }

                return true;
            }

            return false;
        }


        public void MovePlayerRight()
        {
            if (ActivePlayer.CanMove)
            {
                var target = this.TileMatrix[ActivePlayer.OccupiedNode.MatrixPosition.X + 1,
                    ActivePlayer.OccupiedNode.MatrixPosition.Y];
                if (TraverseCheck(target))
                {
                    ActivePlayer.MoveRight();
                    CheckOccupantTile(target, ActivePlayer);
                }
            }
        }

        public void MovePlayerLeft()
        {
            if (ActivePlayer.CanMove)
            {
                var target = this.TileMatrix[ActivePlayer.OccupiedNode.MatrixPosition.X - 1,
                    ActivePlayer.OccupiedNode.MatrixPosition.Y];
                if (TraverseCheck(target))
                {
                    ActivePlayer.MoveLeft();
                    CheckOccupantTile(target, ActivePlayer);
                }
            }
        }

        public void MovePlayerUp()
        {
            if (ActivePlayer.CanMove)
            {
                var target = this.TileMatrix[ActivePlayer.OccupiedNode.MatrixPosition.X,
                    ActivePlayer.OccupiedNode.MatrixPosition.Y - 1];
                if (TraverseCheck(target))
                {
                    ActivePlayer.MoveUp();
                    CheckOccupantTile(target, ActivePlayer);
                }
            }
        }

        public void MovePlayerDown()
        {
            if (ActivePlayer.CanMove)
            {
                var target = this.TileMatrix[ActivePlayer.OccupiedNode.MatrixPosition.X,
                    ActivePlayer.OccupiedNode.MatrixPosition.Y + 1];
                if (TraverseCheck(target))
                {
                    ActivePlayer.MoveDown();
                    CheckOccupantTile(target, ActivePlayer);
                }
            }
        }

        private void RemoveHighlights()
        {
            for (var i = 0; i < TileMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < TileMatrix.GetLength(1); j++)
                {
                    TileMatrix[i, j].HighlightRect.FillColor = new Color(0, 0, 0, 0);
                }
            }
        }

        private void CheckOccupantTile(Tile tile, Player player)
        {
            if (tile.TreasureChest != null)
            {
                if (!tile.TreasureChest.IsUsed && ActivePlayer.ItemInventory.Items.Count < ActivePlayer.ItemInventory.MaxSlots)
                {
                    ActivePlayer.ItemInventory.AddItemToFront(tile.TreasureChest.Open());
                }
            }
        }

        private void VisualizePathfinder(Node startNode, Node endNode)
        {
            RemoveHighlights();
            this.Pathfinding.FindPath(startNode.MatrixPosition, endNode.MatrixPosition);
            var test = Pathfinding.Path;
            var resource = new ResourceManager();
            foreach (var node in test)
            {
                var tile = TileMatrix[node.MatrixPosition.X, node.MatrixPosition.Y];
                tile.HighlightRect.FillColor = new Color(195, 0, 75, 125);
            }
        }

        public void Update()
        {
            if (LevelGenerationQueue.Count == 0)
            {
                ImGui.Begin("Level Options");
                {
                    if (ImGui.Button("Place Items"))
                    {
                    }

                    if (ImGui.Button("Find Spawn to Exit path"))
                    {
                        VisualizePathfinder(SpawnTile.Node, ExitTile.Node);
                    }

                    if (ImGui.Button("Enable / Disable Auto Pathfinding"))
                    {
                        RemoveHighlights();
                        AutoPathFinding = !AutoPathFinding;
                    }
                }
            }

            ImGui.End();

            if (this.LevelGenerationQueue.Count > 0)
            {
                for (int i = 0; i < LevelQueueCreationSpeed; i++)
                {
                    LevelGenerationQueue.TryDequeue(out var task);
                    task?.Invoke();
                }
            }
        }
    }
}