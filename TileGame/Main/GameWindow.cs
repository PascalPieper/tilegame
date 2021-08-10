using System.Numerics;
using ImGuiNET;
using Saffron2D.GuiCollection;
using SFML.Graphics;
using SFML.System;
using TileGame.Game;
using TileGame.Level;
using TileGame.Tiles;

namespace TileGame.Main
{
    class GameWindow
    {
        private Clock deltaTimeClock;
        private Time DeltaTime;
        private int _generationSpeed = 4;

        public GameWindow()
        {
            this.deltaTimeClock = new Clock();
            this.DeltaTime = new Time();
        }

        public void Run()
        {
            //Window Settings
            var mode = new SFML.Window.VideoMode(1920, 1080);
            View view1 = new View(new FloatRect(-120, -15, 512, 288));
            //view1.Zoom(2);
            var window = new SFML.Graphics.RenderWindow(mode, "TileGame Portfolio");
            window.SetFramerateLimit(60);

            //Setup for Input
            window.KeyPressed += this.Window_KeyPressed;
            GuiImpl.Init(window);

            //Instantiation of crucial Managers
            var gameManager = new GameManager();
            Level.Level activeLevel = null;
            var generator = new LevelGenerator(gameManager, new TileFactory(gameManager));
            LoadDefaultLevel(ref activeLevel, generator, _generationSpeed);

            // Start the game loop
            while (window.IsOpen)
            {
                DeltaTime = this.deltaTimeClock.Restart();
                GuiImpl.Update(window, this.DeltaTime);

                #region ImGui Interface

                if (ImGui.Begin("Level Selection"))
                {
                    if (ImGui.SliderInt("Spawn Speed", ref _generationSpeed, 1, 10))
                    {
                    }

                    if (ImGui.Button("Load Exercise 1"))
                    {
                        LoadDefaultLevel(ref activeLevel, generator, _generationSpeed);
                    }

                    if (ImGui.Button("Load Exercise 2"))
                    {
                        activeLevel?.DestroyAllTiles();
                    }

                    if (ImGui.Button("Load Exercise 3"))
                    {
                    }

                    if (ImGui.Button("Load Exercise 4"))
                    {
                    }

                    if (ImGui.Button("Load Exercise 5"))
                    {
                    }

                    if (ImGui.Button("Load Exercise 5"))
                    {
                    }

                    if (ImGui.Button("Tick"))
                    {
                        activeLevel?.Tick();
                    }

                    if (ImGui.Button("Close Game"))
                    {
                        window.Close();
                    }

                    ImGui.End();
                }

                if (ImGui.Begin("Camera"))
                {
                    if (ImGui.Button("Increase Zoom"))
                    {
                        view1.Size = new Vector2f(0, view1.Size.Y + 55);
                    }

                    if (ImGui.Button("Decrease Zoom"))
                    {
                        view1.Size = new Vector2f(view1.Size.X - 55, view1.Size.Y - 55);
                    }

                    if (ImGui.Button("Center Camera"))
                    {
                        view1.Size = new Vector2f(1000, 1000);
                    }

                    if (ImGui.Button("Find Path"))
                    {
                        Pathfinding.Pathfinding pf = new Pathfinding.Pathfinding();
                        if (activeLevel != null)
                            pf.FindPath(new Vector2(activeLevel.SpawnTile.Node.GridX, activeLevel.SpawnTile.Node.GridY),
                                new Vector2(activeLevel.ExitTile.Node.GridX, activeLevel.ExitTile.Node.GridY));
                    }

                    ImGui.End();
                }

                #endregion

                activeLevel?.Update();

                // Process events
                window.SetView(view1);
                window.DispatchEvents();
                gameManager.Draw(window);
                GuiImpl.Render();
                window.Display();
                window.Clear();

                deltaTimeClock.Restart();
            }
        }

        private void Window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            var window = (SFML.Window.Window)sender;
            if (e.Code == SFML.Window.Keyboard.Key.Escape)
            {
                window.Close();
            }
        }

        private void LoadDefaultLevel(ref Level.Level activeLevel, LevelGenerator generator, int generationSpeed)
        {
            activeLevel?.DestroyAllTiles();


            string[] allowedTiles = new[] { "Grass" };
            string[] allowedBlockers = new[] { "Mountains" };
            TileAssembly tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);
            LevelTemplate levelTemplate = new LevelTemplate(tileAssembly, new Vector2i(32, 32),
                new Vector2f(16, 16));
            activeLevel = generator.GenerateLevel(levelTemplate, _generationSpeed);
        }
    }
}